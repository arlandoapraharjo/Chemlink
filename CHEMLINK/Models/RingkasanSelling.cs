using System;

namespace CHEMLINK.Models
{
    // Model untuk view v_ringkasan_selling
    public class RingkasanSelling
    {
        public int IdSelling { get; set; }
        public string NoFaktur { get; set; } = string.Empty;
        public DateTime TanggalSelling { get; set; }
        public string Keterangan { get; set; } = string.Empty;
        public string Kasir { get; set; } = string.Empty;
    }
}
