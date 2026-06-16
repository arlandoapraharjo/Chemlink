using System;
using CHEMLINK.Models;

namespace CHEMLINK.Models
{
    public class UserEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Status { get; set; } = "";
        public string Alamat { get; set; } = "";
        public string NoTelp { get; set; } = "";
        public string Email { get; set; } = "";
        public string Kecamatan { get; set; } = "";
    }

    public class ProductEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public DateTime? ExpiryDate { get; set; }
    }

    public class CategoryEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class CartItemEventArgs : EventArgs
    {
        public Product SelectedProduct { get; set; } = null!;
        public int Qty { get; set; }
    }

    public class SupplierEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string KontakPerson { get; set; } = "";
        public string Email { get; set; } = "";
        public string Kota { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
