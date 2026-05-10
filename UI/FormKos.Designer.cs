namespace management_kos.UI;

partial class FormKos
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        pnlInput = new Panel();
        lblTitle = new Label();
        lblNamaKos = new Label();
        txtNamaKos = new TextBox();
        lblAlamat = new Label();
        txtAlamat = new TextBox();
        lblHargaDasar = new Label();
        txtHargaDasar = new TextBox();
        lblJumlahKamar = new Label();
        txtJumlahKamar = new TextBox();
        lblNamaPemilik = new Label();
        txtNamaPemilik = new TextBox();
        lblNomorTelepon = new Label();
        txtNomorTelepon = new TextBox();
        lblCatatan = new Label();
        txtCatatan = new TextBox();
        btnTambah = new Button();
        btnUpdate = new Button();
        btnHapus = new Button();
        btnReset = new Button();
        dgvKos = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvKos).BeginInit();
        SuspendLayout();

        // pnlInput
        pnlInput.BackColor = Color.FromArgb(245, 247, 250);
        pnlInput.Dock = DockStyle.Top;
        pnlInput.Height = 340;
        pnlInput.Padding = new Padding(16);

        //Title
        lblTitle.Text = "Data Kos";
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(32, 43, 59);
        lblTitle.Location = new Point(16, 12);
        lblTitle.AutoSize = true;

        //Input fields (kiri)
        int lx = 16, tx = 160, rowH = 37, startY = 48;

        lblNamaKos.Text = "Nama Kos:"; lblNamaKos.AutoSize = true;
        lblNamaKos.Location = new Point(lx, startY + 4);
        txtNamaKos.Location = new Point(tx, startY); txtNamaKos.Size = new Size(220, 24);

        lblAlamat.Text = "Alamat:"; lblAlamat.AutoSize = true;
        lblAlamat.Location = new Point(lx, startY + rowH + 4);
        txtAlamat.Location = new Point(tx, startY + rowH); txtAlamat.Size = new Size(220, 24);

        lblHargaDasar.Text = "Harga Dasar (Rp):"; lblHargaDasar.AutoSize = true;
        lblHargaDasar.Location = new Point(lx, startY + rowH * 2 + 4);
        txtHargaDasar.Location = new Point(tx, startY + rowH * 2); txtHargaDasar.Size = new Size(220, 24);

        lblJumlahKamar.Text = "Jumlah Kamar:"; lblJumlahKamar.AutoSize = true;
        lblJumlahKamar.Location = new Point(lx, startY + rowH * 3 + 4);
        txtJumlahKamar.Location = new Point(tx, startY + rowH * 3); txtJumlahKamar.Size = new Size(220, 24);

        lblNamaPemilik.Text = "Nama Pemilik:"; lblNamaPemilik.AutoSize = true;
        lblNamaPemilik.Location = new Point(lx, startY + rowH * 4 + 4);
        txtNamaPemilik.Location = new Point(tx, startY + rowH * 4); txtNamaPemilik.Size = new Size(220, 24);

        lblNomorTelepon.Text = "Nomor Telepon:"; lblNomorTelepon.AutoSize = true;
        lblNomorTelepon.Location = new Point(lx, startY + rowH * 5 + 4);
        txtNomorTelepon.Location = new Point(tx, startY + rowH * 5); txtNomorTelepon.Size = new Size(220, 24);

        lblCatatan.Text = "Catatan:"; lblCatatan.AutoSize = true;
        lblCatatan.Location = new Point(lx, startY + rowH * 6 + 4);
        txtCatatan.Location = new Point(tx, startY + rowH * 6); txtCatatan.Size = new Size(300, 24);

        //Tombol kanan atas berwarna
        int bx = 430, by = startY, bw = 110, bh = 30, bg = 8;

        btnTambah.Text = "Tambah";
        btnTambah.Location = new Point(bx, by);
        btnTambah.Size = new Size(bw, bh);
        btnTambah.BackColor = Color.FromArgb(37, 99, 235);
        btnTambah.ForeColor = Color.White;
        btnTambah.FlatStyle = FlatStyle.Flat;
        btnTambah.Click += btnTambah_Click;

        btnUpdate.Text = "Update";
        btnUpdate.Location = new Point(bx + bw + bg, by);
        btnUpdate.Size = new Size(bw, bh);
        btnUpdate.BackColor = Color.FromArgb(245, 158, 11);
        btnUpdate.ForeColor = Color.White;
        btnUpdate.FlatStyle = FlatStyle.Flat;
        btnUpdate.Click += btnUpdate_Click;

        btnHapus.Text = "Hapus";
        btnHapus.Location = new Point(bx + (bw + bg) * 2, by);
        btnHapus.Size = new Size(bw, bh);
        btnHapus.BackColor = Color.FromArgb(220, 38, 38);
        btnHapus.ForeColor = Color.White;
        btnHapus.FlatStyle = FlatStyle.Flat;
        btnHapus.Click += btnHapus_Click;

        btnReset.Text = "Reset Form";
        btnReset.Location = new Point(bx, by + bh + bg);
        btnReset.Size = new Size(bw, bh);
        btnReset.FlatStyle = FlatStyle.Flat;
        btnReset.Click += btnReset_Click;

        //Tambah ke pnlInput
        pnlInput.Controls.AddRange(new Control[]
        {
            lblTitle,
            lblNamaKos,      txtNamaKos,
            lblAlamat,       txtAlamat,
            lblHargaDasar,   txtHargaDasar,
            lblJumlahKamar,  txtJumlahKamar,
            lblNamaPemilik,  txtNamaPemilik,
            lblNomorTelepon, txtNomorTelepon,
            lblCatatan,      txtCatatan,
            btnTambah, btnUpdate, btnHapus, btnReset
        });

        //DataGridView
        dgvKos.Dock = DockStyle.Fill;
        dgvKos.ReadOnly = true;
        dgvKos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvKos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKos.AllowUserToAddRows = false;
        dgvKos.AllowUserToDeleteRows = false;
        dgvKos.BackgroundColor = Color.White;
        dgvKos.BorderStyle = BorderStyle.None;
        dgvKos.RowHeadersVisible = false;
        dgvKos.Font = new Font("Segoe UI", 9F);
        dgvKos.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(32, 43, 59),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };
        dgvKos.EnableHeadersVisualStyles = false;
        dgvKos.MultiSelect = false;
        dgvKos.CellClick += dgvKos_CellClick;
        dgvKos.CellContentClick += dgvKos_CellContentClick;

        //FormKos
        this.Text = "Management Kos - Data Kos";
        this.Size = new Size(900, 680);
        this.Font = new Font("Segoe UI", 9F);
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Controls.Add(dgvKos);
        this.Controls.Add(pnlInput);
        Load += FormKos_Load;
        ((System.ComponentModel.ISupportInitialize)dgvKos).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInput;
    private Label lblTitle;
    private Label lblNamaKos;
    private TextBox txtNamaKos;
    private Label lblAlamat;
    private TextBox txtAlamat;
    private Label lblHargaDasar;
    private TextBox txtHargaDasar;
    private Label lblJumlahKamar;
    private TextBox txtJumlahKamar;
    private Label lblNamaPemilik;
    private TextBox txtNamaPemilik;
    private Label lblNomorTelepon;
    private TextBox txtNomorTelepon;
    private Label lblCatatan;
    private TextBox txtCatatan;
    private Button btnTambah;
    private Button btnUpdate;
    private Button btnHapus;
    private Button btnReset;
    private DataGridView dgvKos;
}