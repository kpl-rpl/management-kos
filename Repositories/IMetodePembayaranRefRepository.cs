using management_kos.Models;

namespace management_kos.Repositories;

public interface IMetodePembayaranRefRepository
{
    List<MetodePembayaranRef> GetAll();
    MetodePembayaranRef? GetById(int id);
    MetodePembayaranRef? GetByName(string namaMetode);
    void Insert(MetodePembayaranRef metode);
    void Update(MetodePembayaranRef metode);
    void Delete(int id);
}
