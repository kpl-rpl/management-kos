namespace management_kos.UI
{
    partial class FormKontrakSewa
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlInput          = new Panel();
            lblTitle          = new Label();
            lblPenghuniId     = new Label();
            cmbPenghuni       = new ComboBox();
            lblKamarId        = new Label();
            cmbKamar          = new ComboBox();
            lblTanggalMulai   = new Label();
            dtpTanggalMulai   = new DateTimePicker();
            lblTanggalSelesai = new Label();
            dtpTanggalSelesai = new DateTimePicker();
            lblHarga          = new Label();
            txtHarga          = new TextBox();
            lblDeposit        = new Label();
            txtDeposit        = new TextBox();
            lblStatus         = new Label();
            cmbStatus         = new ComboBox();
            lblCatatan        = new Label();
            txtCatatan        = new TextBox();
            btnTambah         = new Button();
            btnUpdate         = new Button();
            btnHapus          = new Button();
            btnSelesai        = new Button();
            btnBatal          = new Button();
            btnReset          = new Button();
            dgvKontrak        = new DataGridView();

            // Panel input
            pnlInput.BackColor = Color.FromArgb(245, 247, 250);
            pnlInput.Dock      = DockStyle.Top;
            pnlInput.Height    = 370;
            pnlInput.Padding   = new Padding(16);

            // Judul
            lblTitle.Text      = "Data Kontrak Sewa";
            lblTitle.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(32, 43, 59);
            lblTitle.Location  = new Point(16, 12);
            lblTitle.AutoSize  = true;

            // Penghuni
            lblPenghuniId.Text     = "Penghuni:";
            lblPenghuniId.Location = new Point(16, 50);
            lblPenghuniId.AutoSize = true;
            cmbPenghuni.Location      = new Point(180, 47);
            cmbPenghuni.Size          = new Size(220, 24);
            cmbPenghuni.DropDownStyle = ComboBoxStyle.DropDownList;

            // Kamar
            lblKamarId.Text     = "Kamar:";
            lblKamarId.Location = new Point(16, 82);
            lblKamarId.AutoSize = true;
            cmbKamar.Location      = new Point(180, 79);
            cmbKamar.Size          = new Size(220, 24);
            cmbKamar.DropDownStyle = ComboBoxStyle.DropDownList;

            // Tanggal Mulai
            lblTanggalMulai.Text     = "Tanggal Mulai:";
            lblTanggalMulai.Location = new Point(16, 114);
            lblTanggalMulai.AutoSize = true;
            dtpTanggalMulai.Location = new Point(180, 111);
            dtpTanggalMulai.Size     = new Size(160, 24);
            dtpTanggalMulai.Format   = DateTimePickerFormat.Short;

            // Tanggal Selesai
            lblTanggalSelesai.Text     = "Tanggal Selesai:";
            lblTanggalSelesai.Location = new Point(16, 146);
            lblTanggalSelesai.AutoSize = true;
            dtpTanggalSelesai.Location = new Point(180, 143);
            dtpTanggalSelesai.Size     = new Size(160, 24);
            dtpTanggalSelesai.Format   = DateTimePickerFormat.Short;
            dtpTanggalSelesai.Value    = DateTime.Today.AddMonths(12);

            // Harga Sewa
            lblHarga.Text     = "Harga Sewa/Bulan (Rp):";
            lblHarga.Location = new Point(16, 178);
            lblHarga.AutoSize = true;
            txtHarga.Location = new Point(200, 175);
            txtHarga.Size     = new Size(140, 24);

            // Deposit
            lblDeposit.Text     = "Deposit (Rp, opsional):";
            lblDeposit.Location = new Point(16, 210);
            lblDeposit.AutoSize = true;
            txtDeposit.Location = new Point(200, 207);
            txtDeposit.Size     = new Size(140, 24);

            // Status
            lblStatus.Text     = "Status:";
            lblStatus.Location = new Point(16, 242);
            lblStatus.AutoSize = true;
            cmbStatus.Location       = new Point(200, 239);
            cmbStatus.Size           = new Size(140, 24);
            cmbStatus.DropDownStyle  = ComboBoxStyle.DropDownList;

            // Catatan
            lblCatatan.Text     = "Catatan:";
            lblCatatan.Location = new Point(16, 274);
            lblCatatan.AutoSize = true;
            txtCatatan.Location = new Point(200, 271);
            txtCatatan.Size     = new Size(300, 24);

            // Tombol-tombol
            int btnY = 314;

            btnTambah.Name      = "btnTambah";
            btnTambah.Text      = "Tambah";
            btnTambah.Location  = new Point(16, btnY);
            btnTambah.Size      = new Size(90, 32);
            btnTambah.BackColor = Color.FromArgb(37, 99, 235);
            btnTambah.ForeColor = Color.White;
            btnTambah.FlatStyle = FlatStyle.Flat;
            btnTambah.Click    += btnTambah_Click;

            btnUpdate.Name      = "btnUpdate";
            btnUpdate.Text      = "Update";
            btnUpdate.Location  = new Point(116, btnY);
            btnUpdate.Size      = new Size(90, 32);
            btnUpdate.BackColor = Color.FromArgb(245, 158, 11);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Click    += btnUpdate_Click;

            btnHapus.Name      = "btnHapus";
            btnHapus.Text      = "Hapus";
            btnHapus.Location  = new Point(216, btnY);
            btnHapus.Size      = new Size(90, 32);
            btnHapus.BackColor = Color.FromArgb(220, 38, 38);
            btnHapus.ForeColor = Color.White;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.Click    += btnHapus_Click;

            btnSelesai.Name      = "btnSelesai";
            btnSelesai.Text      = "Selesaikan";
            btnSelesai.Location  = new Point(316, btnY);
            btnSelesai.Size      = new Size(100, 32);
            btnSelesai.BackColor = Color.FromArgb(22, 163, 74);
            btnSelesai.ForeColor = Color.White;
            btnSelesai.FlatStyle = FlatStyle.Flat;
            btnSelesai.Click    += btnSelesai_Click;

            btnBatal.Name      = "btnBatal";
            btnBatal.Text      = "Batalkan";
            btnBatal.Location  = new Point(426, btnY);
            btnBatal.Size      = new Size(90, 32);
            btnBatal.BackColor = Color.FromArgb(107, 114, 128);
            btnBatal.ForeColor = Color.White;
            btnBatal.FlatStyle = FlatStyle.Flat;
            btnBatal.Click    += btnBatal_Click;

            btnReset.Name      = "btnReset";
            btnReset.Text      = "Reset";
            btnReset.Location  = new Point(526, btnY);
            btnReset.Size      = new Size(80, 32);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Click    += btnReset_Click;

            pnlInput.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblPenghuniId,    cmbPenghuni,
                lblKamarId,       cmbKamar,
                lblTanggalMulai,  dtpTanggalMulai,
                lblTanggalSelesai, dtpTanggalSelesai,
                lblHarga,         txtHarga,
                lblDeposit,       txtDeposit,
                lblStatus,        cmbStatus,
                lblCatatan,       txtCatatan,
                btnTambah, btnUpdate, btnHapus, btnSelesai, btnBatal, btnReset
            });

            // DataGridView
            dgvKontrak.Dock                = DockStyle.Fill;
            dgvKontrak.ReadOnly            = true;
            dgvKontrak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKontrak.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvKontrak.AllowUserToAddRows  = false;
            dgvKontrak.BackgroundColor     = Color.White;
            dgvKontrak.BorderStyle         = BorderStyle.None;
            dgvKontrak.RowHeadersVisible   = false;
            dgvKontrak.Font                = new Font("Segoe UI", 9F);
            dgvKontrak.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 43, 59),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            dgvKontrak.EnableHeadersVisualStyles = false;
            dgvKontrak.CellClick += dgvKontrak_CellClick;

            this.Text      = "Manajemen Kontrak Sewa";
            this.Size      = new Size(980, 680);
            this.Font      = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;
            this.Controls.Add(dgvKontrak);
            this.Controls.Add(pnlInput);
        }

        #endregion

        private Panel           pnlInput;
        private Label           lblTitle;
        private Label           lblPenghuniId;
        private ComboBox        cmbPenghuni;
        private Label           lblKamarId;
        private ComboBox        cmbKamar;
        private Label           lblTanggalMulai;
        private DateTimePicker  dtpTanggalMulai;
        private Label           lblTanggalSelesai;
        private DateTimePicker  dtpTanggalSelesai;
        private Label           lblHarga;
        private TextBox         txtHarga;
        private Label           lblDeposit;
        private TextBox         txtDeposit;
        private Label           lblStatus;
        private ComboBox        cmbStatus;
        private Label           lblCatatan;
        private TextBox         txtCatatan;
        private Button          btnTambah;
        private Button          btnUpdate;
        private Button          btnHapus;
        private Button          btnSelesai;
        private Button          btnBatal;
        private Button          btnReset;
        private DataGridView    dgvKontrak;
    }
}
