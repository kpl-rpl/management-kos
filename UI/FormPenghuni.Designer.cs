namespace management_kos.UI
{
    partial class FormPenghuni
    {
        private System.ComponentModel.IContainer components = null;

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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            dtpTanggalMasuk = new DateTimePicker();
            txtNama = new TextBox();
            txtTelpon = new TextBox();
            txtEmail = new TextBox();
            dropDownKamar = new ComboBox();
            btnTambah = new Button();
            btnUpdate = new Button();
            btnHapus = new Button();
            btnReset = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            //pnlInput
            pnlInput.BackColor = Color.FromArgb(245, 247, 250);
            pnlInput.Dock = DockStyle.Top;
            pnlInput.Height = 265;
            pnlInput.Padding = new Padding(16);

            //Title
            lblTitle.Text = "Data Penghuni";
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(32, 43, 59);
            lblTitle.Location = new Point(16, 12);
            lblTitle.AutoSize = true;

            //Input fields
            int lx = 16, tx = 160, startY = 48, rowH = 37;

            label1.Text = "Nama:"; label1.AutoSize = true;
            label1.Location = new Point(lx, startY + 4);
            txtNama.Location = new Point(tx, startY); txtNama.Size = new Size(220, 24);

            label2.Text = "No. Telepon:"; label2.AutoSize = true;
            label2.Location = new Point(lx, startY + rowH + 4);
            txtTelpon.Location = new Point(tx, startY + rowH); txtTelpon.Size = new Size(220, 24);

            label3.Text = "Email (opsional):"; label3.AutoSize = true;
            label3.Location = new Point(lx, startY + rowH * 2 + 4);
            txtEmail.Location = new Point(tx, startY + rowH * 2); txtEmail.Size = new Size(220, 24);

            label4.Text = "Kamar:"; label4.AutoSize = true;
            label4.Location = new Point(lx, startY + rowH * 3 + 4);
            dropDownKamar.Location = new Point(tx, startY + rowH * 3);
            dropDownKamar.Size = new Size(220, 24);
            dropDownKamar.FormattingEnabled = true;
            dropDownKamar.DisplayMember = "DisplayText";
            dropDownKamar.ValueMember = "Id";

            label5.Text = "Tanggal Masuk:"; label5.AutoSize = true;
            label5.Location = new Point(lx, startY + rowH * 4 + 4);
            dtpTanggalMasuk.Location = new Point(tx, startY + rowH * 4);
            dtpTanggalMasuk.Size = new Size(160, 24);
            dtpTanggalMasuk.Format = DateTimePickerFormat.Short;

            //Tombol kanan atas berwarna
            int bx = 430, by = startY, bw = 100, bh = 30, bg = 8;

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
                label1, txtNama,
                label2, txtTelpon,
                label3, txtEmail,
                label4, dropDownKamar,
                label5, dtpTanggalMasuk,
                btnTambah, btnUpdate, btnHapus, btnReset
            });

            //DataGridView
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Font = new Font("Segoe UI", 9F);
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 43, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            dataGridView1.EnableHeadersVisualStyles = false;

            //FormPenghuni
            this.Text = "Management Kos - Data Penghuni";
            this.Size = new Size(860, 580);
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;
            this.Controls.Add(dataGridView1);
            this.Controls.Add(pnlInput);
            Load += FormPenghuni_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlInput;
        private Label lblTitle;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtNama;
        private TextBox txtTelpon;
        private TextBox txtEmail;
        private ComboBox dropDownKamar;
        private DateTimePicker dtpTanggalMasuk;
        private Button btnTambah;
        private Button btnUpdate;
        private Button btnHapus;
        private Button btnReset;
        private DataGridView dataGridView1;
    }
}