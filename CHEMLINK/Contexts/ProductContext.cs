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
                        SELECT id_produk, nama_produk, harga, tanggal_exp, keterangan, nama_kategori, nama_supplier, jumlah_stock 
                        FROM v_detail_produk 
                        ORDER BY id_produk ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)) //objek untuk syntax query
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader()) //syntax untuk menjalankan query
                        {
                            int idxId = dr.GetOrdinal("id_produk");
                            int idxName = dr.GetOrdinal("nama_produk");
                            int idxHarga = dr.GetOrdinal("harga");
                            int idxExp = dr.GetOrdinal("tanggal_exp");
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
                                product.ExpiryDate = !dr.IsDBNull(idxExp) ? dr.GetDateTime(idxExp) : (DateTime?)null;
                                product.SupplierName = !dr.IsDBNull(idxSup) ? dr.GetString(idxSup) : "";

                                listproduct.Add(product);
                            }
                        }
                    }
                }
            }

            return listproduct;
        }

        public void Create(string nama, int idKategori, int stok, decimal harga, string keterangan = "", DateTime? tanggalExp = null)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Step 1: Insert product via stored procedure (creates Stocks row with 0)
                    string sql = "CALL sp_tambah_produk_baru(@nama, @harga, @tglExp, @keterangan, @idKat, @idSup)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@harga", (int)harga);
                        cmd.Parameters.AddWithValue("@tglExp", tanggalExp.HasValue ? (object)tanggalExp.Value.Date : DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@keterangan", string.IsNullOrWhiteSpace(keterangan) ? "" : keterangan);
                        cmd.Parameters.AddWithValue("@idKat", idKategori);
                        cmd.Parameters.AddWithValue("@idSup", 1); // Default supplier ID 1
                        cmd.ExecuteNonQuery();
                    }

                    // Step 2: Update the stock to the actual value for the newly created product
                    string sqlUpdateStock = @"
                        UPDATE Stocks 
                        SET jumlah_stock = @stok 
                        WHERE id_produk = (
                            SELECT id_produk FROM Produk 
                            WHERE nama_produk = @nama 
                            ORDER BY id_produk DESC LIMIT 1
                        )";
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(sqlUpdateStock, conn))
                    {
                        cmd2.Parameters.AddWithValue("@nama", nama);
                        cmd2.Parameters.AddWithValue("@stok", stok);
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(int id, string nama, int idKategori, int stok, decimal harga, string keterangan = "", DateTime? tanggalExp = null)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Step 1: Update Produk table (no jumlah_stock column here)
                    string sqlProduk = @"UPDATE Produk 
                        SET nama_produk = @nama, id_kategori = @idKat, harga = @harga,
                            tanggal_exp = @tglExp, keterangan = @keterangan
                        WHERE id_produk = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlProduk, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@idKat", idKategori);
                        cmd.Parameters.AddWithValue("@harga", (int)harga);
                        cmd.Parameters.AddWithValue("@tglExp", tanggalExp.HasValue ? (object)tanggalExp.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@keterangan", string.IsNullOrWhiteSpace(keterangan) ? "" : keterangan);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    // Step 2: Update stock in Stocks table
                    string sqlStock = @"UPDATE Stocks 
                        SET jumlah_stock = @stok, time_stamp = CURRENT_TIMESTAMP
                        WHERE id_produk = @id";
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(sqlStock, conn))
                    {
                        cmd2.Parameters.AddWithValue("@stok", stok);
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.ExecuteNonQuery();
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
                    // Step 1: Delete from Stocks first (FK constraint)
                    string sqlStock = "DELETE FROM Stocks WHERE id_produk = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStock, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    // Step 2: Delete from Produk
                    string sqlProduk = "DELETE FROM Produk WHERE id_produk = @id";
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(sqlProduk, conn))
                    {
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.ExecuteNonQuery();
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
