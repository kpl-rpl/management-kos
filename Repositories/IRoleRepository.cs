using management_kos.Models;

namespace management_kos.Repositories;

public interface IRoleRepository
{
    List<Role> GetAll();
    Role? GetById(int id);
    Role? GetByName(string namaRole);
    void Insert(Role role);
    void Update(Role role);
    void Delete(int id);
}
