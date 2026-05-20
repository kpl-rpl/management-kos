using System.Text.RegularExpressions;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class PenghuniService
{
    private readonly IPenghuniRepository _penghuniRepository;
    private readonly IKamarRepository _kamarRepository;

    private static readonly Regex PhoneRegex =
    new(@"^[0-9+\-\s]{8,20}$", RegexOptions.Compiled);

    private static readonly Regex EmailRegex =
    new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

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
        UpdateStatusKamar(penghuni.KamarId, KamarStatus.Terisi);
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
            UpdateStatusKamar(penghuni.KamarId, KamarStatus.Terisi);
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

        if (kamar.Status == KamarStatus.Perbaikan)
        {
            throw new InvalidOperationException(
                "Kamar sedang dalam perbaikan dan tidak dapat ditempati.");
        }

        if (kamar.Status == KamarStatus.Terisi)
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

        var statusBaru = penghuniAktif ? KamarStatus.Terisi : KamarStatus.Kosong;
        UpdateStatusKamar(kamarId, statusBaru);
    }

    private void UpdateStatusKamar(int kamarId, KamarStatus status)
    {
        var kamar = _kamarRepository.GetById(kamarId);
        if (kamar is null) return;

        kamar.Status = status;
        _kamarRepository.Update(kamar);
    }

    private static void Validate(Penghuni p)
    {
        // Normalisasi dulu sebelum validasi
        p.Nama = p.Nama?.Trim() ?? "";
        p.NomorTelepon = p.NomorTelepon?.Trim() ?? "";
        p.Email = string.IsNullOrWhiteSpace(p.Email)
            ? null
            : p.Email.Trim();

        // Tabel aturan validasi
        var rules = new List<(Func<Penghuni, bool> IsInvalid, string Message)>
    {
        (x => x.KamarId <= 0,
            "KamarId wajib diisi dan harus lebih dari 0."),

        (x => string.IsNullOrWhiteSpace(x.Nama),
            "Nama penghuni wajib diisi."),

        (x => string.IsNullOrWhiteSpace(x.NomorTelepon),
            "Nomor Telepon wajib diisi."),

        (x => !PhoneRegex.IsMatch(x.NomorTelepon),
            "Format Nomor Telepon tidak valid."),

        (x => x.Email is not null && !EmailRegex.IsMatch(x.Email),
            "Format Email tidak valid."),

        (x => x.TanggalMasuk == default,
            "Tanggal Masuk wajib diisi."),

        (x => x.TanggalKeluar.HasValue &&
              x.TanggalKeluar.Value < x.TanggalMasuk,
            "Tanggal Keluar tidak boleh sebelum Tanggal Masuk."),
    };

        foreach (var rule in rules)
        {
            if (rule.IsInvalid(p))
                throw new ArgumentException(rule.Message);
        }
    }
}