using System;

namespace CHEMLINK.Models
{
    // Model untuk Stok Kritis (Stock < 10)
    public class StockKritis
    {
        public int IdProduk { get; set; }
        public string NamaProduk { get; set; } = string.Empty;
        public int JumlahStock { get; set; }
    }
}
