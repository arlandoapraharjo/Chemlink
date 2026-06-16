using System;

namespace CHEMLINK.Models
{
    // Model untuk Produk Obat Pertanian dengan kolom lengkap
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // keterangan
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpiryDate { get; set; } // tanggal_exp
        public string SupplierName { get; set; } = string.Empty; // nama_supplier
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}