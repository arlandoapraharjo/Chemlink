using System;

namespace CHEMLINK.Models
{
    // Model untuk Produk Obat Pertanian (Menggunakan Stock, tanpa ExpiryDate)
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
//test commit in products