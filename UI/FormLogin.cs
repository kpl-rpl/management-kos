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

        Text = "Login Admin";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(390, 260);
        Font = new Font("Segoe UI", 9F);
        BackColor = Color.White;
        AcceptButton = _btnLogin = new Button();
        CancelButton = _btnCancel = new Button();

        var lblTitle = new Label
        {
            Text = "Management Kos",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(32, 43, 59),
            AutoSize = true,
            Location = new Point(32, 24)
        };

        var lblSubtitle = new Label
        {
            Text = "Masuk sebagai admin/operator untuk melanjutkan",
            ForeColor = Color.FromArgb(75, 85, 99),
            AutoSize = true,
            Location = new Point(35, 58)
        };

        var lblUsername = new Label
        {
            Text = "Username",
            AutoSize = true,
            Location = new Point(35, 95)
        };

        _txtUsername = new TextBox
        {
            Location = new Point(130, 91),
            Size = new Size(220, 24),
            TabIndex = 0
        };

        var lblPassword = new Label
        {
            Text = "Password",
            AutoSize = true,
            Location = new Point(35, 130)
        };

        _txtPassword = new TextBox
        {
            Location = new Point(130, 126),
            Size = new Size(220, 24),
            UseSystemPasswordChar = true,
            TabIndex = 1
        };

        _lblError = new Label
        {
            ForeColor = Color.FromArgb(220, 38, 38),
            AutoSize = false,
            Location = new Point(35, 162),
            Size = new Size(315, 32)
        };

        _btnLogin.Text = "Login";
        _btnLogin.Location = new Point(170, 205);
        _btnLogin.Size = new Size(85, 32);
        _btnLogin.BackColor = Color.FromArgb(37, 99, 235);
        _btnLogin.ForeColor = Color.White;
        _btnLogin.FlatStyle = FlatStyle.Flat;
        _btnLogin.TabIndex = 2;
        _btnLogin.Click += btnLogin_Click;

        _btnCancel.Text = "Batal";
        _btnCancel.Location = new Point(265, 205);
        _btnCancel.Size = new Size(85, 32);
        _btnCancel.FlatStyle = FlatStyle.Flat;
        _btnCancel.TabIndex = 3;
        _btnCancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.AddRange(new Control[]
        {
            lblTitle,
            lblSubtitle,
            lblUsername,
            _txtUsername,
            lblPassword,
            _txtPassword,
            _lblError,
            _btnLogin,
            _btnCancel
        });
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
