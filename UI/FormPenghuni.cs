using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace management_kos.UI
{
    public partial class FormPenghuni : Form
    {
        private readonly PenghuniService _penghuniService;
        private int _selectedPenghuniId = 0;

        public FormPenghuni(PenghuniService penghuniService)
        {
            _penghuniService = penghuniService;
            InitializeComponent();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                var penghuni = BuildPenghuniFromInput();
                _penghuniService.TambahPenghuni(penghuni);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Data penghuni berhasil ditambahkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPenghuniId <= 0)
            {
                MessageBox.Show("Pilih data penghuni yang akan diubah.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var penghuni = BuildPenghuniFromInput();
                penghuni.Id = _selectedPenghuniId;
                _penghuniService.UbahPenghuni(penghuni);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Data penghuni berhasil diubah.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_selectedPenghuniId <= 0)
            {
                MessageBox.Show("Pilih data penghuni yang akan dihapus.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus data penghuni ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            _penghuniService.HapusPenghuni(_selectedPenghuniId);
            RefreshGrid();
            ClearInput();
            MessageBox.Show("Data penghuni berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        // Helper methods
        private Penghuni BuildPenghuniFromInput()
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text))
                throw new ArgumentException("Nama wajib diisi.");
            if (string.IsNullOrWhiteSpace(txtNIK.Text))
                throw new ArgumentException("NIK wajib diisi.");
            if (string.IsNullOrWhiteSpace(txtTelpon.Text))
                throw new ArgumentException("No. Telpon wajib diisi.");

            return new Penghuni
            {
                Nama = txtNama.Text.Trim(),
                NIK = txtNIK.Text.Trim(),
                Telpon = txtTelpon.Text.Trim()
            };
        }

        private void ClearInput()
        {
            _selectedPenghuniId = 0;
            txtNama.Clear();
            txtNIK.Clear();
            txtTelpon.Clear();
            txtNama.Focus();
        }

        private void RefreshGrid()
        {
            var items = _penghuniService.GetAllPenghuni();
            dgvPenghuni.DataSource = null;
            dgvPenghuni.DataSource = items;
        }
    }
}
