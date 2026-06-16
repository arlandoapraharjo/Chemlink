using System;

namespace CHEMLINK.Models
{
    // Model Item Keranjang Belanja untuk transaksi Kasir
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Qty * Price;
    }

    public class CartItemEventArgs : EventArgs
    {
        public Product SelectedProduct { get; set; } = null!;
        public int Qty { get; set; }
    }
}