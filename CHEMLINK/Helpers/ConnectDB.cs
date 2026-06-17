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
            "Password=manchmall123;" + //pw kalian
            "Database=ChemDB;" + //nama db kalian
            "Search Path=chemlink_sch;" +
            "Timeout=10;" +
            "CommandTimeout=30;";

            
        public static NpgsqlConnection? GetConn() 
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                conn.Dispose();
                MessageBox.Show("Koneksi database gagal: " + ex.Message + "\n\nPastikan PostgreSQL berjalan dan database ChemlinkDB sudah dibuat.", "Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return conn;
        }

        public static void UpdateDatabaseObjects()
        {
            try
            {
                // Try multiple possible locations for the SQL file
                string? sqlFile = FindSqlFile();
                if (sqlFile == null)
                {
                    MessageBox.Show(
                        "Script SQL tidak ditemukan.\nPastikan folder 'Database' dengan file 'Chemlink_Advanced_Objects.sql' berada di root project.",
                        "File SQL Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
        
                string sql = File.ReadAllText(sqlFile);
                using (var conn = GetConn())
                {
                    if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.CommandTimeout = 60;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal update objek database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private static string? FindSqlFile()
        {
            string fileName = "Chemlink_Advanced_Objects.sql";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        
            // Walk up the directory tree to find the Database folder
            var dir = new DirectoryInfo(baseDir);
            while (dir != null)
            {
                string candidate = Path.Combine(dir.FullName, "Database", fileName);
                if (File.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }
        
            return null;
        }
    }
}
