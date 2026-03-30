using management_kos.Models;

namespace management_kos.Repositories;

public interface IKosRepository
{
    List<Kos> GetAll();
    Kos? GetById(int id);
    void Insert(Kos kos);
    void Update(Kos kos);
    void Delete(int id);
}
