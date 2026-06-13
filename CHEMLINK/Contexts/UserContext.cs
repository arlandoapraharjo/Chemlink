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
                    string sql = "SELECT id_user, username, password, Role FROM \"User\" WHERE username = @user AND status = 'Active'";

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
                    string sql = "SELECT id_user, username, password, Role FROM \"User\" WHERE status = 'Active' ORDER BY id_user ASC";

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
                    // Check if an inactive user with the same username exists
                    string checkSql = "SELECT id_user FROM \"User\" WHERE username = @user AND status = 'Inactive'";
                    using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@user", user.Username);
                        var existingId = checkCmd.ExecuteScalar();

                        if (existingId != null)
                        {
                            // Reactivate the soft-deleted user with new data
                            string reactivateSql = "UPDATE \"User\" SET password = @pass, Role = @role, status = 'Active' WHERE id_user = @id";
                            using (NpgsqlCommand updateCmd = new NpgsqlCommand(reactivateSql, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@id", Convert.ToInt32(existingId));
                                updateCmd.Parameters.AddWithValue("@pass", user.Password);
                                updateCmd.Parameters.AddWithValue("@role", user.Role);
                                updateCmd.ExecuteNonQuery();
                            }
                            return;
                        }
                    }

                    // No inactive duplicate — insert as new user
                    string sql = "INSERT INTO \"User\" (username, password, Role, status) VALUES (@user, @pass, @role, 'Active')";
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
                    string sql = "UPDATE \"User\" SET status = 'Inactive' WHERE id_user = @id";
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
