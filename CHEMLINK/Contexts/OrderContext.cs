using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    public class OrderContext
    {
        public void Checkout(List<CartItem> cart, int inputByUserId, string keterangan)
        {
            if (cart == null || cart.Count == 0) return;

            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    // Generate a common factor/invoice number
                    string noFaktur = "INV-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    DateTime today = DateTime.Now;

                    foreach (var item in cart)
                    {
                        string sql = "CALL sp_catat_pesanan_masuk(@tanggal::DATE, @keterangan, @inputBy, @idProduk, @jumlahMasuk, @noFaktur, @catatan)";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@tanggal", today.Date);
                            cmd.Parameters.AddWithValue("@keterangan", keterangan);
                            cmd.Parameters.AddWithValue("@inputBy", inputByUserId);
                            cmd.Parameters.AddWithValue("@idProduk", item.ProductId);
                            cmd.Parameters.AddWithValue("@jumlahMasuk", item.Qty);
                            cmd.Parameters.AddWithValue("@noFaktur", noFaktur);
                            cmd.Parameters.AddWithValue("@catatan", $"Pesanan produk {item.ProductName}");
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public DataTable GetFinancialReport()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    string sql = "SELECT * FROM v_laporan_keuangan";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            return dt;
        }
    }
}
