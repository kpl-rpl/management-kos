using System.Text.RegularExpressions;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class KosService
{
    private readonly IKosRepository _kosRepository;

    public KosService(IKosRepository kosRepository)
    {
        _kosRepository = kosRepository;
    }

    public List<Kos> GetAllKos()
    {
        return _kosRepository.GetAll();
    }

    public void TambahKos(Kos kos)
    {
        Validate(kos);
        _kosRepository.Insert(kos);
    }

    public void UbahKos(Kos kos)
    {
        if (kos.Id <= 0)
        {
            throw new ArgumentException("ID Kos tidak valid.");
        }

        Validate(kos);
        _kosRepository.Update(kos);
    }

    public void HapusKos(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID Kos tidak valid.");
        }

        _kosRepository.Delete(id);
    }

    private static void Validate(Kos kos)
    {
        if (string.IsNullOrWhiteSpace(kos.NamaKos))
        {
            throw new ArgumentException("Nama Kos wajib diisi.");
        }

        if (string.IsNullOrWhiteSpace(kos.Alamat))
        {
            throw new ArgumentException("Alamat wajib diisi.");
        }

        if (kos.HargaDasar <= 0)
        {
            throw new ArgumentException("Harga Dasar harus lebih dari 0.");
        }

        if (kos.JumlahKamar <= 0)
        {
            throw new ArgumentException("Jumlah Kamar harus lebih dari 0.");
        }

        if (string.IsNullOrWhiteSpace(kos.NamaPemilik))
        {
            throw new ArgumentException("Nama Pemilik wajib diisi.");
        }

        if (string.IsNullOrWhiteSpace(kos.NomorTelepon))
        {
            throw new ArgumentException("Nomor Telepon wajib diisi.");
        }

        if (!Regex.IsMatch(kos.NomorTelepon, @"^[0-9+\-\s]{8,20}$"))
        {
            throw new ArgumentException("Format Nomor Telepon tidak valid.");
        }
    }
}
