using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    class SupplierContext
    {
        public List<Supplier> Read()
        {
            List<Supplier> listsupplier = new List<Supplier>();

            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_supplier, nama_perusahaan, kontak_person, no_telp, email, alamat_supplier, kota_supplier, status FROM Supplier";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listsupplier.Add(MapSupplier(dr));
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
                    string sql = @"INSERT INTO Supplier (nama_perusahaan, kontak_person, no_telp, email, alamat_supplier, kota_supplier, status)
                        VALUES (@nama, @kontak, @telp, @email, @alamat, @kota, 'Active')";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", supplier.Name ?? "");
                        cmd.Parameters.AddWithValue("@kontak", string.IsNullOrWhiteSpace(supplier.KontakPerson) ? DBNull.Value : supplier.KontakPerson);
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(supplier.Phone) ? DBNull.Value : supplier.Phone);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(supplier.Email) ? DBNull.Value : supplier.Email);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(supplier.Address) ? DBNull.Value : supplier.Address);
                        cmd.Parameters.AddWithValue("@kota", string.IsNullOrWhiteSpace(supplier.Kota) ? DBNull.Value : supplier.Kota);
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
                    string sql = @"UPDATE Supplier SET nama_perusahaan = @nama, kontak_person = @kontak,
                        no_telp = @telp, email = @email, alamat_supplier = @alamat, kota_supplier = @kota
                        WHERE id_supplier = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", supplier.Name ?? "");
                        cmd.Parameters.AddWithValue("@kontak", string.IsNullOrWhiteSpace(supplier.KontakPerson) ? DBNull.Value : supplier.KontakPerson);
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(supplier.Phone) ? DBNull.Value : supplier.Phone);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(supplier.Email) ? DBNull.Value : supplier.Email);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(supplier.Address) ? DBNull.Value : supplier.Address);
                        cmd.Parameters.AddWithValue("@kota", string.IsNullOrWhiteSpace(supplier.Kota) ? DBNull.Value : supplier.Kota);
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

        private static Supplier MapSupplier(NpgsqlDataReader dr)
        {
            return new Supplier
            {
                Id = Convert.ToInt32(dr["id_supplier"]),
                Name = dr["nama_perusahaan"].ToString() ?? "",
                KontakPerson = dr["kontak_person"] != DBNull.Value ? dr["kontak_person"].ToString() ?? "" : "",
                Phone = dr["no_telp"] != DBNull.Value ? dr["no_telp"].ToString() ?? "" : "",
                Email = dr["email"] != DBNull.Value ? dr["email"].ToString() ?? "" : "",
                Address = dr["alamat_supplier"] != DBNull.Value ? dr["alamat_supplier"].ToString() ?? "" : "",
                Kota = dr["kota_supplier"] != DBNull.Value ? dr["kota_supplier"].ToString() ?? "" : "",
                Status = dr["status"] != DBNull.Value ? dr["status"].ToString() ?? "" : ""
            };
        }
    }
}
