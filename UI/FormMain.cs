using management_kos.Services;

namespace management_kos.UI;

public partial class FormMain : Form
{
    private readonly KosService _kosService;
    private readonly KamarService _kamarService;
    private readonly PenghuniService _penghuniService;
    private readonly HomeView _homeView;
    private readonly PembayaranService _pembayaranService;
    private readonly KontrakSewaService _kontrakSewaService;
    private FormKos? _formKos;
    private FormKamar? _formKamar;
    private FormPenghuni? _formPenghuni;
    private FormPembayaran? _formPembayaran;
    private FormKontrakSewa? _formKontrakSewa;

    public FormMain(KosService kosService, KamarService kamarService, PenghuniService penghuniService, PembayaranService pembayaranService, KontrakSewaService kontrakSewaService)
    {
        _kosService = kosService;
        _kamarService = kamarService;
        _penghuniService = penghuniService;
        _pembayaranService = pembayaranService;
        _kontrakSewaService = kontrakSewaService;
        _homeView = new HomeView { Dock = DockStyle.Fill };
        InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
        ShowHomeView();
    }

    private void btnHome_Click(object sender, EventArgs e)
    {
        ShowHomeView();
    }

    private void btnDataKos_Click(object sender, EventArgs e)
    {
        ShowDataKosView();
    }

    private void ShowHomeView()
    {
        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_homeView);
        lblCurrentView.Text = "Home";
    }

    private void ShowDataKosView()
    {
        if (_formKos is null || _formKos.IsDisposed)
            _formKos = new FormKos(_kosService) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_formKos);
        _formKos.Show();
        _formKos.BringToFront();
        _formKos.RefreshData();
        lblCurrentView.Text = "Data Kos";
    }

    private void ShowDataKamarView()
    {
        if (_formKamar is null || _formKamar.IsDisposed)
            _formKamar = new FormKamar(_kamarService, _kosService) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_formKamar);
        _formKamar.Show();
        _formKamar.BringToFront();
        _formKamar.RefreshData();
        lblCurrentView.Text = "Data Kamar";
    }

    private void ShowDataPenghuniView()
    {
        if (_formPenghuni is null || _formPenghuni.IsDisposed)
            _formPenghuni = new FormPenghuni(_penghuniService, _kamarService) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_formPenghuni);
        _formPenghuni.Show();
        _formPenghuni.BringToFront();
        _formPenghuni.RefreshData();
        lblCurrentView.Text = "Data Penghuni";
    }

    private void ShowDataPembayaranView()
    {
        if (_formPembayaran is null || _formPembayaran.IsDisposed)
            _formPembayaran = new FormPembayaran(_pembayaranService) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_formPembayaran);
        _formPembayaran.Show();
        _formPembayaran.BringToFront();
        _formPembayaran.RefreshData();
        lblCurrentView.Text = "Data Pembayaran";
    }

    private void ShowDataKontrakSewaView()
    {
        if (_formKontrakSewa is null || _formKontrakSewa.IsDisposed)
            _formKontrakSewa = new FormKontrakSewa(_kontrakSewaService) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_formKontrakSewa);
        _formKontrakSewa.Show();
        _formKontrakSewa.BringToFront();
        _formKontrakSewa.RefreshData();
        lblCurrentView.Text = "Data Kontrak Sewa";
    }

    private void btnDataKamar_Click(object sender, EventArgs e) => ShowDataKamarView();
    private void btnDataPenghuni_Click(object sender, EventArgs e) => ShowDataPenghuniView();
    private void btnDataPembayaran_Click(object sender, EventArgs e) => ShowDataPembayaranView();
    private void btnDataKontrakSewa_Click(object sender, EventArgs e) => ShowDataKontrakSewaView();
}
