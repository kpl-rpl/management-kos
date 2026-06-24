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
            cmbFilterStatus.Items.AddRange(new[] { "Semua", "Kosong", "Terisi", "Dipesan", "Perbaikan" });
            cmbFilterStatus.SelectedIndex = 0;
            ComboBoxSearchHelper.EnableSearch(cmbFilterStatus);
            LoadKosToComboBox();
            radioButton1.Checked = true;
            RefreshGrid();
        }

        private void LoadKosToComboBox()
        {
            var listKos = _kosService.GetAllKos();
            comboBox1.DataSource = listKos;
            comboBox1.DisplayMember = "NamaKos";
            comboBox1.ValueMember = "Id";
            ComboBoxSearchHelper.EnableSearch(comboBox1);

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
                _selectedKamarId = 0;
            else if (!int.TryParse(Convert.ToString(idCell.Value), out _selectedKamarId))
                _selectedKamarId = 0;

            textBox1.Text      = Convert.ToString(row.Cells[nameof(Kamar.NomorKamar)]?.Value);
            txtHargaKamar.Text = Convert.ToString(row.Cells[nameof(Kamar.HargaKamar)]?.Value);

            var statusValue = row.Cells[nameof(Kamar.Status)]?.Value;
            var statusText = statusValue?.ToString();
            SetStatusRadio(statusText);
        }

        private void SetStatusRadio(string? status)
        {
            if (!Enum.TryParse(status, true, out KamarStatus parsed))
            {
                radioButton1.Checked = true;
                return;
            }

            radioButton1.Checked = parsed == KamarStatus.Kosong;
            radioButton2.Checked = parsed == KamarStatus.Terisi;
            radioButton3.Checked = parsed == KamarStatus.Dipesan;
            radioButton4.Checked = parsed == KamarStatus.Perbaikan;
        }

        private KamarStatus GetSelectedStatus()
        {
            if (radioButton1.Checked) return KamarStatus.Kosong;
            if (radioButton2.Checked) return KamarStatus.Terisi;
            if (radioButton3.Checked) return KamarStatus.Dipesan;
            if (radioButton4.Checked) return KamarStatus.Perbaikan;
            return KamarStatus.Kosong;
        }

        public void RefreshData()
        {
            LoadKosToComboBox();
            var kosAda = _kosService.GetAllKos().Count > 0;
            SetInputEnabled(kosAda);
            if (kosAda) RefreshGrid();
        }

        private void SetInputEnabled(bool enabled)
        {
            textBox1.Enabled = enabled;
            txtHargaKamar.Enabled = enabled;
            radioButton1.Enabled = enabled;
            radioButton2.Enabled = enabled;
            radioButton3.Enabled = enabled;
            radioButton4.Enabled = enabled;
            btnTambah.Enabled = enabled;
            btnUpdate.Enabled = enabled;
            btnHapus.Enabled = enabled;
            if (!enabled)
            {
                dgvKos.DataSource = null;
                MessageBox.Show(
                    "Belum ada data Kos. Tambahkan Kos terlebih dahulu sebelum mengelola Kamar.",
                    "Data Kos Kosong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void RefreshGrid()
        {
            if (_selectedKosId <= 0) return;
            var data = _kamarService.GetKamarByKosId(_selectedKosId);
            var keyword = txtCari.Text.Trim();
            var status = cmbFilterStatus.Text.Trim();

            if (!string.IsNullOrWhiteSpace(status) && status != "Semua")
            {
                data = data.Where(k => k.Status.ToString() == status).ToList();
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(k =>
                    k.Id.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || k.NomorKamar.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || (k.NamaKos?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false)
                    || k.Status.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            dgvKos.DataSource = null;
            dgvKos.DataSource = data;

            if (dgvKos.Columns.Count > 0)
            {
                SetHeader(nameof(Kamar.Id), "Nomor");
                SetHeader(nameof(Kamar.NomorKamar), "Nomor Kamar");
                SetHeader(nameof(Kamar.Status), "Status");
                SetHeader(nameof(Kamar.DisplayText), "Nama Kamar");
                HideColumn(nameof(Kamar.KosId));
                HideColumn(nameof(Kamar.IsActive));

                dgvKos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void SetHeader(string columnName, string headerText)
        {
            var column = dgvKos.Columns[columnName];
            if (column is not null)
            {
                column.HeaderText = headerText;
            }
        }

        private void HideColumn(string columnName)
        {
            var column = dgvKos.Columns[columnName];
            if (column is not null)
            {
                column.Visible = false;
            }
        }

        private void ClearInput()
        {
            _selectedKamarId = 0;
            textBox1.Clear();
            txtHargaKamar.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            RefreshGrid();
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
            if (!int.TryParse(txtHargaKamar.Text.Trim(), out int harga))
                throw new ArgumentException("Harga Kamar harus berupa angka bulat.");

            return new Kamar
            {
                KosId      = _selectedKosId,
                NomorKamar = textBox1.Text.Trim(),
                HargaKamar = harga,
                Status     = GetSelectedStatus()
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
