using System;
using System.Collections.Generic;
using System.Windows.Forms;
using management_kos.Services;
using management_kos.Models;

namespace management_kos.UI
{
    public partial class FormPembayaran : Form
    {
        private readonly PembayaranService _pembayaranService;
        private readonly KontrakSewaService _kontrakSewaService;
        private int _selectedPembayaranId = 0;

        public FormPembayaran(PembayaranService pembayaranService, KontrakSewaService kontrakSewaService)
        {
            _pembayaranService = pembayaranService;
            _kontrakSewaService = kontrakSewaService;
            InitializeComponent();
            this.Load += FormPembayaran_Load;
        }

        private void FormPembayaran_Load(object sender, EventArgs e)
        {
            cmbMetodePembayaran.Items.AddRange(new[] { "Transfer", "Tunai", "QRIS" });
            cmbMetodePembayaran.SelectedIndex = 0;

            LoadKontrakDropdown();
            RefreshGrid();
        }

        private void LoadKontrakDropdown()
        {
            var list = _kontrakSewaService.GetAll();
            cmbKontrakSewa.DataSource = list;
            cmbKontrakSewa.DisplayMember = "DisplayText";
            cmbKontrakSewa.ValueMember = "Id";
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                var pembayaran = BuildPembayaranFromInput();
                _pembayaranService.CatatPembayaran(pembayaran);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Pembayaran berhasil dicatat.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            if (_selectedPembayaranId <= 0)
            {
                MessageBox.Show("Pilih data pembayaran yang akan diubah.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtJumlahDibayar.Text, out decimal nominal) || nominal <= 0)
            {
                MessageBox.Show("Masukkan jumlah pembayaran yang valid.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var metode = cmbMetodePembayaran.SelectedItem?.ToString() ?? "Transfer";

            try
            {
                var pembayaran = BuildPembayaranFromInput();
                pembayaran.Id = _selectedPembayaranId;
                pembayaran.JumlahDibayar = nominal;
                pembayaran.MetodePembayaran = metode;
                _pembayaranService.UbahPembayaran(pembayaran);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Pembayaran berhasil dicatat.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPembayaranId <= 0)
            {
                MessageBox.Show("Pilih data pembayaran yang akan diubah.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var pembayaran = BuildPembayaranFromInput();
                pembayaran.Id = _selectedPembayaranId;
                _pembayaranService.UbahPembayaran(pembayaran);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Data pembayaran berhasil diubah.", "Informasi",
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
            if (_selectedPembayaranId <= 0)
            {
                MessageBox.Show("Pilih data pembayaran yang akan dihapus.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus data pembayaran ini?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _pembayaranService.HapusPembayaran(_selectedPembayaranId);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Data pembayaran berhasil dihapus.", "Informasi",
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

        private void dgvPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPembayaran.Rows.Count) return;

            var row = dgvPembayaran.Rows[e.RowIndex];

            _selectedPembayaranId = Convert.ToInt32(row.Cells["Id"].Value);

            var kontrakId = Convert.ToInt32(row.Cells["KontrakSewaId"].Value);
            cmbKontrakSewa.SelectedValue = kontrakId;

            txtJumlahDibayar.Text = Convert.ToString(row.Cells["JumlahDibayar"].Value);
            txtCatatan.Text = Convert.ToString(row.Cells["Catatan"].Value);

            var metode = Convert.ToString(row.Cells["MetodePembayaran"].Value);
            int idx = cmbMetodePembayaran.Items.IndexOf(metode);
            cmbMetodePembayaran.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private Pembayaran BuildPembayaranFromInput()
        {
            if (cmbKontrakSewa.SelectedValue is not int kontrakId || kontrakId <= 0)
                throw new ArgumentException("Kontrak Sewa harus dipilih.");

            if (!decimal.TryParse(txtJumlahDibayar.Text.Trim(), out decimal dibayar))
                throw new ArgumentException("Jumlah pembayaran harus berupa angka.");

            return new Pembayaran
            {
                KontrakSewaId = kontrakId,
                TanggalBayar = DateTime.Today,
                JumlahDibayar = dibayar,
                MetodePembayaran = cmbMetodePembayaran.SelectedItem?.ToString() ?? "Transfer",
                Catatan = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
            };
        }

        public void RefreshData()
        {
            LoadKontrakDropdown();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var data = _pembayaranService.GetAll();
            dgvPembayaran.DataSource = null;
            dgvPembayaran.DataSource = data;

            if (dgvPembayaran.Columns["Catatan"] != null)
                dgvPembayaran.Columns["Catatan"].Visible = false;
        }

        private void ClearInput()
        {
            _selectedPembayaranId = 0;
            if (cmbKontrakSewa.Items.Count > 0) cmbKontrakSewa.SelectedIndex = 0;
            txtJumlahDibayar.Text = string.Empty;
            txtCatatan.Text = string.Empty;
            cmbMetodePembayaran.SelectedIndex = 0;
        }
    }
}
