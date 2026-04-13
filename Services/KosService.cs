using System.Text.RegularExpressions;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class KosService
{
    private readonly IKosRepository _kosRepository;
    private static readonly Regex PhoneRegex = new(@"^[0-9+\-\s]{8,20}$", RegexOptions.Compiled);

    public KosService(IKosRepository kosRepository)
    {
        _kosRepository = kosRepository ?? throw new ArgumentNullException(nameof(kosRepository));
    }

    public List<Kos> GetAllKos()
    {
        return _kosRepository.GetAll();
    }

    public void TambahKos(Kos kos)
    {
        ArgumentNullException.ThrowIfNull(kos);
        NormalizeInput(kos);
        Validate(kos);
        _kosRepository.Insert(kos);
    }

    public void UbahKos(Kos kos)
    {
        ArgumentNullException.ThrowIfNull(kos);

        if (kos.Id <= 0)
        {
            throw new ArgumentException("ID Kos tidak valid.");
        }

        if (_kosRepository.GetById(kos.Id) is null)
        {
            throw new ArgumentException("Data kos tidak ditemukan.");
        }

        NormalizeInput(kos);
        Validate(kos);
        _kosRepository.Update(kos);
    }

    public void HapusKos(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID Kos tidak valid.");
        }

        if (_kosRepository.GetById(id) is null)
        {
            throw new ArgumentException("Data kos tidak ditemukan.");
        }

        _kosRepository.Delete(id);
    }

    private static void NormalizeInput(Kos kos)
    {
        kos.NamaKos = kos.NamaKos.Trim();
        kos.Alamat = kos.Alamat.Trim();
        kos.NamaPemilik = kos.NamaPemilik.Trim();
        kos.NomorTelepon = kos.NomorTelepon.Trim();
        kos.Catatan = string.IsNullOrWhiteSpace(kos.Catatan) ? null : kos.Catatan.Trim();
    }

    private static void Validate(Kos kos)
    {
        // Table-driven validation
        var rules = new List<(Func<Kos, bool> IsInvalid, string Message)>
        {
            (x => string.IsNullOrWhiteSpace(x.NamaKos), "Nama Kos wajib diisi."),
            (x => string.IsNullOrWhiteSpace(x.Alamat), "Alamat wajib diisi."),
            (x => x.HargaDasar <= 0, "Harga Dasar harus lebih dari 0."),
            (x => x.JumlahKamar <= 0, "Jumlah Kamar harus lebih dari 0."),
            (x => string.IsNullOrWhiteSpace(x.NamaPemilik), "Nama Pemilik wajib diisi."),
            (x => string.IsNullOrWhiteSpace(x.NomorTelepon), "Nomor Telepon wajib diisi."),
            (x => !PhoneRegex.IsMatch(x.NomorTelepon), "Format Nomor Telepon tidak valid.")
        };

        foreach (var rule in rules)
        {
            if (rule.IsInvalid(kos))
            {
                throw new ArgumentException(rule.Message);
            }
        }
    }
}
