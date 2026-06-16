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
        public string Address { get; set; } = string.Empty; // alamat
        public string Phone { get; set; } = string.Empty; // no_telp
        public string Email { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty; // kecamatan
        public string Status { get; set; } = "Active"; // status
    }
}