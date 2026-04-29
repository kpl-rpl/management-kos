namespace management_kos.UI
{
	partial class FormPembayaran
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
			pnlInput = new Panel();
			lblTitle = new Label();

			lblKontrakSewaId = new Label();
			txtKontrakSewaId = new TextBox();

			lblPeriode = new Label();
			txtPeriode = new TextBox();

			lblJumlahTagihan = new Label();
			txtJumlahTagihan = new TextBox();

			lblJumlahDibayar = new Label();
			txtJumlahDibayar = new TextBox();

			lblMetode = new Label();
			cmbMetodePembayaran = new ComboBox();

			lblCatatan = new Label();
			txtCatatan = new TextBox();

			btnTambah = new Button();
			btnBayar = new Button();
			btnUpdate = new Button();
			btnHapus = new Button();
			btnReset = new Button();

			dgvPembayaran = new DataGridView();

			pnlInput.BackColor = Color.FromArgb(245, 247, 250);
			pnlInput.Dock = DockStyle.Top;
			pnlInput.Height = 310;
			pnlInput.Padding = new Padding(16);

			lblTitle.Text = "Data Pembayaran";
			lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(32, 43, 59);
			lblTitle.Location = new Point(16, 12);
			lblTitle.AutoSize = true;

			lblKontrakSewaId.Text = "ID Kontrak Sewa:";
			lblKontrakSewaId.Location = new Point(16, 50);
			lblKontrakSewaId.AutoSize = true;

			txtKontrakSewaId.Location = new Point(160, 47);
			txtKontrakSewaId.Size = new Size(120, 24);

			lblPeriode.Text = "Periode (cth: Mei 2025):";
			lblPeriode.Location = new Point(16, 85);
			lblPeriode.AutoSize = true;

			txtPeriode.Location = new Point(200, 82);
			txtPeriode.Size = new Size(160, 24);

			lblJumlahTagihan.Text = "Jumlah Tagihan (Rp):";
			lblJumlahTagihan.Location = new Point(16, 120);
			lblJumlahTagihan.AutoSize = true;

			txtJumlahTagihan.Location = new Point(200, 117);
			txtJumlahTagihan.Size = new Size(160, 24);

			lblJumlahDibayar.Text = "Jumlah Dibayar (Rp):";
			lblJumlahDibayar.Location = new Point(16, 155);
			lblJumlahDibayar.AutoSize = true;

			txtJumlahDibayar.Location = new Point(200, 152);
			txtJumlahDibayar.Size = new Size(160, 24);

			lblMetode.Text = "Metode Pembayaran:";
			lblMetode.Location = new Point(16, 190);
			lblMetode.AutoSize = true;

			cmbMetodePembayaran.Location = new Point(200, 187);
			cmbMetodePembayaran.Size = new Size(160, 24);
			cmbMetodePembayaran.DropDownStyle = ComboBoxStyle.DropDownList;

			lblCatatan.Text = "Catatan:";
			lblCatatan.Location = new Point(16, 225);
			lblCatatan.AutoSize = true;

			txtCatatan.Location = new Point(200, 222);
			txtCatatan.Size = new Size(300, 24);

			int btnY = 262;

			btnTambah.Text = "Tambah Tagihan";
			btnTambah.Location = new Point(16, btnY);
			btnTambah.Size = new Size(130, 32);
			btnTambah.BackColor = Color.FromArgb(37, 99, 235);
			btnTambah.ForeColor = Color.White;
			btnTambah.FlatStyle = FlatStyle.Flat;
			btnTambah.Click += btnTambah_Click;

			btnBayar.Text = "Catat Bayar";
			btnBayar.Location = new Point(156, btnY);
			btnBayar.Size = new Size(120, 32);
			btnBayar.BackColor = Color.FromArgb(22, 163, 74);
			btnBayar.ForeColor = Color.White;
			btnBayar.FlatStyle = FlatStyle.Flat;
			btnBayar.Click += btnBayar_Click;

			btnUpdate.Text = "Update";
			btnUpdate.Location = new Point(286, btnY);
			btnUpdate.Size = new Size(100, 32);
			btnUpdate.BackColor = Color.FromArgb(245, 158, 11);
			btnUpdate.ForeColor = Color.White;
			btnUpdate.FlatStyle = FlatStyle.Flat;
			btnUpdate.Click += btnUpdate_Click;

			btnHapus.Text = "Hapus";
			btnHapus.Location = new Point(396, btnY);
			btnHapus.Size = new Size(90, 32);
			btnHapus.BackColor = Color.FromArgb(220, 38, 38);
			btnHapus.ForeColor = Color.White;
			btnHapus.FlatStyle = FlatStyle.Flat;
			btnHapus.Click += btnHapus_Click;

			btnReset.Text = "Reset";
			btnReset.Location = new Point(496, btnY);
			btnReset.Size = new Size(80, 32);
			btnReset.FlatStyle = FlatStyle.Flat;
			btnReset.Click += btnReset_Click;

			pnlInput.Controls.AddRange(new Control[]
			{
				lblTitle,
				lblKontrakSewaId, txtKontrakSewaId,
				lblPeriode,       txtPeriode,
				lblJumlahTagihan, txtJumlahTagihan,
				lblJumlahDibayar, txtJumlahDibayar,
				lblMetode,        cmbMetodePembayaran,
				lblCatatan,       txtCatatan,
				btnTambah, btnBayar, btnUpdate, btnHapus, btnReset
			});

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

			this.Text = "Manajemen Pembayaran";
			this.Size = new Size(860, 580);
			this.Font = new Font("Segoe UI", 9F);
			this.BackColor = Color.White;
			this.Controls.Add(dgvPembayaran);
			this.Controls.Add(pnlInput);
		}

		#endregion

		private Panel pnlInput;
		private Label lblTitle;
		private Label lblKontrakSewaId;
		private TextBox txtKontrakSewaId;
		private Label lblPeriode;
		private TextBox txtPeriode;
		private Label lblJumlahTagihan;
		private TextBox txtJumlahTagihan;
		private Label lblJumlahDibayar;
		private TextBox txtJumlahDibayar;
		private Label lblMetode;
		private ComboBox cmbMetodePembayaran;
		private Label lblCatatan;
		private TextBox txtCatatan;
		private Button btnTambah;
		private Button btnBayar;
		private Button btnUpdate;
		private Button btnHapus;
		private Button btnReset;
		private DataGridView dgvPembayaran;
	}
}