using System;
using System.Collections.Generic;
using System.Text;

namespace management_kos.Models
{
    public class Penghuni
    {
        public int Id { get; set; }
        public int KamarId { get; set; }
        public string Nama { get; set; } = string.Empty;
        public string NomorTelepon { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime TanggalMasuk { get; set; } = DateTime.Today;
        public DateTime? TanggalKeluar { get; set; }
        public string? Catatan { get; set; }
    }
}
