using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    class SupplierContext
    {
        public List<Supplier> Read() //Baca Data
        {
            List<Supplier> listsupplier = new List<Supplier>(); //objek untuk list

            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_supplier, nama_perusahaan, no_telp, kota_supplier FROM Supplier";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)) //objek untuk syntax query
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader()) //syntax untuk menjalankan query
                        {
                            while (dr.Read())
                            {
                                Supplier supplier = new Supplier();
                                supplier.Id = Convert.ToInt32(dr["id_supplier"]);
                                supplier.Name = dr["nama_perusahaan"].ToString() ?? "";
                                supplier.Phone = dr["no_telp"] != DBNull.Value ? dr["no_telp"].ToString() ?? "" : "";
                                supplier.Address = dr["kota_supplier"] != DBNull.Value ? dr["kota_supplier"].ToString() ?? "" : "";

                                listsupplier.Add(supplier);
                            }
                        }
                    }
                }
            }

            return listsupplier;
        }

        public void Create(Supplier supplier)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "CALL sp_tambah_supplier(@nama, @kontak, @telp, @email, @alamat, @kota)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", supplier.Name ?? "");
                        cmd.Parameters.AddWithValue("@kontak", DBNull.Value);
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(supplier.Phone) ? DBNull.Value : supplier.Phone);
                        cmd.Parameters.AddWithValue("@email", DBNull.Value);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(supplier.Address) ? DBNull.Value : supplier.Address);
                        // ensure city/kota parameter is also populated from Address so kota_supplier is set by the procedure
                        cmd.Parameters.AddWithValue("@kota", string.IsNullOrWhiteSpace(supplier.Address) ? DBNull.Value : supplier.Address);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(int id, Supplier supplier)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"UPDATE Supplier SET nama_perusahaan = @nama, no_telp = @telp, kota_supplier = @alamat WHERE id_supplier = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", supplier.Name ?? "");
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(supplier.Phone) ? DBNull.Value : supplier.Phone);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(supplier.Address) ? DBNull.Value : supplier.Address);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "DELETE FROM Supplier WHERE id_supplier = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
