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
            // ===== TEST MODE =====

            [DllImport("kernel32.dll")]
            static extern bool AttachConsole(int dwProcessId);

            const int ATTACH_PARENT_PROCESS = -1;
            AttachConsole(ATTACH_PARENT_PROCESS);

            management_kos.Services.KamarServiceTest.Run();
            return;
            // ===== UI MODE =====
            //Application.Run(new FormMain(kosService));
        }
    }
}
