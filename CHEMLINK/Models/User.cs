using System;

namespace CHEMLINK.Models
{
    // Model untuk Pengguna Sistem (Admin & Kasir)
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Admin" atau "Kasir"
        public string FullName { get; set; } = string.Empty;
    }
}