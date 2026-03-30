namespace management_kos.UI;

partial class FormKos
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
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
        // 
        // lblNamaKos
        // 
        lblNamaKos.AutoSize = true;
        lblNamaKos.Location = new Point(20, 20);
        lblNamaKos.Name = "lblNamaKos";
        lblNamaKos.Size = new Size(62, 15);
        lblNamaKos.TabIndex = 0;
        lblNamaKos.Text = "Nama Kos";
        // 
        // txtNamaKos
        // 
        txtNamaKos.Location = new Point(140, 17);
        txtNamaKos.Name = "txtNamaKos";
        txtNamaKos.Size = new Size(260, 23);
        txtNamaKos.TabIndex = 1;
        // 
        // lblAlamat
        // 
        lblAlamat.AutoSize = true;
        lblAlamat.Location = new Point(20, 52);
        lblAlamat.Name = "lblAlamat";
        lblAlamat.Size = new Size(45, 15);
        lblAlamat.TabIndex = 2;
        lblAlamat.Text = "Alamat";
        // 
        // txtAlamat
        // 
        txtAlamat.Location = new Point(140, 49);
        txtAlamat.Name = "txtAlamat";
        txtAlamat.Size = new Size(260, 23);
        txtAlamat.TabIndex = 3;
        // 
        // lblHargaDasar
        // 
        lblHargaDasar.AutoSize = true;
        lblHargaDasar.Location = new Point(20, 84);
        lblHargaDasar.Name = "lblHargaDasar";
        lblHargaDasar.Size = new Size(71, 15);
        lblHargaDasar.TabIndex = 4;
        lblHargaDasar.Text = "Harga Dasar";
        // 
        // txtHargaDasar
        // 
        txtHargaDasar.Location = new Point(140, 81);
        txtHargaDasar.Name = "txtHargaDasar";
        txtHargaDasar.Size = new Size(260, 23);
        txtHargaDasar.TabIndex = 5;
        // 
        // lblJumlahKamar
        // 
        lblJumlahKamar.AutoSize = true;
        lblJumlahKamar.Location = new Point(20, 116);
        lblJumlahKamar.Name = "lblJumlahKamar";
        lblJumlahKamar.Size = new Size(83, 15);
        lblJumlahKamar.TabIndex = 6;
        lblJumlahKamar.Text = "Jumlah Kamar";
        // 
        // txtJumlahKamar
        // 
        txtJumlahKamar.Location = new Point(140, 113);
        txtJumlahKamar.Name = "txtJumlahKamar";
        txtJumlahKamar.Size = new Size(260, 23);
        txtJumlahKamar.TabIndex = 7;
        // 
        // lblNamaPemilik
        // 
        lblNamaPemilik.AutoSize = true;
        lblNamaPemilik.Location = new Point(20, 148);
        lblNamaPemilik.Name = "lblNamaPemilik";
        lblNamaPemilik.Size = new Size(82, 15);
        lblNamaPemilik.TabIndex = 8;
        lblNamaPemilik.Text = "Nama Pemilik";
        // 
        // txtNamaPemilik
        // 
        txtNamaPemilik.Location = new Point(140, 145);
        txtNamaPemilik.Name = "txtNamaPemilik";
        txtNamaPemilik.Size = new Size(260, 23);
        txtNamaPemilik.TabIndex = 9;
        // 
        // lblNomorTelepon
        // 
        lblNomorTelepon.AutoSize = true;
        lblNomorTelepon.Location = new Point(20, 180);
        lblNomorTelepon.Name = "lblNomorTelepon";
        lblNomorTelepon.Size = new Size(87, 15);
        lblNomorTelepon.TabIndex = 10;
        lblNomorTelepon.Text = "Nomor Telepon";
        // 
        // txtNomorTelepon
        // 
        txtNomorTelepon.Location = new Point(140, 177);
        txtNomorTelepon.Name = "txtNomorTelepon";
        txtNomorTelepon.Size = new Size(260, 23);
        txtNomorTelepon.TabIndex = 11;
        // 
        // lblCatatan
        // 
        lblCatatan.AutoSize = true;
        lblCatatan.Location = new Point(20, 212);
        lblCatatan.Name = "lblCatatan";
        lblCatatan.Size = new Size(48, 15);
        lblCatatan.TabIndex = 12;
        lblCatatan.Text = "Catatan";
        // 
        // txtCatatan
        // 
        txtCatatan.Location = new Point(140, 209);
        txtCatatan.Name = "txtCatatan";
        txtCatatan.Size = new Size(260, 23);
        txtCatatan.TabIndex = 13;
        // 
        // btnTambah
        // 
        btnTambah.Location = new Point(430, 17);
        btnTambah.Name = "btnTambah";
        btnTambah.Size = new Size(100, 30);
        btnTambah.TabIndex = 14;
        btnTambah.Text = "Tambah";
        btnTambah.UseVisualStyleBackColor = true;
        btnTambah.Click += btnTambah_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.Location = new Point(540, 17);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(100, 30);
        btnUpdate.TabIndex = 15;
        btnUpdate.Text = "Update";
        btnUpdate.UseVisualStyleBackColor = true;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnHapus
        // 
        btnHapus.Location = new Point(650, 17);
        btnHapus.Name = "btnHapus";
        btnHapus.Size = new Size(100, 30);
        btnHapus.TabIndex = 16;
        btnHapus.Text = "Hapus";
        btnHapus.UseVisualStyleBackColor = true;
        btnHapus.Click += btnHapus_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(430, 53);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(100, 30);
        btnReset.TabIndex = 17;
        btnReset.Text = "Reset Form";
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += btnReset_Click;
        // 
        // dgvKos
        // 
        dgvKos.AllowUserToAddRows = false;
        dgvKos.AllowUserToDeleteRows = false;
        dgvKos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvKos.Location = new Point(20, 250);
        dgvKos.MultiSelect = false;
        dgvKos.Name = "dgvKos";
        dgvKos.ReadOnly = true;
        dgvKos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKos.Size = new Size(730, 220);
        dgvKos.TabIndex = 18;
        dgvKos.CellClick += dgvKos_CellClick;
        // 
        // FormKos
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(780, 490);
        Controls.Add(dgvKos);
        Controls.Add(btnReset);
        Controls.Add(btnHapus);
        Controls.Add(btnUpdate);
        Controls.Add(btnTambah);
        Controls.Add(txtCatatan);
        Controls.Add(lblCatatan);
        Controls.Add(txtNomorTelepon);
        Controls.Add(lblNomorTelepon);
        Controls.Add(txtNamaPemilik);
        Controls.Add(lblNamaPemilik);
        Controls.Add(txtJumlahKamar);
        Controls.Add(lblJumlahKamar);
        Controls.Add(txtHargaDasar);
        Controls.Add(lblHargaDasar);
        Controls.Add(txtAlamat);
        Controls.Add(lblAlamat);
        Controls.Add(txtNamaKos);
        Controls.Add(lblNamaKos);
        Name = "FormKos";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Management Kos - Data Kos";
        Load += FormKos_Load;
        ((System.ComponentModel.ISupportInitialize)dgvKos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

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
