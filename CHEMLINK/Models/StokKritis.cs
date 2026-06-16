using System;

namespace CHEMLINK.Models
{
    // Model untuk view v_stok_kritis
    public class StokKritis
    {
        public int IdProduk { get; set; }
        public string NamaProduk { get; set; } = string.Empty;
        public int JumlahStock { get; set; }
    }
}
