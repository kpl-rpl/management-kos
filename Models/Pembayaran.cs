namespace management_kos.Models;

public class Pembayaran
{
    public int Id { get; set; }
    public int KontrakSewaId { get; set; }
    public DateTime? TanggalBayar { get; set; }
    public decimal JumlahDibayar { get; set; }
    public string MetodePembayaran { get; set; } = string.Empty;
    public string? Catatan { get; set; }
}
