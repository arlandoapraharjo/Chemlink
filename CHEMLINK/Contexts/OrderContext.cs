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

                    using (var tx = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in cart)
                            {
                                // Record order detail. The stored procedure sp_catat_pesanan_masuk
                                // is expected to insert order/order_details and update Stocks accordingly.
                                string sql = "CALL sp_catat_pesanan_masuk(@tanggal::DATE, @keterangan, @inputBy, @idProduk, @jumlahMasuk, @noFaktur, @catatan)";
                                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn, tx))
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

                            tx.Commit();
                        }
                        catch
                        {
                            try { tx.Rollback(); } catch { }
                            throw;
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

        public DataTable GetCategoryBreakdown()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    string sql = @"SELECT 
                        k.nama_kategori AS ""Kategori"",
                        SUM(od.jumlah_masuk)::int AS ""Qty Terjual"",
                        (SUM(od.jumlah_masuk * p.harga))::int AS ""Total Pendapatan""
                    FROM order_details od
                    JOIN Produk p ON od.id_produk = p.id_produk
                    JOIN Kategori k ON p.id_kategori = k.id_kategori
                    GROUP BY k.nama_kategori
                    ORDER BY SUM(od.jumlah_masuk * p.harga) DESC";
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
