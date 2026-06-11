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
                        SELECT p.id_produk, p.nama_produk, k.nama_kategori, p.harga, s.jumlah_stock 
                        FROM Produk p 
                        LEFT JOIN Kategori k ON p.id_kategori = k.id_kategori
                        LEFT JOIN Stocks s ON p.id_produk = s.id_produk 
                        ORDER BY p.id_produk ASC";

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
    }
}
