namespace management_kos.Models;

public class KontrakSewa
{
    public int Id { get; set; }
    public int PenghuniId { get; set; }
    public int KamarId { get; set; }
    public DateTime TanggalMulai { get; set; } = DateTime.Today;
    public DateTime TanggalSelesai { get; set; } = DateTime.Today.AddMonths(1);
    public decimal DurasiBulanInput { get; set; } = 1;
    public int JumlahBulanTagihan { get; set; } = 1;
    public decimal HargaSewaBulanan { get; set; }
    public decimal TotalTagihan { get; set; }
    public decimal? Deposit { get; set; }
    public KontrakStatus Status { get; set; } = KontrakStatus.Aktif;
    public string? Catatan { get; set; }
    public bool IsActive { get; set; } = true;
    public string? NamaPenghuni { get; set; }
    public string? InfoKamar { get; set; }

    public string DisplayText =>
        $"#{Id} - {NamaPenghuni ?? $"Penghuni {PenghuniId}"} | {InfoKamar ?? $"Kamar {KamarId}"}";
}

public enum KontrakStatus
{
    Dipesan,
    Aktif,
    Selesai,
    Dibatalkan
}
