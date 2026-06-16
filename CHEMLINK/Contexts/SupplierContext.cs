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

            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_supplier, nama_perusahaan, kontak_person, no_telp, email, alamat_supplier, kota_supplier, status FROM Supplier ORDER BY id_supplier ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            int idxId = dr.GetOrdinal("id_supplier");
                            int idxName = dr.GetOrdinal("nama_perusahaan");
                            int idxKontak = dr.GetOrdinal("kontak_person");
                            int idxTelp = dr.GetOrdinal("no_telp");
                            int idxEmail = dr.GetOrdinal("email");
                            int idxAlamat = dr.GetOrdinal("alamat_supplier");
                            int idxKota = dr.GetOrdinal("kota_supplier");
                            int idxStat = dr.GetOrdinal("status");

                            while (dr.Read())
                            {
                                listsupplier.Add(new Supplier
                                {
                                    Id = !dr.IsDBNull(idxId) ? dr.GetInt32(idxId) : 0,
                                    Name = !dr.IsDBNull(idxName) ? dr.GetString(idxName) : "",
                                    KontakPerson = !dr.IsDBNull(idxKontak) ? dr.GetString(idxKontak) : "",
                                    Phone = !dr.IsDBNull(idxTelp) ? dr.GetString(idxTelp) : "",
                                    Email = !dr.IsDBNull(idxEmail) ? dr.GetString(idxEmail) : "",
                                    Address = !dr.IsDBNull(idxAlamat) ? dr.GetString(idxAlamat) : "",
                                    Kota = !dr.IsDBNull(idxKota) ? dr.GetString(idxKota) : "",
                                    Status = !dr.IsDBNull(idxStat) ? dr.GetString(idxStat) : ""
                                });
                            }
                        }
                    }
                }
            }

            return listsupplier;
        }

        public Supplier? GetById(int id)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_supplier, nama_perusahaan, kontak_person, no_telp, email, alamat_supplier, kota_supplier, status FROM Supplier WHERE id_supplier = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return MapSupplier(dr);
                            }
                        }
                    }
                }
            }
            return null;
        }

        public void Create(Supplier supplier)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "CALL sp_tambah_supplier(@nama, @kontak, @telp, @email, @alamat, @kota)";
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
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = @"UPDATE Supplier SET nama_perusahaan = @nama, kontak_person = @kontak,
                        no_telp = @telp, email = @email, alamat_supplier = @alamat, kota_supplier = @kota, status = @status
                        WHERE id_supplier = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", supplier.Name ?? "");
                        cmd.Parameters.AddWithValue("@kontak", string.IsNullOrWhiteSpace(supplier.KontakPerson) ? DBNull.Value : supplier.KontakPerson);
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(supplier.Phone) ? DBNull.Value : supplier.Phone);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(supplier.Email) ? DBNull.Value : supplier.Email);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(supplier.Address) ? DBNull.Value : supplier.Address);
                        cmd.Parameters.AddWithValue("@kota", string.IsNullOrWhiteSpace(supplier.Kota) ? DBNull.Value : supplier.Kota);
                        cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(supplier.Status) ? "Aktif" : supplier.Status);
                        cmd.Parameters.AddWithValue("@id", id);
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
            int idxId = dr.GetOrdinal("id_supplier");
            int idxName = dr.GetOrdinal("nama_perusahaan");
            int idxKontak = dr.GetOrdinal("kontak_person");
            int idxTelp = dr.GetOrdinal("no_telp");
            int idxEmail = dr.GetOrdinal("email");
            int idxAlamat = dr.GetOrdinal("alamat_supplier");
            int idxKota = dr.GetOrdinal("kota_supplier");
            int idxStat = dr.GetOrdinal("status");

            return new Supplier
            {
                Id = !dr.IsDBNull(idxId) ? dr.GetInt32(idxId) : 0,
                Name = !dr.IsDBNull(idxName) ? dr.GetString(idxName) : "",
                KontakPerson = !dr.IsDBNull(idxKontak) ? dr.GetString(idxKontak) : "",
                Phone = !dr.IsDBNull(idxTelp) ? dr.GetString(idxTelp) : "",
                Email = !dr.IsDBNull(idxEmail) ? dr.GetString(idxEmail) : "",
                Address = !dr.IsDBNull(idxAlamat) ? dr.GetString(idxAlamat) : "",
                Kota = !dr.IsDBNull(idxKota) ? dr.GetString(idxKota) : "",
                Status = !dr.IsDBNull(idxStat) ? dr.GetString(idxStat) : ""
            };
        }
    }
}
