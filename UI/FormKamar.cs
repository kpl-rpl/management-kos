using management_kos.Models;
using management_kos.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace management_kos.UI
{
    public partial class FormKamar : Form
    {

        private readonly KamarService _kamarService;
        private readonly KosService _kosService;
        private int _selectedKamarId;
        private int _selectedKosId;

        public FormKamar(KamarService kamarService, KosService kosService, int kosId)
        {
            _kamarService = kamarService;
            _kosService = kosService;
            _selectedKosId = kosId;
            InitializeComponent();
        }

        public FormKamar(KamarService kamarService, KosService kosService)
        {
            _kamarService = kamarService;
            _kosService = kosService;
            InitializeComponent();
        }

        private void FormKamar_Load(object sender, EventArgs e)
        {
            LoadKosToComboBox();
            RefreshGrid();
        }

        private void LoadKosToComboBox()
        {
            var listKos = _kosService.GetAllKos();
            comboBox1.DataSource = listKos;
            comboBox1.DisplayMember = "NamaKos";
            comboBox1.ValueMember = "Id";

            if (listKos.Count == 0)
            {
                _selectedKosId = 0;
                return;
            }

            var selectedIndex = listKos.FindIndex(k => k.Id == _selectedKosId);
            if (selectedIndex >= 0)
            {
                comboBox1.SelectedIndex = selectedIndex;
                return;
            }

            comboBox1.SelectedIndex = 0;
            _selectedKosId = listKos[0].Id;
        }

        private void dgvKos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKos.Rows.Count) return;
            if (dgvKos.AllowUserToAddRows && e.RowIndex == dgvKos.Rows.Count - 1) return;

            var row = dgvKos.Rows[e.RowIndex];

            var idCell = row.Cells[nameof(Kamar.Id)];
            if (idCell?.Value == null || idCell.Value == DBNull.Value)
            {
                _selectedKamarId = 0;
            }
            else if (!int.TryParse(Convert.ToString(idCell.Value), out _selectedKamarId))
            {
                _selectedKamarId = 0;
            }

            var nomorKamarCell = row.Cells[nameof(Kamar.NomorKamar)];
            textBox1.Text = Convert.ToString(nomorKamarCell?.Value);

            var statusCell = row.Cells[nameof(Kamar.Status)];
            var status = Convert.ToString(statusCell?.Value);
            SetStatusRadio(status);
        }

        private void SetStatusRadio(string? status)
        {
            radioButton1.Checked = status == "Kosong";
            radioButton2.Checked = status == "Terisi";
            radioButton3.Checked = status == "Dipesan";
            radioButton4.Checked = status == "Perbaikan";
        }

        private string GetSelectedStatus()
        {
            if (radioButton1.Checked) return "Kosong";
            if (radioButton2.Checked) return "Terisi";
            if (radioButton3.Checked) return "Dipesan";
            if (radioButton4.Checked) return "Perbaikan";
            return "Kosong";
        }

        private void RefreshGrid()
        {
            var data = _kamarService.GetKamarByKosId(_selectedKosId);

            dgvKos.DataSource = null;
            dgvKos.DataSource = data;

            if (dgvKos.Columns.Count > 0)
            {
                if (dgvKos.Columns.Contains(nameof(Kamar.Id)))
                    dgvKos.Columns[nameof(Kamar.Id)].HeaderText = "ID";
                if (dgvKos.Columns.Contains(nameof(Kamar.NomorKamar)))
                    dgvKos.Columns[nameof(Kamar.NomorKamar)].HeaderText = "Nomor Kamar";
                if (dgvKos.Columns.Contains(nameof(Kamar.Status)))
                    dgvKos.Columns[nameof(Kamar.Status)].HeaderText = "Status";

                dgvKos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ClearInput()
        {
            _selectedKamarId = 0;
            textBox1.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                Kamar kamar = BuildKamarFromInput();
                _kamarService.TambahKamar(kamar);

                RefreshGrid();
                ClearInput();

                MessageBox.Show("Kamar berhasil ditambahkan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Kamar BuildKamarFromInput()
        {
            return new Kamar
            {
                KosId = _selectedKosId,
                NomorKamar = textBox1.Text.Trim(),
                Status = GetSelectedStatus()
            };
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedKamarId <= 0)
            {
                MessageBox.Show("Pilih kamar terlebih dahulu.");
                return;
            }

            try
            {
                Kamar kamar = BuildKamarFromInput();
                kamar.Id = _selectedKamarId;

                _kamarService.UbahKamar(kamar);

                RefreshGrid();
                ClearInput();

                MessageBox.Show("Kamar berhasil diupdate.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_selectedKamarId <= 0)
            {
                MessageBox.Show("Pilih kamar terlebih dahulu.");
                return;
            }

            var confirm = MessageBox.Show("Yakin hapus kamar?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            _kamarService.HapusKamar(_selectedKamarId);

            RefreshGrid();
            ClearInput();

            MessageBox.Show("Kamar berhasil dihapus.");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Tidak perlu implementasi khusus
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            // Tidak perlu implementasi khusus
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is Kos selectedKos)
            {
                _selectedKosId = selectedKos.Id;
                RefreshGrid();
            }
        }
    }
}
