using System;

namespace CHEMLINK.Models
{
    // Model untuk view v_ringkasan_pesanan
    public class RingkasanPesanan
    {
        public int IdOrder { get; set; }
        public string NoFaktur { get; set; } = string.Empty;
        public DateTime TanggalOrder { get; set; }
        public string KeteranganOrder { get; set; } = string.Empty;
        public string DiinputOleh { get; set; } = string.Empty;
    }
}
