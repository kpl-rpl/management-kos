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
        private readonly PembayaranService _pembayaranService;
        private readonly PenghuniService _penghuniService;
        private readonly KamarService _kamarService;
        private readonly KosService _kosService;
        private int _selectedId = 0;

        public FormKontrakSewa(
            KontrakSewaService service,
            PembayaranService pembayaranService,
            PenghuniService penghuniService,
            KamarService kamarService,
            KosService kosService)
        {
            _service = service;
            _pembayaranService = pembayaranService;
            _penghuniService = penghuniService;
            _kamarService = kamarService;
            _kosService = kosService;
            InitializeComponent();
            this.Load += FormKontrakSewa_Load;
        }

        private void FormKontrakSewa_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new[]
            {
                KontrakStatus.Dipesan.ToString(),
                KontrakStatus.Aktif.ToString()
            });
            cmbStatus.SelectedItem = KontrakStatus.Dipesan.ToString();
            ApplyDepositState();

            LoadPenghuniDropdown();
            LoadKosDropdown();
            ApplyState(FormState.Idle);
            RefreshGrid();
        }

        private void LoadPenghuniDropdown()
        {
            var list = _penghuniService.GetAllPenghuni();
            cmbPenghuni.DataSource = list;
            cmbPenghuni.DisplayMember = "Nama";
            cmbPenghuni.ValueMember = "Id";
        }

        private void LoadKosDropdown()
        {
            var list = _kosService.GetAllKos();
            cmbKos.DataSource = list;
            cmbKos.DisplayMember = "NamaKos";
            cmbKos.ValueMember = "Id";
        }

        private void LoadKamarDropdown(int kosId)
        {
            var list = _kamarService.GetKamarByKosId(kosId);
            list = list.FindAll(k => k.Status != KamarStatus.Perbaikan);
            cmbKamar.DataSource = list;
            cmbKamar.DisplayMember = "NomorKamar";
            cmbKamar.ValueMember = "Id";
            UpdateHargaKamarInfo();
        }

        private void cmbKos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKos.SelectedValue is int kosId && kosId > 0)
            {
                LoadKamarDropdown(kosId);
            }
        }

        private void cmbKamar_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHargaKamarInfo();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyDepositState();
        }

        private void ApplyDepositState()
        {
            var status = cmbStatus.SelectedItem?.ToString();
            var isAktif = status == KontrakStatus.Aktif.ToString();

            if (isAktif)
            {
                txtDeposit.Text = "0";
            }

            txtDeposit.Enabled = !isAktif;
            txtDeposit.BackColor = isAktif
                ? Color.FromArgb(229, 231, 235)
                : Color.White;
        }

        private void UpdateHargaKamarInfo()
        {
            if (cmbKamar.SelectedItem is Kamar kamar)
            {
                lblHargaValue.Text = kamar.HargaKamar.ToString("N0");
                return;
            }

            lblHargaValue.Text = "-";
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
                CatatPembayaranLunas(_selectedId);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Pembayaran kontrak bulan ini ditandai Lunas.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CatatPembayaranLunas(int kontrakId)
        {
            var kontrak = _service.GetById(kontrakId)
                ?? throw new InvalidOperationException("Kontrak tidak ditemukan.");

            var periode = DateTime.Today.ToString("yyyy-MM");
            var pembayaran = _pembayaranService
                .GetByKontrak(kontrakId)
                .FirstOrDefault(p => string.Equals(p.Periode, periode, StringComparison.OrdinalIgnoreCase));

            if (pembayaran is null)
            {
                _pembayaranService.TambahTagihan(new Pembayaran
                {
                    KontrakSewaId = kontrak.Id,
                    Periode = periode,
                    TanggalBayar = DateTime.Today,
                    JumlahTagihan = kontrak.HargaSewaBulanan,
                    JumlahDibayar = kontrak.HargaSewaBulanan,
                    MetodePembayaran = "Tunai",
                    Catatan = "Pembayaran lunas dicatat dari modul kontrak sewa."
                });
                return;
            }

            var sisaTagihan = pembayaran.JumlahTagihan - pembayaran.JumlahDibayar;
            if (sisaTagihan <= 0 || pembayaran.Status == StatusPembayaran.Lunas.ToString())
            {
                throw new InvalidOperationException("Pembayaran periode ini sudah lunas.");
            }

            _pembayaranService.BayarTagihan(pembayaran.Id, sisaTagihan, "Tunai");
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

            _selectedId = Convert.ToInt32(row.Cells["Id"].Value);

            var penghuniId = Convert.ToInt32(row.Cells["PenghuniId"].Value);
            cmbPenghuni.SelectedValue = penghuniId;

            var kamarId = Convert.ToInt32(row.Cells["KamarId"].Value);
            var kamar = _kamarService.GetAllKamar().FirstOrDefault(k => k.Id == kamarId);
            if (kamar is not null)
            {
                cmbKos.SelectedValue = kamar.KosId;
            }
            cmbKamar.SelectedValue = kamarId;

            dtpTanggalMulai.Value   = Convert.ToDateTime(row.Cells["TanggalMulai"].Value);
            dtpTanggalSelesai.Value = Convert.ToDateTime(row.Cells["TanggalSelesai"].Value);
            txtDeposit.Text         = row.Cells["Deposit"].Value == DBNull.Value || row.Cells["Deposit"].Value == null
                                      ? string.Empty
                                      : Convert.ToString(row.Cells["Deposit"].Value);
            txtCatatan.Text         = Convert.ToString(row.Cells["Catatan"].Value);

            var statusValue = Convert.ToString(row.Cells["Status"].Value) ?? KontrakStatus.Aktif.ToString();
            int idx = cmbStatus.Items.IndexOf(statusValue);
            cmbStatus.SelectedIndex = idx >= 0 ? idx : 0;
            ApplyDepositState();

            // Automata: transisi ke state Selected saat baris dipilih
            ApplyState(FormState.Selected);
        }

        private KontrakSewa BuildFromInput()
        {
            if (cmbPenghuni.SelectedValue is not int penghuniId || penghuniId <= 0)
                throw new ArgumentException("Penghuni harus dipilih.");
            if (cmbKamar.SelectedValue is not int kamarId || kamarId <= 0)
                throw new ArgumentException("Kamar harus dipilih.");
            if (cmbKamar.SelectedItem is not Kamar kamar)
                throw new ArgumentException("Data kamar tidak valid.");

            decimal.TryParse(txtDeposit.Text.Trim(), out decimal deposit);

            return new KontrakSewa
            {
                PenghuniId       = penghuniId,
                KamarId          = kamarId,
                TanggalMulai     = dtpTanggalMulai.Value.Date,
                TanggalSelesai   = dtpTanggalSelesai.Value.Date,
                HargaSewaBulanan = kamar.HargaKamar,
                Deposit          = string.IsNullOrWhiteSpace(txtDeposit.Text) ? null : deposit,
                Status           = Enum.TryParse(cmbStatus.SelectedItem?.ToString(), out KontrakStatus parsed)
                                   ? parsed
                                   : KontrakStatus.Aktif,
                Catatan          = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
            };
        }

        public void RefreshData()
        {
            LoadPenghuniDropdown();
            LoadKosDropdown();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvKontrak.DataSource = null;
            dgvKontrak.DataSource = _service.GetAll();
            if (dgvKontrak.Columns["Catatan"] != null)
                dgvKontrak.Columns["Catatan"].Visible = false;
        }

        private void ClearInput()
        {
            if (cmbPenghuni.Items.Count > 0) cmbPenghuni.SelectedIndex = 0;
            if (cmbKos.Items.Count > 0) cmbKos.SelectedIndex = 0;
            if (cmbKamar.Items.Count > 0) cmbKamar.SelectedIndex = 0;
            dtpTanggalMulai.Value   = DateTime.Today;
            dtpTanggalSelesai.Value = DateTime.Today.AddMonths(12);
            UpdateHargaKamarInfo();
            txtDeposit.Text         = string.Empty;
            txtCatatan.Text         = string.Empty;
            cmbStatus.SelectedIndex = 0;
            ApplyDepositState();
            ApplyState(FormState.Idle);
        }
    }
}
