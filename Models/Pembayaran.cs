namespace management_kos.Models;

public class Pembayaran
{
    public int Id { get; set; }
    public int KontrakSewaId { get; set; }
    public string Periode { get; set; } = string.Empty;
    public DateTime? TanggalBayar { get; set; }
    public decimal JumlahTagihan { get; set; }
    public decimal JumlahDibayar { get; set; }
    public string MetodePembayaran { get; set; } = "Transfer";
    public string Status { get; set; } = "BelumBayar";
    public string? Catatan { get; set; }
}
