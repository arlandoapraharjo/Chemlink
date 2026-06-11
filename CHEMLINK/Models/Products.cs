using System;

namespace CHEMLINK.Models
{
    // Model untuk Pengguna Sistem (Admin & Kasir)
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Admin" atau "Kasir"
        public string FullName { get; set; } = string.Empty;
    }

    // Model untuk Produk Obat Pertanian (Menggunakan Stock, tanpa ExpiryDate)
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

    // Model untuk Supplier
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    // Model Item Keranjang Belanja untuk transaksi Kasir
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Qty * Price;
    }
}

//test commit in products