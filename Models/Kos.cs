namespace management_kos.Models;

public class Kos
{
    public int Id { get; set; }
    public string NamaKos { get; set; } = string.Empty;
    public string Alamat { get; set; } = string.Empty;
    public decimal HargaDasar { get; set; }
    public int JumlahKamar { get; set; }
    public string NamaPemilik { get; set; } = string.Empty;
    public string NomorTelepon { get; set; } = string.Empty;
    public string? Catatan { get; set; }
}
