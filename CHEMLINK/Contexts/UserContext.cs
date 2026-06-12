using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    public class UserContext
    {
        public User? AuthenticateUser(string username, string password)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_user, username, password, Role FROM \"User\" WHERE username = @user";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string dbPass = dr["password"].ToString() ?? "";
                                if (dbPass == password)
                                {
                                    return new User
                                    {
                                        Id = Convert.ToInt32(dr["id_user"]),
                                        Username = dr["username"].ToString() ?? "",
                                        FullName = dr["username"].ToString() ?? "",
                                        Role = dr["Role"] != DBNull.Value ? dr["Role"].ToString() ?? "" : ""
                                    };
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public List<User> Read()
        {
            List<User> listuser = new List<User>();

            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_user, username, password, Role FROM \"User\" ORDER BY id_user ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                User user = new User();
                                user.Id = Convert.ToInt32(dr["id_user"]);
                                user.Username = dr["username"].ToString() ?? "";
                                user.Password = dr["password"].ToString() ?? "";
                                user.FullName = dr["username"].ToString() ?? "";
                                user.Role = dr["Role"] != DBNull.Value ? dr["Role"].ToString() ?? "" : "";

                                listuser.Add(user);
                            }
                        }
                    }
                }
            }
            return listuser;
        }

        public void Create(User user)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "CALL sp_tambah_user(@user, @pass, @role)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user.Username);
                        cmd.Parameters.AddWithValue("@pass", user.Password);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(User user)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql;
                    if (!string.IsNullOrWhiteSpace(user.Password))
                    {
                        sql = "UPDATE \"User\" SET username = @user, password = @pass, Role = @role WHERE id_user = @id";
                    }
                    else
                    {
                        sql = "UPDATE \"User\" SET username = @user, Role = @role WHERE id_user = @id";
                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@user", user.Username);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        
                        if (!string.IsNullOrWhiteSpace(user.Password))
                        {
                            cmd.Parameters.AddWithValue("@pass", user.Password);
                        }
                        
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
                    // Menggunakan Stored Procedure untuk Soft Delete
                    string sql = "CALL sp_update_status_user(@id, 'Inactive')";
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
