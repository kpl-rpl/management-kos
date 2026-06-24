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
            [FormState.Idle]     = new HashSet<string> { "btnTambah", "btnReset", "btnCari" },
            [FormState.Selected] = new HashSet<string> { "btnTambah", "btnUpdate", "btnHapus", "btnSelesai", "btnBatal", "btnPerpanjang", "btnReset", "btnCari" },
        };

        private readonly KontrakSewaService _service;
        private readonly PembayaranService _pembayaranService;
        private readonly PenghuniService _penghuniService;
        private readonly KamarService _kamarService;
        private readonly KosService _kosService;
        private readonly ReferenceDataService _referenceDataService;
        private int _selectedId = 0;

        public FormKontrakSewa(
            KontrakSewaService service,
            PembayaranService pembayaranService,
            PenghuniService penghuniService,
            KamarService kamarService,
            KosService kosService,
            ReferenceDataService referenceDataService)
        {
            _service = service;
            _pembayaranService = pembayaranService;
            _penghuniService = penghuniService;
            _kamarService = kamarService;
            _kosService = kosService;
            _referenceDataService = referenceDataService;
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
            ComboBoxSearchHelper.EnableSearch(cmbStatus);
            cmbFilterStatus.Items.AddRange(new[] { "Semua", "Dipesan", "Aktif", "Selesai", "Dibatalkan" });
            cmbFilterStatus.SelectedIndex = 0;
            ComboBoxSearchHelper.EnableSearch(cmbFilterStatus);
            ApplyDepositState();

            LoadPenghuniDropdown();
            LoadKosDropdown();
            LoadMetodePembayaranDropdown();
            lblMetodePembayaran.Visible = false;
            cmbMetodePembayaran.Visible = false;
            ApplyState(FormState.Idle);
            RefreshGrid();
        }

        private void LoadPenghuniDropdown()
        {
            var list = _penghuniService.GetAllPenghuni();
            cmbPenghuni.DataSource = list;
            cmbPenghuni.DisplayMember = "Nama";
            cmbPenghuni.ValueMember = "Id";
            ComboBoxSearchHelper.EnableSearch(cmbPenghuni);
        }

        private void LoadKosDropdown()
        {
            var list = _kosService.GetAllKos();
            cmbKos.DataSource = list;
            cmbKos.DisplayMember = "NamaKos";
            cmbKos.ValueMember = "Id";
            ComboBoxSearchHelper.EnableSearch(cmbKos);
        }

        private void LoadMetodePembayaranDropdown()
        {
            var list = _referenceDataService.GetAllMetodePembayaran();
            cmbMetodePembayaran.DisplayMember = nameof(MetodePembayaranRef.NamaMetode);
            cmbMetodePembayaran.ValueMember = nameof(MetodePembayaranRef.Id);
            cmbMetodePembayaran.DataSource = list;
            ComboBoxSearchHelper.EnableSearch(cmbMetodePembayaran);
        }

        private void LoadKamarDropdown(int kosId)
        {
            var list = _kamarService.GetKamarByKosId(kosId);
            list = list.FindAll(k => k.Status != KamarStatus.Perbaikan);
            cmbKamar.DataSource = list;
            cmbKamar.DisplayMember = "NomorKamar";
            cmbKamar.ValueMember = "Id";
            ComboBoxSearchHelper.EnableSearch(cmbKamar);
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
            cmbMetodePembayaran.Enabled = state == FormState.Idle;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                var k = BuildFromInput();
                var penghuniBaru = BuildPenghuniBaruFromInput();
                if (penghuniBaru is null)
                {
                    _service.TambahKontrak(k);
                }
                else
                {
                    _service.TambahKontrakDenganPenghuni(penghuniBaru, k);
                    LoadPenghuniDropdown();
                }

                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak berhasil ditambahkan. Pembayaran dapat dicatat dari menu Pembayaran.", "Informasi",
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
                var summary = _pembayaranService.GetSummary(_selectedId);
                if (summary.SisaPembayaran > 0)
                {
                    _pembayaranService.CatatPembayaran(new Pembayaran
                    {
                        KontrakSewaId = _selectedId,
                        TanggalBayar = DateTime.Today,
                        JumlahDibayar = summary.SisaPembayaran,
                        MetodePembayaran = GetDefaultMetodePembayaran(),
                        Catatan = "Pelunasan otomatis saat kontrak diselesaikan."
                    });
                }

                _service.SelesaikanKontrak(_selectedId);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Kontrak berhasil diselesaikan dan sisa pembayaran dicatat lunas.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDefaultMetodePembayaran()
        {
            return cmbMetodePembayaran.SelectedItem is MetodePembayaranRef metode
                ? metode.NamaMetode
                : "Transfer";
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

        private void btnCari_Click(object sender, EventArgs e)
        {
            RefreshGrid(txtCari.Text.Trim());
        }

        private void btnPerpanjang_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Pilih kontrak yang akan diperpanjang.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!decimal.TryParse(txtDurasiBulan.Text.Trim(), out var durasi))
                    throw new ArgumentException("Durasi perpanjangan harus berupa angka.");

                _service.PerpanjangKontrak(_selectedId, durasi, txtCatatan.Text);
                RefreshGrid();
                ClearInput();
                MessageBox.Show("Perpanjangan kontrak berhasil ditambahkan.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            txtDurasiBulan.Text     = Convert.ToString(row.Cells[nameof(KontrakSewa.DurasiBulanInput)].Value);
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
            var hasPenghuniBaru = !string.IsNullOrWhiteSpace(txtNamaPenghuniBaru.Text);
            var penghuniId = cmbPenghuni.SelectedValue is int selectedPenghuniId
                ? selectedPenghuniId
                : 0;

            if (!hasPenghuniBaru && penghuniId <= 0)
                throw new ArgumentException("Penghuni harus dipilih.");
            if (cmbKamar.SelectedValue is not int kamarId || kamarId <= 0)
                throw new ArgumentException("Kamar harus dipilih.");
            if (cmbKamar.SelectedItem is not Kamar kamar)
                throw new ArgumentException("Data kamar tidak valid.");

            decimal.TryParse(txtDeposit.Text.Trim(), out decimal deposit);
            if (!decimal.TryParse(txtDurasiBulan.Text.Trim(), out decimal durasiBulan))
                throw new ArgumentException("Durasi sewa harus berupa angka.");

            return new KontrakSewa
            {
                PenghuniId       = penghuniId,
                KamarId          = kamarId,
                TanggalMulai     = dtpTanggalMulai.Value.Date,
                DurasiBulanInput = durasiBulan,
                HargaSewaBulanan = kamar.HargaKamar,
                Deposit          = string.IsNullOrWhiteSpace(txtDeposit.Text) ? null : deposit,
                Status           = Enum.TryParse(cmbStatus.SelectedItem?.ToString(), out KontrakStatus parsed)
                                   ? parsed
                                   : KontrakStatus.Aktif,
                Catatan          = string.IsNullOrWhiteSpace(txtCatatan.Text) ? null : txtCatatan.Text.Trim()
            };
        }

        private Penghuni? BuildPenghuniBaruFromInput()
        {
            if (string.IsNullOrWhiteSpace(txtNamaPenghuniBaru.Text))
                return null;

            return new Penghuni
            {
                Nama = txtNamaPenghuniBaru.Text.Trim(),
                NomorTelepon = txtTeleponPenghuniBaru.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmailPenghuniBaru.Text) ? null : txtEmailPenghuniBaru.Text.Trim(),
                TanggalMasuk = dtpTanggalMulai.Value.Date
            };
        }

        public void RefreshData()
        {
            LoadPenghuniDropdown();
            LoadKosDropdown();
            LoadMetodePembayaranDropdown();
            RefreshGrid();
        }

        private void RefreshGrid(string keyword = "")
        {
            var data = string.IsNullOrWhiteSpace(keyword)
                ? _service.GetAll()
                : _service.Search(keyword);
            var status = cmbFilterStatus.Text.Trim();
            if (!string.IsNullOrWhiteSpace(status) && status != "Semua")
            {
                data = data.Where(k => k.Status.ToString() == status).ToList();
            }

            dgvKontrak.DataSource = null;
            dgvKontrak.DataSource = data;
            SetHeader(dgvKontrak, nameof(KontrakSewa.Id), "Nomor");
            HideColumn(dgvKontrak, "Catatan");
            HideColumn(dgvKontrak, nameof(KontrakSewa.IsActive));
            HideColumn(dgvKontrak, nameof(KontrakSewa.PenghuniId));
            HideColumn(dgvKontrak, nameof(KontrakSewa.KamarId));
        }

        private void RefreshPembayaranGrid(int? kontrakId = null)
        {
            var data = kontrakId.HasValue && kontrakId.Value > 0
                ? _pembayaranService.GetByKontrak(kontrakId.Value)
                : _pembayaranService.GetAll();

            dgvPembayaran.DataSource = null;
            dgvPembayaran.DataSource = data;

            HideColumn(dgvPembayaran, "Catatan");
            HideColumn(dgvPembayaran, nameof(Pembayaran.IsActive));
        }

        private static void HideColumn(DataGridView grid, string columnName)
        {
            var column = grid.Columns[columnName];
            if (column is not null)
            {
                column.Visible = false;
            }
        }

        private static void SetHeader(DataGridView grid, string columnName, string headerText)
        {
            var column = grid.Columns[columnName];
            if (column is not null)
            {
                column.HeaderText = headerText;
            }
        }

        private void ClearInput()
        {
            if (cmbPenghuni.Items.Count > 0) cmbPenghuni.SelectedIndex = 0;
            if (cmbKos.Items.Count > 0) cmbKos.SelectedIndex = 0;
            if (cmbKamar.Items.Count > 0) cmbKamar.SelectedIndex = 0;
            if (cmbMetodePembayaran.Items.Count > 0) cmbMetodePembayaran.SelectedIndex = 0;
            dtpTanggalMulai.Value   = DateTime.Today;
            txtDurasiBulan.Text     = "1";
            UpdateHargaKamarInfo();
            txtDeposit.Text         = string.Empty;
            txtCatatan.Text         = string.Empty;
            txtNamaPenghuniBaru.Text = string.Empty;
            txtTeleponPenghuniBaru.Text = string.Empty;
            txtEmailPenghuniBaru.Text = string.Empty;
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;
            ApplyDepositState();
            ApplyState(FormState.Idle);
        }
    }
}
