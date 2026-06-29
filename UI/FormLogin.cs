using management_kos.Models;
using management_kos.Services;

namespace management_kos.UI;

public class FormLogin : Form
{
    private readonly AppUserService _appUserService;
    private readonly TextBox _txtUsername;
    private readonly TextBox _txtPassword;
    private readonly Button _btnLogin;
    private readonly Button _btnCancel;
    private readonly Label _lblError;

    public AppUser? AuthenticatedUser { get; private set; }

    public FormLogin(AppUserService appUserService)
    {
        _appUserService = appUserService ?? throw new ArgumentNullException(nameof(appUserService));

        const int cardWidth = 360;
        const int cardHeight = 500;
        const int margin = 40;

        Text = "Login - Management Kos";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(cardWidth + margin * 2, cardHeight + margin * 2);
        Font = new Font("Segoe UI", 9F);
        BackColor = Color.FromArgb(245, 247, 250);

        AcceptButton = _btnLogin = new Button();
        CancelButton = _btnCancel = new Button();

        var card = new Panel
        {
            BackColor = Color.White,
            Size = new Size(cardWidth, cardHeight),
            Location = new Point(margin, margin)
        };

        int contentW = cardWidth - 56; 

        var iconPanel = new Panel
        {
            BackColor = Color.FromArgb(230, 241, 251),
            Size = new Size(52, 52),
            Location = new Point((cardWidth - 52) / 2, 28)
        };
        var lblIcon = new Label
        {
            Text = "🏠",
            Font = new Font("Segoe UI", 20F),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };
        iconPanel.Controls.Add(lblIcon);

        var lblTitle = new Label
        {
            Text = "Management Kos",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            ForeColor = Color.FromArgb(32, 43, 59),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(cardWidth, 28),
            Location = new Point(0, 92)
        };

        var lblSubtitle = new Label
        {
            Text = "Masuk sebagai admin/operator\nuntuk melanjutkan",
            Font = new Font("Segoe UI", 8.5F),
            ForeColor = Color.FromArgb(107, 114, 128),
            AutoSize = false,
            TextAlign = ContentAlignment.TopCenter,
            Size = new Size(cardWidth, 36),
            Location = new Point(0, 122)
        };

        var lblDemoAccount = new Label
        {
            Text = "Akun demo: admin / admin123",
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(32, 43, 59),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(cardWidth, 24),
            Location = new Point(0, 160)
        };

        var lblUsername = new Label
        {
            Text = "Username",
            Font = new Font("Segoe UI", 8.5F),
            ForeColor = Color.FromArgb(75, 85, 99),
            AutoSize = true,
            Location = new Point(28, 198)
        };

        _txtUsername = new TextBox
        {
            Location = new Point(28, 219),
            Size = new Size(contentW, 28),
            Font = new Font("Segoe UI", 10F),
            TabIndex = 0,
            PlaceholderText = "Masukkan username"
        };

        var lblPassword = new Label
        {
            Text = "Password",
            Font = new Font("Segoe UI", 8.5F),
            ForeColor = Color.FromArgb(75, 85, 99),
            AutoSize = true,
            Location = new Point(28, 260)
        };

        _txtPassword = new TextBox
        {
            Location = new Point(28, 281),
            Size = new Size(contentW, 28),
            Font = new Font("Segoe UI", 10F),
            UseSystemPasswordChar = true,
            TabIndex = 1,
            PlaceholderText = "Masukkan password"
        };

        _lblError = new Label
        {
            ForeColor = Color.FromArgb(220, 38, 38),
            Font = new Font("Segoe UI", 8.5F),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(28, 320),
            Size = new Size(contentW, 32)
        };

        _btnLogin.Text = "Login";
        _btnLogin.Location = new Point(28, 360);
        _btnLogin.Size = new Size(contentW, 40);
        _btnLogin.BackColor = Color.FromArgb(32, 43, 59);
        _btnLogin.ForeColor = Color.White;
        _btnLogin.FlatStyle = FlatStyle.Flat;
        _btnLogin.FlatAppearance.BorderSize = 0;
        _btnLogin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _btnLogin.Cursor = Cursors.Hand;
        _btnLogin.TabIndex = 2;
        _btnLogin.Click += btnLogin_Click;

        _btnCancel.Text = "Batal";
        _btnCancel.Location = new Point(28, 408);
        _btnCancel.Size = new Size(contentW, 36);
        _btnCancel.FlatStyle = FlatStyle.Flat;
        _btnCancel.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
        _btnCancel.ForeColor = Color.FromArgb(75, 85, 99);
        _btnCancel.Cursor = Cursors.Hand;
        _btnCancel.TabIndex = 3;
        _btnCancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        card.Controls.AddRange(new Control[]
        {
            iconPanel,
            lblTitle,
            lblSubtitle,
            lblDemoAccount,
            lblUsername,
            _txtUsername,
            lblPassword,
            _txtPassword,
            _lblError,
            _btnLogin,
            _btnCancel
        });

        Controls.Add(card);
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _txtUsername.Focus();
    }

    private void btnLogin_Click(object? sender, EventArgs e)
    {
        _lblError.Text = string.Empty;

        var username = _txtUsername.Text.Trim();
        var password = _txtPassword.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            _lblError.Text = "Username dan password wajib diisi.";
            return;
        }

        try
        {
            var user = _appUserService.Authenticate(username, password);
            if (user is null)
            {
                _lblError.Text = "Username atau password salah.";
                _txtPassword.Clear();
                _txtPassword.Focus();
                return;
            }

            AuthenticatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            _lblError.Text = ex.Message;
        }
    }
}
