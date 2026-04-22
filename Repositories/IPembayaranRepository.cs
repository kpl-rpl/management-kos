using management_kos.Models;

namespace management_kos.Repositories;

public interface IPembayaranRepository : IRepository<Pembayaran>
{
    List<Pembayaran> GetByKontrakSewaId(int kontrakSewaId);
}
