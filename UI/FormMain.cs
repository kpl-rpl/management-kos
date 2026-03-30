using management_kos.Services;

namespace management_kos.UI;

public partial class FormMain : Form
{
    private readonly KosService _kosService;
    private readonly HomeView _homeView;
    private FormKos? _formKos;

    public FormMain(KosService kosService)
    {
        _kosService = kosService;
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
        {
            _formKos = new FormKos(_kosService)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
        }

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(_formKos);
        _formKos.Show();
        _formKos.BringToFront();
        lblCurrentView.Text = "Data Kos";
    }
}
