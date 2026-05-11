namespace management_kos.Models;

public class KontrakSewa
{
    public int Id { get; set; }
    public int PenghuniId { get; set; }
    public int KamarId { get; set; }
    public DateTime TanggalMulai { get; set; } = DateTime.Today;
    public DateTime TanggalSelesai { get; set; } = DateTime.Today.AddMonths(12);
    public decimal HargaSewaBulanan { get; set; }
    public decimal? Deposit { get; set; }
    public string Status { get; set; } = "Aktif";
    public string? Catatan { get; set; }

    public string DisplayText =>
        $"#{Id} — Kamar {KamarId} | {TanggalMulai:dd MMM yyyy} s/d {TanggalSelesai:dd MMM yyyy} ({Status})";
}
