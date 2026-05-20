using management_kos.Models;
using management_kos.Repositories;
using System;
using System.Linq;

namespace management_kos.Services;

// Parameterization/generics: helper guard clause yang dapat dipakai lintas tipe data.
// Constraint where T : struct memastikan hanya value type yang diterima,
// dan where T : struct, IComparable<T> memungkinkan perbandingan generik.
public static class Guard
{
    public static T NotDefault<T>(T value, string paramName) where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new ArgumentException($"{paramName} tidak boleh kosong atau nol.");
        return value;
    }

    public static string NotEmpty(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} wajib diisi.");
        return value.Trim();
    }

    public static T Positive<T>(T value, string paramName) where T : struct, IComparable<T>
    {
        if (value.CompareTo(default) <= 0)
            throw new ArgumentException($"{paramName} harus lebih dari 0.");
        return value;
    }
}

public class KontrakSewaService
{
    private readonly IKontrakSewaRepository _repo;
    private readonly IPenghuniRepository _penghuniRepo;
    private readonly IKamarRepository _kamarRepo;

    public KontrakSewaService(
        IKontrakSewaRepository repo,
        IPenghuniRepository penghuniRepo,
        IKamarRepository kamarRepo)
    {
        _repo = repo;
        _penghuniRepo = penghuniRepo;
        _kamarRepo = kamarRepo;
    }

    public List<KontrakSewa> GetAll() => _repo.GetAll();

    public KontrakSewa? GetById(int id)
    {
        Guard.Positive(id, "ID");
        return _repo.GetById(id);
    }

    public List<KontrakSewa> GetByPenghuni(int penghuniId)
    {
        Guard.Positive(penghuniId, "ID Penghuni");
        return _repo.GetByPenghuniId(penghuniId);
    }

    public void TambahKontrak(KontrakSewa k)
    {
        Validate(k);
        EnsurePenghuniExists(k.PenghuniId);
        EnsureKamarExists(k.KamarId);
        EnsureKamarAvailable(k, null);
        _repo.Insert(k);
        UpdateKamarStatusForKontrak(k);
    }

    public void UbahKontrak(KontrakSewa k)
    {
        Guard.Positive(k.Id, "ID Kontrak");
        var existing = _repo.GetById(k.Id) ?? throw new Exception("Kontrak tidak ditemukan.");
        Validate(k);
        EnsurePenghuniExists(k.PenghuniId);
        EnsureKamarExists(k.KamarId);
        EnsureKamarAvailable(k, k.Id);
        _repo.Update(k);
        if (existing.KamarId != k.KamarId)
        {
            ResetKamarIfNoActiveOrBooked(existing.KamarId, existing.Id);
        }
        UpdateKamarStatusForKontrak(k);
    }

    public void SelesaikanKontrak(int id)
    {
        Guard.Positive(id, "ID Kontrak");
        var kontrak = _repo.GetById(id) ?? throw new Exception("Kontrak tidak ditemukan.");
        kontrak.Status = KontrakStatus.Selesai;
        _repo.Update(kontrak);
        UpdateKamarStatusForKontrak(kontrak);
    }

    public void BatalkanKontrak(int id)
    {
        Guard.Positive(id, "ID Kontrak");
        var kontrak = _repo.GetById(id) ?? throw new Exception("Kontrak tidak ditemukan.");
        kontrak.Status = KontrakStatus.Dibatalkan;
        _repo.Update(kontrak);
        UpdateKamarStatusForKontrak(kontrak);
    }

    public void HapusKontrak(int id)
    {
        Guard.Positive(id, "ID Kontrak");
        var kontrak = _repo.GetById(id) ?? throw new Exception("Kontrak tidak ditemukan.");
        _repo.Delete(id);
        ResetKamarIfNoActiveOrBooked(kontrak.KamarId, kontrak.Id);
    }

    private void Validate(KontrakSewa k)
    {
        Guard.Positive(k.PenghuniId, "ID Penghuni");
        Guard.Positive(k.KamarId, "ID Kamar");
        Guard.Positive(k.HargaSewaBulanan, "Harga Sewa Bulanan");

        if (k.TanggalSelesai <= k.TanggalMulai)
            throw new ArgumentException("Tanggal selesai harus lebih dari tanggal mulai.");

        if (k.Deposit.HasValue && k.Deposit < 0)
            throw new ArgumentException("Deposit tidak boleh negatif.");

        if (!Enum.IsDefined(typeof(KontrakStatus), k.Status))
            throw new ArgumentException("Status tidak valid. Gunakan: Dipesan, Aktif, Selesai, atau Dibatalkan.");
    }

    private void EnsurePenghuniExists(int id)
    {
        if (_penghuniRepo.GetById(id) is null)
            throw new ArgumentException("Penghuni tidak ditemukan. Pastikan data penghuni sudah ada.");
    }

    private void EnsureKamarExists(int id)
    {
        if (_kamarRepo.GetById(id) is null)
            throw new ArgumentException("Kamar tidak ditemukan. Pastikan data kamar sudah ada.");
    }

    private void EnsureKamarAvailable(KontrakSewa k, int? excludeKontrakId)
    {
        var kamar = _kamarRepo.GetById(k.KamarId) ?? throw new ArgumentException("Kamar tidak ditemukan.");
        if (kamar.Status == KamarStatus.Perbaikan)
            throw new ArgumentException("Kamar sedang perbaikan dan tidak bisa dibooking.");

        var kontrakAktif = _repo
            .GetByKamarId(k.KamarId)
            .Any(x => x.Id != (excludeKontrakId ?? 0)
                && (x.Status == KontrakStatus.Aktif || x.Status == KontrakStatus.Dipesan));

        if (kontrakAktif)
            throw new ArgumentException("Kamar sudah dibooking atau sedang terisi.");

        if (k.Status == KontrakStatus.Dipesan && kamar.Status != KamarStatus.Kosong)
            throw new ArgumentException("Kamar hanya bisa dibooking jika statusnya Kosong.");
    }

    private void UpdateKamarStatusForKontrak(KontrakSewa kontrak)
    {
        var kamar = _kamarRepo.GetById(kontrak.KamarId);
        if (kamar is null) return;

        var targetStatus = kontrak.Status switch
        {
            KontrakStatus.Aktif => KamarStatus.Terisi,
            KontrakStatus.Dipesan => KamarStatus.Dipesan,
            _ => (KamarStatus?)null
        };

        if (targetStatus is null)
        {
            ResetKamarIfNoActiveOrBooked(kontrak.KamarId, kontrak.Id);
            return;
        }

        if (kamar.Status != targetStatus)
        {
            kamar.Status = targetStatus.Value;
            _kamarRepo.Update(kamar);
        }
    }

    private void ResetKamarIfNoActiveOrBooked(int kamarId, int kontrakId)
    {
        var kamar = _kamarRepo.GetById(kamarId);
        if (kamar is null) return;

        var masihAktif = _repo
            .GetByKamarId(kamarId)
            .Any(x => x.Id != kontrakId && (x.Status == KontrakStatus.Aktif || x.Status == KontrakStatus.Dipesan));

        if (!masihAktif && kamar.Status != KamarStatus.Kosong)
        {
            kamar.Status = KamarStatus.Kosong;
            _kamarRepo.Update(kamar);
        }
    }
}
