using management_kos.Data;
using management_kos.Repositories;
using management_kos.Services;
using management_kos.UI;

namespace management_kos
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var dbContext = new MySqlDbContext();
            dbContext.InitializeDatabase();

            IKosRepository kosRepository = new KosRepository(dbContext);
            var kosService = new KosService(kosRepository);

            Application.Run(new FormKos(kosService));
        }
    }
}
