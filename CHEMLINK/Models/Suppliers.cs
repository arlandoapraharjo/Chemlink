using System;

namespace CHEMLINK.Models
{
    // Model untuk Supplier dengan kolom lengkap
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // nama_perusahaan
        public string Phone { get; set; } = string.Empty; // no_telp
        public string Address { get; set; } = string.Empty; // alamat_supplier
        public string City { get; set; } = string.Empty; // kota_supplier
        public string ContactPerson { get; set; } = string.Empty; // kontak_person
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = "Active"; // status
    }
}