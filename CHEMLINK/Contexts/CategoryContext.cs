using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    public class CategoryContext
    {
        public List<Category> Read()
        {
            List<Category> list = new List<Category>();

            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_kategori, nama_kategori FROM Kategori ORDER BY id_kategori ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            int idxId = dr.GetOrdinal("id_kategori");
                            int idxName = dr.GetOrdinal("nama_kategori");

                            while (dr.Read())
                            {
                                Category cat = new Category
                                {
                                    Id = !dr.IsDBNull(idxId) ? dr.GetInt32(idxId) : 0,
                                    Name = !dr.IsDBNull(idxName) ? dr.GetString(idxName) : ""
                                };
                                list.Add(cat);
                            }
                        }
                    }
                }
            }

            return list;
        }

        public void Create(string name)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "INSERT INTO Kategori (nama_kategori) VALUES (@name)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(int id, string name)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "UPDATE Kategori SET nama_kategori = @name WHERE id_kategori = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
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
                    string sql = "DELETE FROM Kategori WHERE id_kategori = @id";
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
