namespace management_kos.Models;

public class PembayaranSummary
{
    public int KontrakSewaId { get; set; }
    public decimal TotalTagihan { get; set; }
    public decimal TotalDibayar { get; set; }
    public decimal SisaPembayaran => Math.Max(0, TotalTagihan - TotalDibayar);
    public bool Lunas => SisaPembayaran <= 0;
}
