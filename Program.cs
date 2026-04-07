using management_kos.Data;
using management_kos.Repositories;
using management_kos.Services;
using management_kos.UI;
using System.Runtime.InteropServices;

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

            IKamarRepository kamarRepository = new KamarRepository(dbContext);
            var kamarService = new KamarService(kamarRepository, kosRepository);

            Application.Run(new FormMain(kosService, kamarService));
        }
    }
}
