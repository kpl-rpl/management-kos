namespace management_kos.UI
{
    partial class FormPembayaran
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
            lblKontrakSewaId = new Label();
            cmbKontrakSewa = new ComboBox();
            lblJumlahDibayar = new Label();
            txtJumlahDibayar = new TextBox();
            lblMetode = new Label();
            cmbMetodePembayaran = new ComboBox();
            lblTotalTagihan = new Label();
            lblTotalTagihanValue = new Label();
            lblTotalDibayar = new Label();
            lblTotalDibayarValue = new Label();
            lblSisa = new Label();
            lblSisaValue = new Label();
            lblCatatan = new Label();
            txtCatatan = new TextBox();
            txtCari = new TextBox();
            cmbFilterMetode = new ComboBox();
            btnCari = new Button();
            btnTambah = new Button();
            btnBayar = new Button();
            btnUpdate = new Button();
            btnHapus = new Button();
            btnReset = new Button();
            dgvPembayaran = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvPembayaran).BeginInit();
            SuspendLayout();

            //pnlInput
            pnlInput.BackColor = Color.FromArgb(245, 247, 250);
            pnlInput.Dock = DockStyle.Top;
            pnlInput.Height = 280;
            pnlInput.Padding = new Padding(16);

            //Title
            lblTitle.Text = "Pembayaran";
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(32, 43, 59);
            lblTitle.Location = new Point(16, 12);
            lblTitle.AutoSize = true;

            //Input fields (kiri)
            int lx = 16, tx = 200, startY = 48, rowH = 37;

            // Baris 0: Kontrak Sewa (full width kiri)
            lblKontrakSewaId.Text = "Kontrak Sewa:"; lblKontrakSewaId.AutoSize = true;
            lblKontrakSewaId.Location = new Point(lx, startY + 4);
            cmbKontrakSewa.Location = new Point(tx, startY);
            cmbKontrakSewa.Size = new Size(380, 24);
            cmbKontrakSewa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKontrakSewa.SelectedIndexChanged += cmbKontrakSewa_SelectedIndexChanged;

            // Baris 1: Jumlah Dibayar
            lblJumlahDibayar.Text = "Jumlah Dibayar (Rp):"; lblJumlahDibayar.AutoSize = true;
            lblJumlahDibayar.Location = new Point(lx, startY + rowH + 4);
            txtJumlahDibayar.Location = new Point(tx, startY + rowH); txtJumlahDibayar.Size = new Size(160, 24);

            // Baris 2: Metode
            lblMetode.Text = "Metode Pembayaran:"; lblMetode.AutoSize = true;
            lblMetode.Location = new Point(lx, startY + rowH * 2 + 4);
            cmbMetodePembayaran.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodePembayaran.FormattingEnabled = true;
            cmbMetodePembayaran.Location = new Point(tx, startY + rowH * 2);
            cmbMetodePembayaran.Size = new Size(160, 24);

            lblTotalTagihan.Text = "Total Tagihan:";
            lblTotalTagihan.AutoSize = true;
            lblTotalTagihan.Location = new Point(430, startY + rowH + 4);
            lblTotalTagihanValue.Text = "-";
            lblTotalTagihanValue.AutoSize = true;
            lblTotalTagihanValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalTagihanValue.Location = new Point(560, startY + rowH + 4);

            lblTotalDibayar.Text = "Total Dibayar:";
            lblTotalDibayar.AutoSize = true;
            lblTotalDibayar.Location = new Point(430, startY + rowH * 2 + 4);
            lblTotalDibayarValue.Text = "-";
            lblTotalDibayarValue.AutoSize = true;
            lblTotalDibayarValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalDibayarValue.Location = new Point(560, startY + rowH * 2 + 4);

            lblSisa.Text = "Sisa:";
            lblSisa.AutoSize = true;
            lblSisa.Location = new Point(430, startY + rowH * 3 + 4);
            lblSisaValue.Text = "-";
            lblSisaValue.AutoSize = true;
            lblSisaValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSisaValue.Location = new Point(560, startY + rowH * 3 + 4);

            txtCari.Location = new Point(16, 224);
            txtCari.Size = new Size(260, 24);
            txtCari.PlaceholderText = "Cari pembayaran...";

            cmbFilterMetode.Location = new Point(286, 224);
            cmbFilterMetode.Size = new Size(140, 24);
            cmbFilterMetode.DropDownStyle = ComboBoxStyle.DropDownList;

            btnCari.Text = "Cari";
            btnCari.Location = new Point(436, 222);
            btnCari.Size = new Size(80, 28);
            btnCari.FlatStyle = FlatStyle.Flat;
            btnCari.Click += btnCari_Click;

            // Baris 3: Catatan
            lblCatatan.Text = "Catatan:"; lblCatatan.AutoSize = true;
            lblCatatan.Location = new Point(lx, startY + rowH * 3 + 4);
            txtCatatan.Location = new Point(tx, startY + rowH * 3); txtCatatan.Size = new Size(280, 24);

            //Tombol kanan atas berwarna
            int bx = 610, by = startY, bw = 120, bh = 32, bg = 8;

            btnTambah.Text = "Catat Pembayaran";
            btnTambah.Location = new Point(bx, by);
            btnTambah.Size = new Size(150, bh);
            btnTambah.BackColor = Color.FromArgb(37, 99, 235);
            btnTambah.ForeColor = Color.White;
            btnTambah.FlatStyle = FlatStyle.Flat;
            btnTambah.Click += btnTambah_Click;

            btnBayar.Text = "Catat Bayar";
            btnBayar.Location = new Point(bx + bw + bg, by);
            btnBayar.Size = new Size(bw, bh);
            btnBayar.BackColor = Color.FromArgb(22, 163, 74);
            btnBayar.ForeColor = Color.White;
            btnBayar.FlatStyle = FlatStyle.Flat;
            btnBayar.Click += btnBayar_Click;

            btnUpdate.Text = "Update";
            btnUpdate.Location = new Point(bx, by + bh + bg);
            btnUpdate.Size = new Size(bw, bh);
            btnUpdate.BackColor = Color.FromArgb(245, 158, 11);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Click += btnUpdate_Click;

            btnHapus.Text = "Hapus";
            btnHapus.Location = new Point(bx + bw + bg, by + bh + bg);
            btnHapus.Size = new Size(bw, bh);
            btnHapus.BackColor = Color.FromArgb(220, 38, 38);
            btnHapus.ForeColor = Color.White;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.Click += btnHapus_Click;

            btnReset.Text = "Bersihkan";
            btnReset.Location = new Point(bx, by + bh + bg);
            btnReset.Size = new Size(150, bh);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Click += btnReset_Click;

            //Tambah ke pnlInput
            pnlInput.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblKontrakSewaId, cmbKontrakSewa,
                lblJumlahDibayar, txtJumlahDibayar,
                lblMetode,        cmbMetodePembayaran,
                lblTotalTagihan, lblTotalTagihanValue,
                lblTotalDibayar, lblTotalDibayarValue,
                lblSisa, lblSisaValue,
                txtCari, cmbFilterMetode, btnCari,
                lblCatatan,       txtCatatan,
                btnTambah, btnBayar, btnUpdate, btnHapus, btnReset
            });

            //DataGridView
            dgvPembayaran.Dock = DockStyle.Fill;
            dgvPembayaran.ReadOnly = true;
            dgvPembayaran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPembayaran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPembayaran.AllowUserToAddRows = false;
            dgvPembayaran.BackgroundColor = Color.White;
            dgvPembayaran.BorderStyle = BorderStyle.None;
            dgvPembayaran.RowHeadersVisible = false;
            dgvPembayaran.Font = new Font("Segoe UI", 9F);
            dgvPembayaran.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 43, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            dgvPembayaran.EnableHeadersVisualStyles = false;
            dgvPembayaran.CellClick += dgvPembayaran_CellClick;

            //FormPembayaran
            this.Text = "Management Kos - Data Pembayaran";
            this.Size = new Size(1000, 650);
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;
            this.Controls.Add(dgvPembayaran);
            this.Controls.Add(pnlInput);
            Load += FormPembayaran_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPembayaran).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlInput;
        private Label lblTitle;
        private Label lblKontrakSewaId;
        private ComboBox cmbKontrakSewa;
        private Label lblJumlahDibayar;
        private TextBox txtJumlahDibayar;
        private Label lblMetode;
        private ComboBox cmbMetodePembayaran;
        private Label lblTotalTagihan;
        private Label lblTotalTagihanValue;
        private Label lblTotalDibayar;
        private Label lblTotalDibayarValue;
        private Label lblSisa;
        private Label lblSisaValue;
        private Label lblCatatan;
        private TextBox txtCatatan;
        private TextBox txtCari;
        private ComboBox cmbFilterMetode;
        private Button btnCari;
        private Button btnTambah;
        private Button btnBayar;
        private Button btnUpdate;
        private Button btnHapus;
        private Button btnReset;
        private DataGridView dgvPembayaran;
    }
}
