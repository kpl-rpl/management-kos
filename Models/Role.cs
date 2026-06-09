namespace management_kos.Models;

public class Role
{
    public int Id { get; set; }
    public string NamaRole { get; set; } = string.Empty;
    public string? Deskripsi { get; set; }
    public bool IsActive { get; set; } = true;
}
