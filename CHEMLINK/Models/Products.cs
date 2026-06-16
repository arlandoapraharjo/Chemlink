using System;

namespace CHEMLINK.Models
{
    // Model untuk Produk Obat Pertanian (Menggunakan Stock)
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
//test commit in products