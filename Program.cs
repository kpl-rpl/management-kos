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

            if (!dbContext.TestConnection(out var dbError))
            {
                MessageBox.Show(
                    $"Tidak dapat terhubung ke database MySQL.\nPastikan MySQL sudah aktif dan coba lagi.\n\nDetail: {dbError}",
                    "Koneksi Database Gagal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            dbContext.InitializeDatabase();

            IKosRepository kosRepository = new KosRepository(dbContext);
            var kosService = new KosService(kosRepository);

            IKamarRepository kamarRepository = new KamarRepository(dbContext);
            var kamarService = new KamarService(kamarRepository, kosRepository);

            IPenghuniRepository penghuniRepository = new PenghuniRepository(dbContext);
            var penghuniService = new PenghuniService(penghuniRepository, kamarRepository);

            IPembayaranRepository pembayaranRepository = new PembayaranRepository(dbContext);
            var pembayaranService = new PembayaranService(pembayaranRepository);

            IKontrakSewaRepository kontrakSewaRepository = new KontrakSewaRepository(dbContext);
            var kontrakSewaService = new KontrakSewaService(kontrakSewaRepository, penghuniRepository, kamarRepository);

            Application.Run(new FormMain(kosService, kamarService, penghuniService, pembayaranService, kontrakSewaService));
        }
    }
}
