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
        lblNamaKos.Location = new Point(23, 27);
        lblNamaKos.Name = "lblNamaKos";
        lblNamaKos.Size = new Size(77, 20);
        lblNamaKos.TabIndex = 0;
        lblNamaKos.Text = "Nama Kos";
        // 
        // txtNamaKos
        // 
        txtNamaKos.Location = new Point(160, 23);
        txtNamaKos.Margin = new Padding(3, 4, 3, 4);
        txtNamaKos.Name = "txtNamaKos";
        txtNamaKos.Size = new Size(297, 27);
        txtNamaKos.TabIndex = 1;
        // 
        // lblAlamat
        // 
        lblAlamat.AutoSize = true;
        lblAlamat.Location = new Point(23, 69);
        lblAlamat.Name = "lblAlamat";
        lblAlamat.Size = new Size(57, 20);
        lblAlamat.TabIndex = 2;
        lblAlamat.Text = "Alamat";
        // 
        // txtAlamat
        // 
        txtAlamat.Location = new Point(160, 65);
        txtAlamat.Margin = new Padding(3, 4, 3, 4);
        txtAlamat.Name = "txtAlamat";
        txtAlamat.Size = new Size(297, 27);
        txtAlamat.TabIndex = 3;
        // 
        // lblHargaDasar
        // 
        lblHargaDasar.AutoSize = true;
        lblHargaDasar.Location = new Point(23, 112);
        lblHargaDasar.Name = "lblHargaDasar";
        lblHargaDasar.Size = new Size(92, 20);
        lblHargaDasar.TabIndex = 4;
        lblHargaDasar.Text = "Harga Dasar";
        // 
        // txtHargaDasar
        // 
        txtHargaDasar.Location = new Point(160, 108);
        txtHargaDasar.Margin = new Padding(3, 4, 3, 4);
        txtHargaDasar.Name = "txtHargaDasar";
        txtHargaDasar.Size = new Size(297, 27);
        txtHargaDasar.TabIndex = 5;
        // 
        // lblJumlahKamar
        // 
        lblJumlahKamar.AutoSize = true;
        lblJumlahKamar.Location = new Point(23, 155);
        lblJumlahKamar.Name = "lblJumlahKamar";
        lblJumlahKamar.Size = new Size(102, 20);
        lblJumlahKamar.TabIndex = 6;
        lblJumlahKamar.Text = "Jumlah Kamar";
        // 
        // txtJumlahKamar
        // 
        txtJumlahKamar.Location = new Point(160, 151);
        txtJumlahKamar.Margin = new Padding(3, 4, 3, 4);
        txtJumlahKamar.Name = "txtJumlahKamar";
        txtJumlahKamar.Size = new Size(297, 27);
        txtJumlahKamar.TabIndex = 7;
        // 
        // lblNamaPemilik
        // 
        lblNamaPemilik.AutoSize = true;
        lblNamaPemilik.Location = new Point(23, 197);
        lblNamaPemilik.Name = "lblNamaPemilik";
        lblNamaPemilik.Size = new Size(100, 20);
        lblNamaPemilik.TabIndex = 8;
        lblNamaPemilik.Text = "Nama Pemilik";
        // 
        // txtNamaPemilik
        // 
        txtNamaPemilik.Location = new Point(160, 193);
        txtNamaPemilik.Margin = new Padding(3, 4, 3, 4);
        txtNamaPemilik.Name = "txtNamaPemilik";
        txtNamaPemilik.Size = new Size(297, 27);
        txtNamaPemilik.TabIndex = 9;
        // 
        // lblNomorTelepon
        // 
        lblNomorTelepon.AutoSize = true;
        lblNomorTelepon.Location = new Point(23, 240);
        lblNomorTelepon.Name = "lblNomorTelepon";
        lblNomorTelepon.Size = new Size(113, 20);
        lblNomorTelepon.TabIndex = 10;
        lblNomorTelepon.Text = "Nomor Telepon";
        // 
        // txtNomorTelepon
        // 
        txtNomorTelepon.Location = new Point(160, 236);
        txtNomorTelepon.Margin = new Padding(3, 4, 3, 4);
        txtNomorTelepon.Name = "txtNomorTelepon";
        txtNomorTelepon.Size = new Size(297, 27);
        txtNomorTelepon.TabIndex = 11;
        // 
        // lblCatatan
        // 
        lblCatatan.AutoSize = true;
        lblCatatan.Location = new Point(23, 283);
        lblCatatan.Name = "lblCatatan";
        lblCatatan.Size = new Size(60, 20);
        lblCatatan.TabIndex = 12;
        lblCatatan.Text = "Catatan";
        // 
        // txtCatatan
        // 
        txtCatatan.Location = new Point(160, 279);
        txtCatatan.Margin = new Padding(3, 4, 3, 4);
        txtCatatan.Name = "txtCatatan";
        txtCatatan.Size = new Size(297, 27);
        txtCatatan.TabIndex = 13;
        // 
        // btnTambah
        // 
        btnTambah.Location = new Point(491, 23);
        btnTambah.Margin = new Padding(3, 4, 3, 4);
        btnTambah.Name = "btnTambah";
        btnTambah.Size = new Size(114, 40);
        btnTambah.TabIndex = 14;
        btnTambah.Text = "Tambah";
        btnTambah.UseVisualStyleBackColor = true;
        btnTambah.Click += btnTambah_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.Location = new Point(617, 23);
        btnUpdate.Margin = new Padding(3, 4, 3, 4);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(114, 40);
        btnUpdate.TabIndex = 15;
        btnUpdate.Text = "Update";
        btnUpdate.UseVisualStyleBackColor = true;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnHapus
        // 
        btnHapus.Location = new Point(743, 23);
        btnHapus.Margin = new Padding(3, 4, 3, 4);
        btnHapus.Name = "btnHapus";
        btnHapus.Size = new Size(114, 40);
        btnHapus.TabIndex = 16;
        btnHapus.Text = "Hapus";
        btnHapus.UseVisualStyleBackColor = true;
        btnHapus.Click += btnHapus_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(491, 71);
        btnReset.Margin = new Padding(3, 4, 3, 4);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(114, 40);
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
        dgvKos.Location = new Point(23, 333);
        dgvKos.Margin = new Padding(3, 4, 3, 4);
        dgvKos.MultiSelect = false;
        dgvKos.Name = "dgvKos";
        dgvKos.ReadOnly = true;
        dgvKos.RowHeadersWidth = 51;
        dgvKos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKos.Size = new Size(834, 293);
        dgvKos.TabIndex = 18;
        dgvKos.CellClick += dgvKos_CellClick;
        // 
        // FormKos
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(891, 653);
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
        Margin = new Padding(3, 4, 3, 4);
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
