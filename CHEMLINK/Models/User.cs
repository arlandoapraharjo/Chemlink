using System;

namespace CHEMLINK.Models
{
    // Model untuk Pengguna Sistem (Admin & Kasir) dengan kolom lengkap
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Admin" atau "Kasir"
        public string FullName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Alamat { get; set; } = string.Empty;
        public string NoTelp { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Kecamatan { get; set; } = string.Empty;
    }
}