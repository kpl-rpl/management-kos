namespace management_kos.Models;

public class MetodePembayaranRef
{
    public int Id { get; set; }
    public string NamaMetode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
