using management_kos.Models;

namespace management_kos.Repositories;

public interface IKontrakSewaRepository : IRepository<KontrakSewa>
{
    List<KontrakSewa> GetByPenghuniId(int penghuniId);
    List<KontrakSewa> GetByKamarId(int kamarId);
    List<KontrakSewa> GetByStatus(string status);
}
