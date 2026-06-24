using System.Text.RegularExpressions;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class PenghuniService
{
    private readonly IPenghuniRepository _penghuniRepository;

    private static readonly Regex PhoneRegex =
        new(@"^[0-9+\-\s]{8,20}$", RegexOptions.Compiled);

    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public PenghuniService(IPenghuniRepository penghuniRepository, IKamarRepository _)
    {
        _penghuniRepository = penghuniRepository;
    }

    public List<Penghuni> GetAllPenghuni() => _penghuniRepository.GetAll();

    public Penghuni? GetPenghuniById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID Penghuni tidak valid.");

        return _penghuniRepository.GetById(id);
    }

    public void TambahPenghuni(Penghuni penghuni)
    {
        Validate(penghuni);
        _penghuniRepository.Insert(penghuni);
    }

    public void UbahPenghuni(Penghuni penghuni)
    {
        if (penghuni.Id <= 0)
            throw new ArgumentException("ID Penghuni tidak valid.");

        Validate(penghuni);

        _ = _penghuniRepository.GetById(penghuni.Id)
            ?? throw new ArgumentException("Penghuni tidak ditemukan.");

        _penghuniRepository.Update(penghuni);
    }

    public void HapusPenghuni(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID Penghuni tidak valid.");

        _ = _penghuniRepository.GetById(id)
            ?? throw new ArgumentException("Penghuni tidak ditemukan.");

        _penghuniRepository.Delete(id);
    }

    public void CheckOutPenghuni(int id, DateTime tanggalKeluar)
    {
        if (id <= 0)
            throw new ArgumentException("ID Penghuni tidak valid.");

        var penghuni = _penghuniRepository.GetById(id)
            ?? throw new ArgumentException("Penghuni tidak ditemukan.");

        if (penghuni.TanggalMasuk.HasValue && tanggalKeluar < penghuni.TanggalMasuk)
            throw new ArgumentException("Tanggal keluar tidak boleh sebelum tanggal masuk.");

        penghuni.TanggalKeluar = tanggalKeluar;
        _penghuniRepository.Update(penghuni);
    }

    private static void Validate(Penghuni p)
    {
        p.Nama = p.Nama?.Trim() ?? string.Empty;
        p.NomorTelepon = p.NomorTelepon?.Trim() ?? string.Empty;
        p.Email = string.IsNullOrWhiteSpace(p.Email) ? null : p.Email.Trim();

        var rules = new List<(Func<Penghuni, bool> IsInvalid, string Message)>
        {
            (x => string.IsNullOrWhiteSpace(x.Nama), "Nama penghuni wajib diisi."),
            (x => string.IsNullOrWhiteSpace(x.NomorTelepon) || !PhoneRegex.IsMatch(x.NomorTelepon),
                "Nomor Telepon wajib diisi dengan format yang valid."),
            (x => x.Email is not null && !EmailRegex.IsMatch(x.Email), "Format Email tidak valid."),
            (x => x.TanggalKeluar.HasValue && x.TanggalMasuk.HasValue && x.TanggalKeluar.Value < x.TanggalMasuk,
                "Tanggal Keluar tidak boleh sebelum Tanggal Masuk."),
        };

        foreach (var rule in rules)
        {
            if (rule.IsInvalid(p))
                throw new ArgumentException(rule.Message);
        }
    }
}
