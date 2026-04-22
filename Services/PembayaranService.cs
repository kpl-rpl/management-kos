using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class PembayaranService
{
    private readonly IPembayaranRepository _pembayaranRepository;

    public PembayaranService(IPembayaranRepository pembayaranRepository)
    {
        _pembayaranRepository = pembayaranRepository;
    }

    public List<Pembayaran> GetAll()
    {
        return _pembayaranRepository.GetAll();
    }

    public List<Pembayaran> GetByKontrak(int kontrakId)
    {
        if (kontrakId <= 0) throw new ArgumentException("ID Kontrak tidak valid.");
        return _pembayaranRepository.GetByKontrakSewaId(kontrakId);
    }

    public void TambahTagihan(Pembayaran p)
    {
        Validate(p);

        // Logika otomatis: Jika baru tambah tagihan dan belum ada uang masuk
        if (p.JumlahDibayar == 0)
        {
            p.Status = "BelumBayar";
        }
        else
        {
            UpdateStatusPembayaran(p);
        }

        _pembayaranRepository.Insert(p);
    }

    public void BayarTagihan(int id, decimal nominal, string metode)
    {
        var pembayaran = _pembayaranRepository.GetById(id);
        if (pembayaran == null) throw new Exception("Data tagihan tidak ditemukan.");

        pembayaran.JumlahDibayar += nominal;
        pembayaran.MetodePembayaran = metode;
        pembayaran.TanggalBayar = DateTime.Now;

        UpdateStatusPembayaran(pembayaran);
        _pembayaranRepository.Update(pembayaran);
    }

    public void UbahPembayaran(Pembayaran p)
    {
        if (p.Id <= 0) throw new ArgumentException("ID Pembayaran tidak valid.");
        Validate(p);
        UpdateStatusPembayaran(p);
        _pembayaranRepository.Update(p);
    }

    public void HapusPembayaran(int id)
    {
        if (id <= 0) throw new ArgumentException("ID tidak valid.");
        _pembayaranRepository.Delete(id);
    }

    // --- Helper Logic ---

    private void UpdateStatusPembayaran(Pembayaran p)
    {
        if (p.JumlahDibayar >= p.JumlahTagihan)
        {
            p.Status = "Lunas";
        }
        else if (p.JumlahDibayar > 0)
        {
            p.Status = "Dicicil";
        }
        else
        {
            p.Status = "BelumBayar";
        }
    }

    private void Validate(Pembayaran p)
    {
        if (p.KontrakSewaId <= 0)
            throw new ArgumentException("Kontrak sewa harus dipilih.");

        if (string.IsNullOrWhiteSpace(p.Periode))
            throw new ArgumentException("Periode (misal: Januari 2024) wajib diisi.");

        if (p.JumlahTagihan <= 0)
            throw new ArgumentException("Jumlah tagihan harus lebih dari 0.");

        if (p.JumlahDibayar < 0)
            throw new ArgumentException("Jumlah dibayar tidak boleh negatif.");
    }
}