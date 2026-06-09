using management_kos.Models;

namespace management_kos.Repositories;

public interface IAppUserRepository
{
    List<AppUser> GetAll();
    AppUser? GetById(int id);
    AppUser? GetByUsername(string username);
    void Insert(AppUser user);
    void Update(AppUser user);
    void Delete(int id);
}
