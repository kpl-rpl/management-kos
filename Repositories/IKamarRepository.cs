using management_kos.Models;

namespace management_kos.Repositories;
public interface IKamarRepository
{
    List<Kamar> GetAll();
    Kamar? GetById(int id);
    List<Kamar> GetByKosId(int kosId);

    void Insert(Kamar kamar);
    void Update(Kamar kamar);
    void Delete(int id);
}