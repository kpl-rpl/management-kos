using management_kos.Models;
using management_kos.Services;

namespace management_kos.UI;

public partial class FormKos : Form
{
    private readonly KosService _kosService;
    private int _selectedKosId;

    public FormKos(KosService kosService)
    {
        _kosService = kosService;
        InitializeComponent();
    }

    private void FormKos_Load(object sender, EventArgs e)
    {
        RefreshGrid();
    }

    private void btnTambah_Click(object sender, EventArgs e)
    {
        try
        {
            var kos = BuildKosFromInput();
            _kosService.TambahKos(kos);
            RefreshGrid();
            ClearInput();

            MessageBox.Show("Data kos berhasil ditambahkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedKosId <= 0)
        {
            MessageBox.Show("Pilih data kos yang akan diubah.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var kos = BuildKosFromInput();
            kos.Id = _selectedKosId;

            _kosService.UbahKos(kos);
            RefreshGrid();
            ClearInput();

            MessageBox.Show("Data kos berhasil diubah.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnHapus_Click(object sender, EventArgs e)
    {
        if (_selectedKosId <= 0)
        {
            MessageBox.Show("Pilih data kos yang akan dihapus.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show("Yakin ingin menghapus data kos ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes)
        {
            return;
        }

        _kosService.HapusKos(_selectedKosId);
        RefreshGrid();
        ClearInput();

        MessageBox.Show("Data kos berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        ClearInput();
    }

    private void dgvKos_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvKos.Rows.Count)
        {
            return;
        }

        var row = dgvKos.Rows[e.RowIndex];
        _selectedKosId = Convert.ToInt32(row.Cells[nameof(Kos.Id)].Value);

        txtNamaKos.Text = Convert.ToString(row.Cells[nameof(Kos.NamaKos)].Value);
        txtAlamat.Text = Convert.ToString(row.Cells[nameof(Kos.Alamat)].Value);
        txtHargaDasar.Text = Convert.ToString(row.Cells[nameof(Kos.HargaDasar)].Value);
        txtJumlahKamar.Text = Convert.ToString(row.Cells[nameof(Kos.JumlahKamar)].Value);
        txtNamaPemilik.Text = Convert.ToString(row.Cells[nameof(Kos.NamaPemilik)].Value);
        txtNomorTelepon.Text = Convert.ToString(row.Cells[nameof(Kos.NomorTelepon)].Value);
        txtCatatan.Text = Convert.ToString(row.Cells[nameof(Kos.Catatan)].Value);
    }

    private void RefreshGrid()
    {
        var items = _kosService.GetAllKos();
        dgvKos.DataSource = null;
        dgvKos.DataSource = items;

        if (dgvKos.Columns.Count > 0)
        {
            dgvKos.Columns[nameof(Kos.Id)].HeaderText = "ID";
            dgvKos.Columns[nameof(Kos.NamaKos)].HeaderText = "Nama Kos";
            dgvKos.Columns[nameof(Kos.HargaDasar)].HeaderText = "Harga Dasar";
            dgvKos.Columns[nameof(Kos.JumlahKamar)].HeaderText = "Jumlah Kamar";
            dgvKos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }

    private Kos BuildKosFromInput()
    {
        if (!decimal.TryParse(txtHargaDasar.Text, out var hargaDasar))
        {
            throw new ArgumentException("Harga Dasar harus berupa angka.");
        }

        if (!int.TryParse(txtJumlahKamar.Text, out var jumlahKamar))
        {
            throw new ArgumentException("Jumlah Kamar harus berupa angka bulat.");
        }

        return new Kos
        {
            NamaKos = txtNamaKos.Text.Trim(),
            Alamat = txtAlamat.Text.Trim(),
            HargaDasar = hargaDasar,
            JumlahKamar = jumlahKamar,
            NamaPemilik = txtNamaPemilik.Text.Trim(),
            NomorTelepon = txtNomorTelepon.Text.Trim(),
            Catatan = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
        };
    }

    private void ClearInput()
    {
        _selectedKosId = 0;
        txtNamaKos.Clear();
        txtAlamat.Clear();
        txtHargaDasar.Clear();
        txtJumlahKamar.Clear();
        txtNamaPemilik.Clear();
        txtNomorTelepon.Clear();
        txtCatatan.Clear();
        txtNamaKos.Focus();
    }

    private void dgvKos_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
