using System;

namespace CHEMLINK.Models
{
    // Model untuk view v_log_stok
    public class LogStok
    {
        public int IdLog { get; set; }
        public string TipeAktivitas { get; set; } = string.Empty;
        public int IdUser { get; set; }
        public string NamaUser { get; set; } = string.Empty;
        public string NamaProduk { get; set; } = string.Empty;
        public int Jumlah { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
