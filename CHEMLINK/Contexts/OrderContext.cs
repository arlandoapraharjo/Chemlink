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
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    DateTime today = DateTime.Now;

                    using (var tx = conn.BeginTransaction())
                    {
                        try
                        {
                            // Insert selling header
                            int idSelling;
                            using (var cmdHead = new NpgsqlCommand(
                                "INSERT INTO selling (tanggal_selling, keterangan, id_kasir) VALUES (@tgl, @ket, @kasir) RETURNING id_selling",
                                conn, tx))
                            {
                                cmdHead.Parameters.AddWithValue("@tgl", today.Date);
                                cmdHead.Parameters.AddWithValue("@ket", keterangan);
                                cmdHead.Parameters.AddWithValue("@kasir", inputByUserId);
                                idSelling = (int)cmdHead.ExecuteScalar()!;
                            }

                            // Insert selling details + update stock + log for each item
                            foreach (var item in cart)
                            {
                                // Insert detail
                                using (var cmdDet = new NpgsqlCommand(
                                    "INSERT INTO selling_details (jumlah_keluar, id_selling, id_produk) VALUES (@jml, @idSell, @idProd)",
                                    conn, tx))
                                {
                                    cmdDet.Parameters.AddWithValue("@jml", item.Qty);
                                    cmdDet.Parameters.AddWithValue("@idSell", idSelling);
                                    cmdDet.Parameters.AddWithValue("@idProd", item.ProductId);
                                    cmdDet.ExecuteNonQuery();
                                }

                                // Decrement stock
                                using (var cmdStock = new NpgsqlCommand(
                                    "UPDATE Stocks SET jumlah_stock = jumlah_stock - @jml, timestamp = CURRENT_TIMESTAMP WHERE id_produk = @idProd",
                                    conn, tx))
                                {
                                    cmdStock.Parameters.AddWithValue("@jml", item.Qty);
                                    cmdStock.Parameters.AddWithValue("@idProd", item.ProductId);
                                    cmdStock.ExecuteNonQuery();
                                }

                                // Log stock activity
                                using (var cmdLog = new NpgsqlCommand(
                                    "INSERT INTO log_stok (tipe_aktivitas, id_user, nama_user, nama_produk, jumlah) SELECT 'OUT', @idUser, u.username, p.nama_produk, @jml FROM Users u, Produk p WHERE u.id_user = @idUser AND p.id_produk = @idProd",
                                    conn, tx))
                                {
                                    cmdLog.Parameters.AddWithValue("@idUser", inputByUserId);
                                    cmdLog.Parameters.AddWithValue("@jml", item.Qty);
                                    cmdLog.Parameters.AddWithValue("@idProd", item.ProductId);
                                    cmdLog.ExecuteNonQuery();
                                }
                            }
                            tx.Commit();
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public DataTable GetFinancialReport()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
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
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    string sql = @"SELECT 
                        k.nama_kategori AS ""Kategori"",
                        SUM(sd.jumlah_keluar)::int AS ""Qty Terjual"",
                        (SUM(sd.jumlah_keluar * p.harga))::int AS ""Total Pendapatan""
                    FROM selling_details sd
                    JOIN Produk p ON sd.id_produk = p.id_produk
                    JOIN Kategori k ON p.id_kategori = k.id_kategori
                    GROUP BY k.nama_kategori
                    ORDER BY SUM(sd.jumlah_keluar * p.harga) DESC";
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
        public DataTable GetStockLog()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    string sql = "SELECT * FROM v_log_stok";
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
