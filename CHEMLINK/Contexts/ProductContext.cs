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

            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"
                        SELECT id_produk, nama_produk, harga, keterangan, nama_kategori, nama_supplier, jumlah_stock 
                        FROM v_detail_produk 
                        ORDER BY id_produk ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)) //objek untuk syntax query
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader()) //syntax untuk menjalankan query
                        {
                            int idxId = dr.GetOrdinal("id_produk");
                            int idxName = dr.GetOrdinal("nama_produk");
                            int idxHarga = dr.GetOrdinal("harga");
                            int idxKet = dr.GetOrdinal("keterangan");
                            int idxKat = dr.GetOrdinal("nama_kategori");
                            int idxSup = dr.GetOrdinal("nama_supplier");
                            int idxStock = dr.GetOrdinal("jumlah_stock");

                            while (dr.Read())
                            {
                                Product product = new Product();
                                product.Id = !dr.IsDBNull(idxId) ? dr.GetInt32(idxId) : 0;
                                product.Name = !dr.IsDBNull(idxName) ? dr.GetString(idxName) : "";
                                product.Category = !dr.IsDBNull(idxKat) ? dr.GetString(idxKat) : "";
                                product.Price = !dr.IsDBNull(idxHarga) ? dr.GetInt32(idxHarga) : 0;
                                product.Stock = !dr.IsDBNull(idxStock) ? dr.GetInt32(idxStock) : 0;
                                product.Description = !dr.IsDBNull(idxKet) ? dr.GetString(idxKet) : "";
                                product.SupplierName = !dr.IsDBNull(idxSup) ? dr.GetString(idxSup) : "";

                                listproduct.Add(product);
                            }
                        }
                    }
                }
            }

            return listproduct;
        }

        public void Create(string nama, int idKategori, int stok, decimal harga, string keterangan = "", int idUser = 1)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Insert product with initial stock via stored procedure
                    // (trigger fn_trg_stocks_update auto-logs 'IN' when stock > 0)
                    string sql = "CALL sp_tambah_produk_baru(@nama, @harga, @keterangan, @idKat, @idSup, @idUser, @stok)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@harga", (int)harga);
                        cmd.Parameters.AddWithValue("@keterangan", string.IsNullOrWhiteSpace(keterangan) ? "" : keterangan);
                        cmd.Parameters.AddWithValue("@idKat", idKategori);
                        cmd.Parameters.AddWithValue("@idSup", 1); // Default supplier ID 1
                        cmd.Parameters.AddWithValue("@idUser", idUser);
                        cmd.Parameters.AddWithValue("@stok", stok);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(int id, string nama, int idKategori, int stok, decimal harga, string keterangan = "")
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Update product details + stock via stored procedure
                    // (trigger fn_trg_produk_name_change auto-cascades name to log_stok)
                    string sql = "CALL sp_update_produk(@id, @nama, @idKat, @harga, @keterangan, @stok)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@idKat", idKategori);
                        cmd.Parameters.AddWithValue("@harga", (int)harga);
                        cmd.Parameters.AddWithValue("@keterangan", string.IsNullOrWhiteSpace(keterangan) ? "" : keterangan);
                        cmd.Parameters.AddWithValue("@stok", stok);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Delete product + stock via stored procedure (handles FK order)
                    string sql = "CALL sp_hapus_produk(@id)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public System.Data.DataTable GetCriticalStockTable()
        {
            var dt = new System.Data.DataTable();
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_produk, nama_produk, jumlah_stock FROM v_stok_kritis ORDER BY id_produk ASC";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public List<StockKritis> ReadCriticalStock()
        {
            List<StockKritis> listStokKritis = new List<StockKritis>();

            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"
                        SELECT id_produk, nama_produk, jumlah_stock 
                        FROM v_stok_kritis 
                        ORDER BY jumlah_stock ASC, id_produk ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            int idxId = dr.GetOrdinal("id_produk");
                            int idxName = dr.GetOrdinal("nama_produk");
                            int idxStock = dr.GetOrdinal("jumlah_stock");

                            while (dr.Read())
                            {
                                StockKritis stok = new StockKritis();
                                stok.IdProduk = !dr.IsDBNull(idxId) ? dr.GetInt32(idxId) : 0;
                                stok.NamaProduk = !dr.IsDBNull(idxName) ? dr.GetString(idxName) : "";
                                stok.JumlahStock = !dr.IsDBNull(idxStock) ? dr.GetInt32(idxStock) : 0;

                                listStokKritis.Add(stok);
                            }
                        }
                    }
                }
            }

            return listStokKritis;
        }
    }
}
