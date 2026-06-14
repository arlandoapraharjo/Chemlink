using System;

namespace CHEMLINK.Models
{
    // Model untuk Supplier
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string KontakPerson { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Kota { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}