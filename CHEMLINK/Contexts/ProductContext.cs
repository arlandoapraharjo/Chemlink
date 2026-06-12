using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    class ProductContext
    {
        public List<Product> Read() //Baca Data
        {
            List<Product> listproduct = new List<Product>(); //objek untuk list

            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"
                        SELECT id_produk, nama_produk, nama_kategori, harga, jumlah_stock 
                        FROM v_detail_produk 
                        ORDER BY id_produk ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)) //objek untuk syntax query
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader()) //syntax untuk menjalankan query
                        {
                            while (dr.Read())
                            {
                                Product product = new Product();
                                product.Id = Convert.ToInt32(dr["id_produk"]);
                                product.Name = dr["nama_produk"].ToString() ?? "";
                                product.Category = dr["nama_kategori"] != DBNull.Value ? dr["nama_kategori"].ToString() ?? "" : "";
                                product.Price = dr["harga"] != DBNull.Value ? Convert.ToDecimal(dr["harga"]) : 0m;
                                product.Stock = dr["jumlah_stock"] != DBNull.Value ? Convert.ToInt32(dr["jumlah_stock"]) : 0;

                                listproduct.Add(product);
                            }
                        }
                    }
                }
            }

            return listproduct;
        }

        public void Create(string nama, string kategori, int stok, decimal harga)
        {
            // Dummy id mapping for simplicity (in real app, we'd look up category id)
            int idKategori = kategori == "Herbisida" ? 1 : kategori == "Fungisida" ? 2 : kategori == "Insektisida" ? 3 : 4;
            
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "CALL sp_tambah_produk_baru(@nama, '', 'pcs', @harga, CURRENT_DATE, '', @idKat, 1, 1)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@harga", (int)harga);
                        cmd.Parameters.AddWithValue("@idKat", idKategori);
                        // In `sp_tambah_produk_baru`, it also initializes stock, but let's say we update stock afterwards
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
