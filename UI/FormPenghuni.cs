using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using management_kos.Services;
using management_kos.Models;

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
            this.Load += FormPenghuni_Load;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            var row = dataGridView1.Rows[e.RowIndex];
            _selectedPenghuniId = Convert.ToInt32(row.Cells[nameof(Penghuni.Id)].Value);

            txtNama.Text    = Convert.ToString(row.Cells[nameof(Penghuni.Nama)].Value);
            txtTelpon.Text  = Convert.ToString(row.Cells[nameof(Penghuni.NomorTelepon)].Value);
            txtEmail.Text   = Convert.ToString(row.Cells[nameof(Penghuni.Email)].Value);
            txtCatatan.Text = Convert.ToString(row.Cells[nameof(Penghuni.Catatan)].Value);
        }

        // Helper methods
        private Penghuni BuildPenghuniFromInput()
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text))
                throw new ArgumentException("Nama wajib diisi.");
            if (string.IsNullOrWhiteSpace(txtTelpon.Text))
                throw new ArgumentException("No. Telepon wajib diisi.");

            return new Penghuni
            {
                Nama         = txtNama.Text.Trim(),
                NomorTelepon = txtTelpon.Text.Trim(),
                Email        = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Catatan      = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
            };
        }

        private void ClearInput()
        {
            _selectedPenghuniId = 0;
            txtNama.Clear();
            txtTelpon.Clear();
            txtEmail.Clear();
            txtCatatan.Clear();
            txtNama.Focus();
        }

        public void RefreshData()
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var items = _penghuniService.GetAllPenghuni();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = items;

            HideColumn(nameof(Penghuni.KamarId));
            HideColumn(nameof(Penghuni.TanggalMasuk));
            HideColumn(nameof(Penghuni.TanggalKeluar));
            HideColumn(nameof(Penghuni.IsActive));

            if (dataGridView1.Columns[nameof(Penghuni.InfoKamar)] is not null)
            {
                dataGridView1.Columns[nameof(Penghuni.InfoKamar)].HeaderText = "Kamar";
            }
        }

        private void FormPenghuni_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            ClearInput();
        }

        private void HideColumn(string columnName)
        {
            if (dataGridView1.Columns[columnName] is not null)
            {
                dataGridView1.Columns[columnName].Visible = false;
            }
        }
    }
}
