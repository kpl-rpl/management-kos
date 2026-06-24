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
            lblKosId          = new Label();
            cmbKos            = new ComboBox();
            lblKamarId        = new Label();
            cmbKamar          = new ComboBox();
            lblTanggalMulai   = new Label();
            dtpTanggalMulai   = new DateTimePicker();
            lblTanggalSelesai = new Label();
            txtDurasiBulan    = new TextBox();
            lblNamaPenghuniBaru = new Label();
            txtNamaPenghuniBaru = new TextBox();
            lblTeleponPenghuniBaru = new Label();
            txtTeleponPenghuniBaru = new TextBox();
            lblEmailPenghuniBaru = new Label();
            txtEmailPenghuniBaru = new TextBox();
            lblHarga          = new Label();
            lblHargaValue     = new Label();
            lblDeposit        = new Label();
            txtDeposit        = new TextBox();
            lblStatus         = new Label();
            cmbStatus         = new ComboBox();
            lblMetodePembayaran = new Label();
            cmbMetodePembayaran = new ComboBox();
            lblCatatan        = new Label();
            txtCatatan        = new TextBox();
            btnTambah         = new Button();
            btnUpdate         = new Button();
            btnHapus          = new Button();
            btnSelesai        = new Button();
            btnBatal          = new Button();
            btnPerpanjang     = new Button();
            btnReset          = new Button();
            txtCari           = new TextBox();
            btnCari           = new Button();
            splitData         = new SplitContainer();
            pnlPembayaran     = new Panel();
            lblPembayaranTitle = new Label();
            dgvKontrak        = new DataGridView();
            dgvPembayaran     = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitData).BeginInit();
            splitData.Panel1.SuspendLayout();
            splitData.Panel2.SuspendLayout();
            splitData.SuspendLayout();
            pnlPembayaran.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKontrak).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPembayaran).BeginInit();

            // Panel input
            pnlInput.BackColor = Color.FromArgb(245, 247, 250);
            pnlInput.Dock      = DockStyle.Top;
            pnlInput.Height    = 500;
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

            // Kos
            lblKosId.Text     = "Kos:";
            lblKosId.Location = new Point(16, 82);
            lblKosId.AutoSize = true;
            cmbKos.Location      = new Point(180, 79);
            cmbKos.Size          = new Size(220, 24);
            cmbKos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKos.SelectedIndexChanged += cmbKos_SelectedIndexChanged;

            // Kamar
            lblKamarId.Text     = "Kamar:";
            lblKamarId.Location = new Point(16, 114);
            lblKamarId.AutoSize = true;
            cmbKamar.Location      = new Point(180, 111);
            cmbKamar.Size          = new Size(220, 24);
            cmbKamar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKamar.SelectedIndexChanged += cmbKamar_SelectedIndexChanged;

            // Tanggal Mulai
            lblTanggalMulai.Text     = "Tanggal Mulai:";
            lblTanggalMulai.Location = new Point(16, 146);
            lblTanggalMulai.AutoSize = true;
            dtpTanggalMulai.Location = new Point(180, 143);
            dtpTanggalMulai.Size     = new Size(160, 24);
            dtpTanggalMulai.Format   = DateTimePickerFormat.Short;

            // Tanggal Selesai
            lblTanggalSelesai.Text     = "Durasi (bulan):";
            lblTanggalSelesai.Location = new Point(16, 178);
            lblTanggalSelesai.AutoSize = true;
            txtDurasiBulan.Location = new Point(180, 175);
            txtDurasiBulan.Size     = new Size(160, 24);
            txtDurasiBulan.Text     = "1";

            // Penghuni baru
            lblNamaPenghuniBaru.Text = "Nama Penghuni Baru:";
            lblNamaPenghuniBaru.Location = new Point(430, 50);
            lblNamaPenghuniBaru.AutoSize = true;
            txtNamaPenghuniBaru.Location = new Point(590, 47);
            txtNamaPenghuniBaru.Size = new Size(220, 24);

            lblTeleponPenghuniBaru.Text = "No. Telepon Baru:";
            lblTeleponPenghuniBaru.Location = new Point(430, 82);
            lblTeleponPenghuniBaru.AutoSize = true;
            txtTeleponPenghuniBaru.Location = new Point(590, 79);
            txtTeleponPenghuniBaru.Size = new Size(220, 24);

            lblEmailPenghuniBaru.Text = "Email Baru:";
            lblEmailPenghuniBaru.Location = new Point(430, 114);
            lblEmailPenghuniBaru.AutoSize = true;
            txtEmailPenghuniBaru.Location = new Point(590, 111);
            txtEmailPenghuniBaru.Size = new Size(220, 24);

            // Harga Sewa
            lblHarga.Text     = "Harga Kamar/Bulan (Rp):";
            lblHarga.Location = new Point(16, 210);
            lblHarga.AutoSize = true;
            lblHargaValue.Location = new Point(200, 210);
            lblHargaValue.Size     = new Size(140, 24);
            lblHargaValue.Text     = "-";
            lblHargaValue.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Deposit
            lblDeposit.Text     = "Deposit (Rp, opsional):";
            lblDeposit.Location = new Point(16, 242);
            lblDeposit.AutoSize = true;
            txtDeposit.Location = new Point(200, 239);
            txtDeposit.Size     = new Size(140, 24);

            // Status
            lblStatus.Text     = "Status:";
            lblStatus.Location = new Point(16, 274);
            lblStatus.AutoSize = true;
            cmbStatus.Location       = new Point(200, 271);
            cmbStatus.Size           = new Size(140, 24);
            cmbStatus.DropDownStyle  = ComboBoxStyle.DropDownList;
            cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;

            // Metode Pembayaran
            lblMetodePembayaran.Text     = "Metode Pembayaran:";
            lblMetodePembayaran.Location = new Point(16, 306);
            lblMetodePembayaran.AutoSize = true;
            cmbMetodePembayaran.Location      = new Point(200, 303);
            cmbMetodePembayaran.Size          = new Size(140, 24);
            cmbMetodePembayaran.DropDownStyle = ComboBoxStyle.DropDownList;

            // Catatan
            lblCatatan.Text     = "Catatan:";
            lblCatatan.Location = new Point(16, 338);
            lblCatatan.AutoSize = true;
            txtCatatan.Location = new Point(200, 335);
            txtCatatan.Size     = new Size(300, 24);

            // Tombol-tombol
            int btnY = 372;

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
            btnSelesai.Text      = "Selesai";
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

            btnPerpanjang.Name      = "btnPerpanjang";
            btnPerpanjang.Text      = "Perpanjang";
            btnPerpanjang.Location  = new Point(526, btnY);
            btnPerpanjang.Size      = new Size(100, 32);
            btnPerpanjang.BackColor = Color.FromArgb(14, 116, 144);
            btnPerpanjang.ForeColor = Color.White;
            btnPerpanjang.FlatStyle = FlatStyle.Flat;
            btnPerpanjang.Click    += btnPerpanjang_Click;

            btnReset.Name      = "btnReset";
            btnReset.Text      = "Reset";
            btnReset.Location  = new Point(636, btnY);
            btnReset.Size      = new Size(80, 32);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Click    += btnReset_Click;

            txtCari.Location = new Point(16, 450);
            txtCari.Size = new Size(300, 24);
            txtCari.PlaceholderText = "Cari kontrak...";

            btnCari.Name = "btnCari";
            btnCari.Text = "Cari";
            btnCari.Location = new Point(326, 448);
            btnCari.Size = new Size(80, 28);
            btnCari.FlatStyle = FlatStyle.Flat;
            btnCari.Click += btnCari_Click;

            pnlInput.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblPenghuniId,    cmbPenghuni,
                lblKosId,         cmbKos,
                lblKamarId,       cmbKamar,
                lblTanggalMulai,  dtpTanggalMulai,
                lblTanggalSelesai, txtDurasiBulan,
                lblNamaPenghuniBaru, txtNamaPenghuniBaru,
                lblTeleponPenghuniBaru, txtTeleponPenghuniBaru,
                lblEmailPenghuniBaru, txtEmailPenghuniBaru,
                lblHarga,         lblHargaValue,
                lblDeposit,       txtDeposit,
                lblStatus,        cmbStatus,
                lblMetodePembayaran, cmbMetodePembayaran,
                lblCatatan,       txtCatatan,
                btnTambah, btnUpdate, btnHapus, btnSelesai, btnBatal, btnPerpanjang, btnReset,
                txtCari, btnCari
            });

            // Split data
            splitData.Dock = DockStyle.Fill;
            splitData.Orientation = Orientation.Horizontal;
            splitData.SplitterDistance = 310;
            splitData.Panel1.Controls.Add(dgvKontrak);
            splitData.Panel2.Controls.Add(dgvPembayaran);
            splitData.Panel2.Controls.Add(pnlPembayaran);

            // DataGridView Kontrak
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

            // Header pembayaran
            pnlPembayaran.Dock = DockStyle.Top;
            pnlPembayaran.Height = 36;
            pnlPembayaran.BackColor = Color.FromArgb(240, 244, 248);
            lblPembayaranTitle.Text = "Riwayat Pembayaran";
            lblPembayaranTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPembayaranTitle.ForeColor = Color.FromArgb(32, 43, 59);
            lblPembayaranTitle.Location = new Point(12, 8);
            lblPembayaranTitle.AutoSize = true;
            pnlPembayaran.Controls.Add(lblPembayaranTitle);

            // DataGridView Pembayaran
            dgvPembayaran.Dock                = DockStyle.Fill;
            dgvPembayaran.ReadOnly            = true;
            dgvPembayaran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPembayaran.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvPembayaran.AllowUserToAddRows  = false;
            dgvPembayaran.BackgroundColor     = Color.White;
            dgvPembayaran.BorderStyle         = BorderStyle.None;
            dgvPembayaran.RowHeadersVisible   = false;
            dgvPembayaran.Font                = new Font("Segoe UI", 9F);
            dgvPembayaran.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 43, 59),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            dgvPembayaran.EnableHeadersVisualStyles = false;

            this.Text      = "Manajemen Kontrak Sewa";
            this.Size      = new Size(980, 680);
            this.Font      = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;
            this.Controls.Add(splitData);
            this.Controls.Add(pnlInput);
            ((System.ComponentModel.ISupportInitialize)dgvPembayaran).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKontrak).EndInit();
            pnlPembayaran.ResumeLayout(false);
            pnlPembayaran.PerformLayout();
            splitData.Panel2.ResumeLayout(false);
            splitData.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitData).EndInit();
            splitData.ResumeLayout(false);
        }

        #endregion

        private Panel           pnlInput;
        private Label           lblTitle;
        private Label           lblPenghuniId;
        private ComboBox        cmbPenghuni;
        private Label           lblKosId;
        private ComboBox        cmbKos;
        private Label           lblKamarId;
        private ComboBox        cmbKamar;
        private Label           lblTanggalMulai;
        private DateTimePicker  dtpTanggalMulai;
        private Label           lblTanggalSelesai;
        private TextBox         txtDurasiBulan;
        private Label           lblNamaPenghuniBaru;
        private TextBox         txtNamaPenghuniBaru;
        private Label           lblTeleponPenghuniBaru;
        private TextBox         txtTeleponPenghuniBaru;
        private Label           lblEmailPenghuniBaru;
        private TextBox         txtEmailPenghuniBaru;
        private Label           lblHarga;
        private Label           lblHargaValue;
        private Label           lblDeposit;
        private TextBox         txtDeposit;
        private Label           lblStatus;
        private ComboBox        cmbStatus;
        private Label           lblMetodePembayaran;
        private ComboBox        cmbMetodePembayaran;
        private Label           lblCatatan;
        private TextBox         txtCatatan;
        private Button          btnTambah;
        private Button          btnUpdate;
        private Button          btnHapus;
        private Button          btnSelesai;
        private Button          btnBatal;
        private Button          btnPerpanjang;
        private Button          btnReset;
        private TextBox         txtCari;
        private Button          btnCari;
        private SplitContainer  splitData;
        private Panel           pnlPembayaran;
        private Label           lblPembayaranTitle;
        private DataGridView    dgvKontrak;
        private DataGridView    dgvPembayaran;
    }
}
