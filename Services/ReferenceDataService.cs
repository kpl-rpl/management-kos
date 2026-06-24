using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class ReferenceDataService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMetodePembayaranRefRepository _metodePembayaranRepository;

    public ReferenceDataService(
        IRoleRepository roleRepository,
        IMetodePembayaranRefRepository metodePembayaranRepository)
    {
        _roleRepository = roleRepository;
        _metodePembayaranRepository = metodePembayaranRepository;
    }

    public List<Role> GetAllRoles() => _roleRepository.GetAll();

    private static readonly List<MetodePembayaranRef> _metodePembayaranList = new()
    {
        new MetodePembayaranRef
        {
            Id = 1,
            NamaMetode = "Transfer",
            IsActive = true
        },
        new MetodePembayaranRef
        {
            Id = 2,
            NamaMetode = "Tunai",
            IsActive = true
        },
        new MetodePembayaranRef
        {
            Id = 3,
            NamaMetode = "QRIS",
            IsActive = true
        }
    };

public List<MetodePembayaranRef> GetAllMetodePembayaran()
{
    return _metodePembayaranList;
}

    public void TambahRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        NormalizeRole(role);
        ValidateRole(role);
        EnsureRoleNameUnique(role.NamaRole, null);
        _roleRepository.Insert(role);
    }

    public void UbahRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        Guard.Positive(role.Id, "ID Role");
        NormalizeRole(role);
        ValidateRole(role);
        EnsureRoleNameUnique(role.NamaRole, role.Id);
        _roleRepository.Update(role);
    }

    public void NonaktifkanRole(int id)
    {
        Guard.Positive(id, "ID Role");
        _roleRepository.Delete(id);
    }

    public void TambahMetodePembayaran(MetodePembayaranRef metode)
    {
        ArgumentNullException.ThrowIfNull(metode);
        NormalizeMetodePembayaran(metode);
        ValidateMetodePembayaran(metode);
        EnsureMetodePembayaranNameUnique(metode.NamaMetode, null);
        _metodePembayaranRepository.Insert(metode);
    }

    public void UbahMetodePembayaran(MetodePembayaranRef metode)
    {
        ArgumentNullException.ThrowIfNull(metode);
        Guard.Positive(metode.Id, "ID Metode Pembayaran");
        NormalizeMetodePembayaran(metode);
        ValidateMetodePembayaran(metode);
        EnsureMetodePembayaranNameUnique(metode.NamaMetode, metode.Id);
        _metodePembayaranRepository.Update(metode);
    }

    public void NonaktifkanMetodePembayaran(int id)
    {
        Guard.Positive(id, "ID Metode Pembayaran");
        _metodePembayaranRepository.Delete(id);
    }

    private void EnsureRoleNameUnique(string namaRole, int? currentId)
    {
        var existing = _roleRepository.GetByName(namaRole);
        if (existing is not null && existing.Id != currentId)
        {
            throw new ArgumentException("Nama role sudah digunakan.");
        }
    }

    private void EnsureMetodePembayaranNameUnique(string namaMetode, int? currentId)
    {
        var existing = _metodePembayaranRepository.GetByName(namaMetode);
        if (existing is not null && existing.Id != currentId)
        {
            throw new ArgumentException("Nama metode pembayaran sudah digunakan.");
        }
    }

    private static void NormalizeRole(Role role)
    {
        role.NamaRole = role.NamaRole.Trim();
        role.Deskripsi = string.IsNullOrWhiteSpace(role.Deskripsi) ? null : role.Deskripsi.Trim();
    }

    private static void ValidateRole(Role role)
    {
        if (string.IsNullOrWhiteSpace(role.NamaRole))
        {
            throw new ArgumentException("Nama role wajib diisi.");
        }
    }

    private static void NormalizeMetodePembayaran(MetodePembayaranRef metode)
    {
        metode.NamaMetode = metode.NamaMetode.Trim();
    }

    private static void ValidateMetodePembayaran(MetodePembayaranRef metode)
    {
        if (string.IsNullOrWhiteSpace(metode.NamaMetode))
        {
            throw new ArgumentException("Nama metode pembayaran wajib diisi.");
        }
    }
}
