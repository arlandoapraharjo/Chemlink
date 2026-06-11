using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace CHEMLINK.Helpers
{
    public class ConnectDB
    {
        private static string connString =
            "Host=localhost;" +
            "Port=5432;" +
            "Username=postgres;" +
            "Password=adminadmin;" + //pw kalian
            "Database=ChemlinkDB;"; //nama db kalian
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
    }
}
