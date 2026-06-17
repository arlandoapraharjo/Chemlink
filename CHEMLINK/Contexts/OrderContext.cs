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
                    // Build arrays for batch checkout stored procedure
                    // (trigger fn_trg_selling_detail auto-decrements stock + auto-logs OUT)
                    int[] productIds = new int[cart.Count];
                    int[] quantities = new int[cart.Count];
                    for (int i = 0; i < cart.Count; i++)
                    {
                        productIds[i] = cart[i].ProductId;
                        quantities[i] = cart[i].Qty;
                    }

                    using (var cmd = new NpgsqlCommand("CALL sp_checkout(@tgl, @ket, @kasir, @prods, @qtys)", conn))
                    {
                        cmd.Parameters.AddWithValue("@tgl", DateOnly.FromDateTime(DateTime.Now));
                        cmd.Parameters.AddWithValue("@ket", keterangan);
                        cmd.Parameters.AddWithValue("@kasir", inputByUserId);
                        cmd.Parameters.AddWithValue("@prods", productIds);
                        cmd.Parameters.AddWithValue("@qtys", quantities);
                        cmd.ExecuteNonQuery();
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
                    string sql = "SELECT * FROM v_penjualan_per_kategori";
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
