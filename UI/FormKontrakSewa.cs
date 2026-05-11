using System;
using System.Collections.Generic;
using System.Windows.Forms;
using management_kos.Models;
using management_kos.Services;

namespace management_kos.UI
{
    public partial class FormKontrakSewa : Form
    {
        // Automata: definisi state form kontrak sewa
        private enum FormState { Idle, Selected }

        // Automata: tabel transisi — setiap state menentukan tombol mana yang aktif
        private static readonly Dictionary<FormState, HashSet<string>> EnabledButtons = new()
        {
            [FormState.Idle]     = new HashSet<string> { "btnTambah", "btnReset" },
            [FormState.Selected] = new HashSet<string> { "btnTambah", "btnUpdate", "btnHapus", "btnSelesai", "btnBatal", "btnReset" },
        };

        private readonly KontrakSewaService _service;
        private int _selectedId = 0;

        public FormKontrakSewa(KontrakSewaService service)
        {
            _service = service;
            InitializeComponent();
            this.Load += FormKontrakSewa_Load;
        }

        private void FormKontrakSewa_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new[] { "Aktif", "Selesai", "Dibatalkan" });
            cmbStatus.SelectedIndex = 0;

            ApplyState(FormState.Idle);
            RefreshGrid();
        }

        // Automata: terapkan state baru — enable/disable tombol sesuai tabel transisi
        private void ApplyState(FormState state)
        {
            var enabled = EnabledButtons[state];
            foreach (Control c in pnlInput.Controls)
            {
                if (c is Button btn)
                    btn.Enabled = enabled.Contains(btn.Name);
            }
            if (state == FormState.Idle) _selectedId = 0;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                var k = BuildFromInput();
                _service.TambahKontrak(k);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak berhasil ditambahkan.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            try
            {
                var k = BuildFromInput();
                k.Id = _selectedId;
                _service.UbahKontrak(k);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak berhasil diubah.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            var confirm = MessageBox.Show("Yakin ingin menghapus kontrak ini?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _service.HapusKontrak(_selectedId);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak berhasil dihapus.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            try
            {
                _service.SelesaikanKontrak(_selectedId);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak ditandai Selesai.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            try
            {
                _service.BatalkanKontrak(_selectedId);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak dibatalkan.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dgvKontrak_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKontrak.Rows.Count) return;

            var row = dgvKontrak.Rows[e.RowIndex];

            _selectedId           = Convert.ToInt32(row.Cells["Id"].Value);
            txtPenghuniId.Text    = Convert.ToString(row.Cells["PenghuniId"].Value);
            txtKamarId.Text       = Convert.ToString(row.Cells["KamarId"].Value);
            dtpTanggalMulai.Value = Convert.ToDateTime(row.Cells["TanggalMulai"].Value);
            dtpTanggalSelesai.Value = Convert.ToDateTime(row.Cells["TanggalSelesai"].Value);
            txtHarga.Text         = Convert.ToString(row.Cells["HargaSewaBulanan"].Value);
            txtDeposit.Text       = row.Cells["Deposit"].Value == DBNull.Value || row.Cells["Deposit"].Value == null
                                    ? string.Empty
                                    : Convert.ToString(row.Cells["Deposit"].Value);
            txtCatatan.Text       = Convert.ToString(row.Cells["Catatan"].Value);

            var status = Convert.ToString(row.Cells["Status"].Value) ?? "Aktif";
            int idx = cmbStatus.Items.IndexOf(status);
            cmbStatus.SelectedIndex = idx >= 0 ? idx : 0;

            // Automata: transisi ke state Selected saat baris dipilih
            ApplyState(FormState.Selected);
        }

        private KontrakSewa BuildFromInput()
        {
            if (!int.TryParse(txtPenghuniId.Text.Trim(), out int penghuniId))
                throw new ArgumentException("ID Penghuni harus berupa angka.");
            if (!int.TryParse(txtKamarId.Text.Trim(), out int kamarId))
                throw new ArgumentException("ID Kamar harus berupa angka.");
            if (!decimal.TryParse(txtHarga.Text.Trim(), out decimal harga))
                throw new ArgumentException("Harga Sewa Bulanan harus berupa angka.");

            decimal.TryParse(txtDeposit.Text.Trim(), out decimal deposit);

            return new KontrakSewa
            {
                PenghuniId       = penghuniId,
                KamarId          = kamarId,
                TanggalMulai     = dtpTanggalMulai.Value.Date,
                TanggalSelesai   = dtpTanggalSelesai.Value.Date,
                HargaSewaBulanan = harga,
                Deposit          = string.IsNullOrWhiteSpace(txtDeposit.Text) ? null : deposit,
                Status           = cmbStatus.SelectedItem?.ToString() ?? "Aktif",
                Catatan          = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
            };
        }

        public void RefreshData() => RefreshGrid();

        private void RefreshGrid()
        {
            dgvKontrak.DataSource = null;
            dgvKontrak.DataSource = _service.GetAll();
            if (dgvKontrak.Columns["Catatan"] != null)
                dgvKontrak.Columns["Catatan"].Visible = false;
        }

        private void ClearInput()
        {
            txtPenghuniId.Text      = string.Empty;
            txtKamarId.Text         = string.Empty;
            dtpTanggalMulai.Value   = DateTime.Today;
            dtpTanggalSelesai.Value = DateTime.Today.AddMonths(12);
            txtHarga.Text           = string.Empty;
            txtDeposit.Text         = string.Empty;
            txtCatatan.Text         = string.Empty;
            cmbStatus.SelectedIndex = 0;
            // Automata: kembali ke state Idle setelah reset
            ApplyState(FormState.Idle);
        }
    }
}
