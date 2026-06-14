using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace CHEMLINK.Helpers
{
    public class ConnectDB
    {
        private static string connString =
            "Host=localhost;" +
            "Port=5432;" +
            "Username=postgres;" +
            "Password=pass;" + //pw kalian
            "Database=Chemlink_DB;"; //nama db kalian
            
        public static NpgsqlConnection GetConn() 
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal di : " + ex.Message);
            }

            return conn;
        }

        public static void UpdateDatabaseObjects()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string sqlFile = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\Database\Chemlink_Advanced_Objects.sql"));
                if (File.Exists(sqlFile))
                {
                    string sql = File.ReadAllText(sqlFile);
                    using (var conn = GetConn())
                    {
                        if (conn != null && conn.State == System.Data.ConnectionState.Open)
                        {
                            using (var cmd = new NpgsqlCommand(sql, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Script SQL lanjutan tidak ditemukan: " + sqlFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal update objek database: " + ex.Message);
            }
        }
    }
}
