using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class PembayaranService
{
    private readonly IPembayaranRepository _pembayaranRepository;
    private readonly IKontrakSewaRepository _kontrakRepository;

    public PembayaranService(IPembayaranRepository pembayaranRepository, IKontrakSewaRepository kontrakRepository)
    {
        _pembayaranRepository = pembayaranRepository;
        _kontrakRepository = kontrakRepository;
    }

    public List<Pembayaran> GetAll()
    {
        return _pembayaranRepository.GetAll();
    }

    public List<Pembayaran> GetByKontrak(int kontrakId)
    {
        if (kontrakId <= 0)
            throw new ArgumentException("ID Kontrak tidak valid.");

        return _pembayaranRepository.GetByKontrakSewaId(kontrakId);
    }

    public PembayaranSummary GetSummary(int kontrakId)
    {
        if (kontrakId <= 0)
            throw new ArgumentException("ID Kontrak tidak valid.");

        var kontrak = _kontrakRepository.GetById(kontrakId)
            ?? throw new ArgumentException("Kontrak sewa tidak ditemukan.");

        var totalDibayar = _pembayaranRepository
            .GetByKontrakSewaId(kontrakId)
            .Sum(p => p.JumlahDibayar);

        return new PembayaranSummary
        {
            KontrakSewaId = kontrakId,
            TotalTagihan = kontrak.TotalTagihan,
            TotalDibayar = totalDibayar
        };
    }

    public void CatatPembayaran(Pembayaran pembayaran)
    {
        Validate(pembayaran);

        if (pembayaran.TanggalBayar is null)
            pembayaran.TanggalBayar = DateTime.Today;

        _pembayaranRepository.Insert(pembayaran);
    }

    public void UbahPembayaran(Pembayaran pembayaran)
    {
        if (pembayaran.Id <= 0)
            throw new ArgumentException("ID Pembayaran tidak valid.");

        Validate(pembayaran);
        _pembayaranRepository.Update(pembayaran);
    }

    public void HapusPembayaran(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID tidak valid.");

        _pembayaranRepository.Delete(id);
    }

    private static void Validate(Pembayaran pembayaran)
    {
        if (pembayaran == null)
            throw new ArgumentNullException(nameof(pembayaran));

        if (pembayaran.KontrakSewaId <= 0)
            throw new ArgumentException("Kontrak sewa harus dipilih.");

        if (pembayaran.JumlahDibayar <= 0)
            throw new ArgumentException("Jumlah dibayar harus lebih dari 0.");

        if (string.IsNullOrWhiteSpace(pembayaran.MetodePembayaran))
            throw new ArgumentException("Metode pembayaran wajib diisi.");
    }
}
