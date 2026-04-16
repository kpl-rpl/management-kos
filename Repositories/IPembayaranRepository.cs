using management_kos.Models;

namespace management_kos.Repositories;

public interface IPembayaranRepository
{
    List<Pembayaran> GetAll();
    Pembayaran? GetById(int id);
    List<Pembayaran> GetByKontrakSewaId(int kontrakSewaId);
    void Insert(Pembayaran pembayaran);
    void Update(Pembayaran pembayaran);
    void Delete(int id);
}
