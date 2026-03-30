namespace management_kos.UI;

public class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        var lblTitle = new Label();
        var lblDescription = new Label();

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitle.Location = new Point(24, 24);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(262, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Selamat Datang 👋";

        lblDescription.AutoSize = true;
        lblDescription.Font = new Font("Segoe UI", 10F);
        lblDescription.Location = new Point(28, 72);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(408, 57);
        lblDescription.TabIndex = 1;
        lblDescription.Text = "Ini halaman Home aplikasi Management Kos.\r\nGunakan menu kiri untuk pindah ke modul Data Kos.\r\nStruktur ini bisa dipakai sebagai dasar modul lainnya.";

        BackColor = Color.White;
        Controls.Add(lblDescription);
        Controls.Add(lblTitle);
        Name = "HomeView";
        Size = new Size(900, 600);
    }
}
