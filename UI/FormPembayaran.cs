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
        private int _selectedPembayaranId = 0;

        public FormPembayaran(PembayaranService pembayaranService)
        {
            _pembayaranService = pembayaranService;
            InitializeComponent();
            this.Load += FormPembayaran_Load;
        }

        private void FormPembayaran_Load(object sender, EventArgs e)
        {
            cmbMetodePembayaran.Items.AddRange(new[] { "Transfer", "Tunai", "QRIS" });
            cmbMetodePembayaran.SelectedIndex = 0;

            RefreshGrid();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                var pembayaran = BuildPembayaranFromInput();
                _pembayaranService.TambahTagihan(pembayaran);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Tagihan berhasil ditambahkan.", "Informasi",
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
                MessageBox.Show("Pilih tagihan yang akan dibayar.", "Validasi",
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
                _pembayaranService.BayarTagihan(_selectedPembayaranId, nominal, metode);
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
            txtKontrakSewaId.Text = Convert.ToString(row.Cells["KontrakSewaId"].Value);
            txtPeriode.Text = Convert.ToString(row.Cells["Periode"].Value);
            txtJumlahTagihan.Text = Convert.ToString(row.Cells["JumlahTagihan"].Value);
            txtJumlahDibayar.Text = Convert.ToString(row.Cells["JumlahDibayar"].Value);
            txtCatatan.Text = Convert.ToString(row.Cells["Catatan"].Value);

            var metode = Convert.ToString(row.Cells["MetodePembayaran"].Value);
            int idx = cmbMetodePembayaran.Items.IndexOf(metode);
            cmbMetodePembayaran.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private Pembayaran BuildPembayaranFromInput()
        {
            if (!int.TryParse(txtKontrakSewaId.Text.Trim(), out int kontrakId))
                throw new ArgumentException("ID Kontrak Sewa harus berupa angka.");

            if (!decimal.TryParse(txtJumlahTagihan.Text.Trim(), out decimal tagihan))
                throw new ArgumentException("Jumlah tagihan harus berupa angka.");

            decimal.TryParse(txtJumlahDibayar.Text.Trim(), out decimal dibayar);

            return new Pembayaran
            {
                KontrakSewaId = kontrakId,
                Periode = txtPeriode.Text.Trim(),
                JumlahTagihan = tagihan,
                JumlahDibayar = dibayar,
                MetodePembayaran = cmbMetodePembayaran.SelectedItem?.ToString() ?? "Transfer",
                Catatan = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
            };
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
            txtKontrakSewaId.Text = string.Empty;
            txtPeriode.Text = string.Empty;
            txtJumlahTagihan.Text = string.Empty;
            txtJumlahDibayar.Text = string.Empty;
            txtCatatan.Text = string.Empty;
            cmbMetodePembayaran.SelectedIndex = 0;
        }
    }
}