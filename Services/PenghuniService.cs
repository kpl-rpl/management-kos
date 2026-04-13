using System.Text.RegularExpressions;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class PenghuniService
{
    private readonly IPenghuniRepository _penghuniRepository;
    private readonly IKamarRepository _kamarRepository;

    public PenghuniService(IPenghuniRepository penghuniRepository, IKamarRepository kamarRepository)
    {
        _penghuniRepository = penghuniRepository;
        _kamarRepository = kamarRepository;
    }

    // Read 

    public List<Penghuni> GetAllPenghuni()
    {
        return _penghuniRepository.GetAll();
    }

    public Penghuni? GetPenghuniById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID Penghuni tidak valid.");
        }

        return _penghuniRepository.GetById(id);
    }

    public List<Penghuni> GetPenghuniByKamarId(int kamarId)
    {
        if (kamarId <= 0)
        {
            throw new ArgumentException("KamarId tidak valid.");
        }

        return _penghuniRepository.GetByKamarId(kamarId);
    }

    // Write 

    public void TambahPenghuni(Penghuni penghuni)
    {
        Validate(penghuni);
        EnsureKamarExists(penghuni.KamarId);
        EnsureKamarTersedia(penghuni.KamarId);

        _penghuniRepository.Insert(penghuni);

        // Tandai kamar menjadi Terisi setelah penghuni ditambahkan
        UpdateStatusKamar(penghuni.KamarId, "Terisi");
    }

    public void UbahPenghuni(Penghuni penghuni)
    {
        if (penghuni.Id <= 0)
        {
            throw new ArgumentException("ID Penghuni tidak valid.");
        }

        Validate(penghuni);

        var existing = _penghuniRepository.GetById(penghuni.Id)
            ?? throw new ArgumentException("Penghuni tidak ditemukan.");

        // Jika pindah kamar, pastikan kamar tujuan tersedia
        if (existing.KamarId != penghuni.KamarId)
        {
            EnsureKamarExists(penghuni.KamarId);
            EnsureKamarTersedia(penghuni.KamarId);
        }

        _penghuniRepository.Update(penghuni);

        // Jika pindah kamar, perbarui status kedua kamar
        if (existing.KamarId != penghuni.KamarId)
        {
            UpdateStatusKamar(penghuni.KamarId, "Terisi");
            RecalculateStatusKamarLama(existing.KamarId);
        }
    }

    public void HapusPenghuni(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID Penghuni tidak valid.");
        }

        var penghuni = _penghuniRepository.GetById(id)
            ?? throw new ArgumentException("Penghuni tidak ditemukan.");

        var kamarId = penghuni.KamarId;

        _penghuniRepository.Delete(id);

        // Kembalikan status kamar ke Kosong jika sudah tidak ada penghuni aktif
        RecalculateStatusKamarLama(kamarId);
    }

    public void CheckOutPenghuni(int id, DateTime tanggalKeluar)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID Penghuni tidak valid.");
        }

        var penghuni = _penghuniRepository.GetById(id)
            ?? throw new ArgumentException("Penghuni tidak ditemukan.");

        if (tanggalKeluar < penghuni.TanggalMasuk)
        {
            throw new ArgumentException("Tanggal keluar tidak boleh sebelum tanggal masuk.");
        }

        penghuni.TanggalKeluar = tanggalKeluar;
        _penghuniRepository.Update(penghuni);

        // Perbarui status kamar berdasarkan penghuni aktif yang tersisa
        RecalculateStatusKamarLama(penghuni.KamarId);
    }

    //Private helpers
    private void EnsureKamarExists(int kamarId)
    {
        var kamar = _kamarRepository.GetById(kamarId);
        if (kamar is null)
        {
            throw new ArgumentException("KamarId tidak ditemukan. Pastikan data kamar sudah ada.");
        }
    }

    private void EnsureKamarTersedia(int kamarId)
    {
        var kamar = _kamarRepository.GetById(kamarId)!;

        if (kamar.Status.Equals("Perbaikan", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Kamar sedang dalam perbaikan dan tidak dapat ditempati.");
        }

        if (kamar.Status.Equals("Terisi", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Kamar sudah terisi. Pilih kamar lain yang masih kosong.");
        }
    }

    /// Hitung ulang status kamar lama berdasarkan apakah masih ada penghuni aktif.
    /// Penghuni aktif = belum punya TanggalKeluar.
    private void RecalculateStatusKamarLama(int kamarId)
    {
        var penghuniAktif = _penghuniRepository
            .GetByKamarId(kamarId)
            .Any(p => p.TanggalKeluar is null);

        var statusBaru = penghuniAktif ? "Terisi" : "Kosong";
        UpdateStatusKamar(kamarId, statusBaru);
    }

    private void UpdateStatusKamar(int kamarId, string status)
    {
        var kamar = _kamarRepository.GetById(kamarId);
        if (kamar is null) return;

        kamar.Status = status;
        _kamarRepository.Update(kamar);
    }

    private static void Validate(Penghuni penghuni)
    {
        if (penghuni.KamarId <= 0)
        {
            throw new ArgumentException("KamarId wajib diisi dan harus lebih dari 0.");
        }

        if (string.IsNullOrWhiteSpace(penghuni.Nama))
        {
            throw new ArgumentException("Nama penghuni wajib diisi.");
        }

        penghuni.Nama = penghuni.Nama.Trim();

        if (string.IsNullOrWhiteSpace(penghuni.NomorTelepon))
        {
            throw new ArgumentException("Nomor Telepon wajib diisi.");
        }

        penghuni.NomorTelepon = penghuni.NomorTelepon.Trim();

        if (!Regex.IsMatch(penghuni.NomorTelepon, @"^[0-9+\-\s]{8,20}$"))
        {
            throw new ArgumentException("Format Nomor Telepon tidak valid.");
        }

        if (penghuni.Email is not null)
        {
            penghuni.Email = penghuni.Email.Trim();

            if (!string.IsNullOrWhiteSpace(penghuni.Email) &&
                !Regex.IsMatch(penghuni.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Format Email tidak valid.");
            }
        }

        if (penghuni.TanggalMasuk == default)
        {
            throw new ArgumentException("Tanggal Masuk wajib diisi.");
        }

        if (penghuni.TanggalKeluar.HasValue &&
            penghuni.TanggalKeluar.Value < penghuni.TanggalMasuk)
        {
            throw new ArgumentException("Tanggal Keluar tidak boleh sebelum Tanggal Masuk.");
        }
    }
}