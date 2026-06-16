using System;

namespace CHEMLINK.Models
{
    // Model untuk view v_detail_produk
    public class DetailProduk
    {
        public int IdProduk { get; set; }
        public string NamaProduk { get; set; } = string.Empty;
        public decimal Harga { get; set; }
        public DateTime? TanggalExp { get; set; }
        public string Keterangan { get; set; } = string.Empty;
        public string NamaKategori { get; set; } = string.Empty;
        public string NamaSupplier { get; set; } = string.Empty;
        public int JumlahStock { get; set; }
    }
}
