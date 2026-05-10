using management_kos.Models;
using management_kos.Services;

namespace management_kos.UI
{
    partial class FormKamar
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
            label3 = new Label();   // Kos
            comboBox1 = new ComboBox();
            label1 = new Label();   // Nomor Kamar
            textBox1 = new TextBox();
            label2 = new Label();   // Status
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            btnTambah = new Button();
            btnUpdate = new Button();
            btnHapus = new Button();
            dgvKos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvKos).BeginInit();
            SuspendLayout();

            //pnlInput
            pnlInput.BackColor = Color.FromArgb(245, 247, 250);
            pnlInput.Dock = DockStyle.Top;
            pnlInput.Height = 200;
            pnlInput.Padding = new Padding(16);

            //Title
            lblTitle.Text = "Data Kamar";
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(32, 43, 59);
            lblTitle.Location = new Point(16, 12);
            lblTitle.AutoSize = true;

            //Kos (baris 1)
            int lx = 16, tx = 160, startY = 48, rowH = 37;

            label3.Text = "Kos:"; label3.AutoSize = true;
            label3.Location = new Point(lx, startY + 4);
            label3.Click += label3_Click;

            comboBox1.Location = new Point(tx, startY);
            comboBox1.Size = new Size(160, 24);
            comboBox1.FormattingEnabled = true;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            //Nomor Kamar (baris 2)
            label1.Text = "Nomor Kamar:"; label1.AutoSize = true;
            label1.Location = new Point(lx, startY + rowH + 4);

            textBox1.Location = new Point(tx, startY + rowH);
            textBox1.Size = new Size(200, 24);
            textBox1.TextChanged += textBox1_TextChanged;

            //Status (baris 3)
            label2.Text = "Status:"; label2.AutoSize = true;
            label2.Location = new Point(lx, startY + rowH * 2 + 4);

            radioButton1.Text = "Kosong"; radioButton1.AutoSize = true;
            radioButton1.Location = new Point(tx, startY + rowH * 2 + 2);
            radioButton1.TabStop = true; radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;

            radioButton2.Text = "Terisi"; radioButton2.AutoSize = true;
            radioButton2.Location = new Point(tx + 80, startY + rowH * 2 + 2);
            radioButton2.TabStop = true; radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;

            radioButton3.Text = "Dipesan"; radioButton3.AutoSize = true;
            radioButton3.Location = new Point(tx + 150, startY + rowH * 2 + 2);
            radioButton3.TabStop = true; radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;

            radioButton4.Text = "Perbaiki"; radioButton4.AutoSize = true;
            radioButton4.Location = new Point(tx + 230, startY + rowH * 2 + 2);
            radioButton4.TabStop = true; radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;

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

            //Tambah ke pnlInput
            pnlInput.Controls.AddRange(new Control[]
            {
                lblTitle,
                label3, comboBox1,
                label1, textBox1,
                label2, radioButton1, radioButton2, radioButton3, radioButton4,
                btnTambah, btnUpdate, btnHapus
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
            dgvKos.CellContentClick += dgvKamar_CellContentClick;

            //FormKamar
            this.Text = "Management Kos - Data Kamar";
            this.Size = new Size(860, 580);
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;
            this.Controls.Add(dgvKos);
            this.Controls.Add(pnlInput);
            Load += FormKamar_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKos).EndInit();
            ResumeLayout(false);
        }

        private void dgvKamar_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        #endregion

        private Panel pnlInput;
        private Label lblTitle;
        private Label label3;
        private ComboBox comboBox1;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Button btnTambah;
        private Button btnUpdate;
        private Button btnHapus;
        private DataGridView dgvKos;
    }
}