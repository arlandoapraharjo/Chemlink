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
                    string sql = "SELECT id_user, username, password, Role, status, alamat, no_telp, email, kota, kecamatan FROM Users WHERE username = @user AND status = 'Active'";

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
                                    return MapUser(dr);
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
                    string sql = "SELECT id_user, username, password, Role, status, alamat, no_telp, email, kota, kecamatan FROM Users WHERE status = 'Active' ORDER BY id_user ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listuser.Add(MapUser(dr));
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
                    string checkSql = "SELECT id_user FROM Users WHERE username = @user AND status = 'Inactive'";
                    using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@user", user.Username);
                        var existingId = checkCmd.ExecuteScalar();

                        if (existingId != null)
                        {
                            // Reactivate the soft-deleted user with new data
                            string reactivateSql = @"UPDATE Users SET password = @pass, Role = @role, status = 'Active',
                                alamat = @alamat, no_telp = @telp, email = @email, kota = @kota, kecamatan = @kec
                                WHERE id_user = @id";
                            using (NpgsqlCommand updateCmd = new NpgsqlCommand(reactivateSql, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@id", Convert.ToInt32(existingId));
                                updateCmd.Parameters.AddWithValue("@pass", user.Password);
                                updateCmd.Parameters.AddWithValue("@role", user.Role);
                                AddDetailParams(updateCmd, user);
                                updateCmd.ExecuteNonQuery();
                            }
                            return;
                        }
                    }

                    // No inactive duplicate — insert as new user
                    string sql = @"INSERT INTO Users (username, password, Role, status, alamat, no_telp, email, kota, kecamatan)
                        VALUES (@user, @pass, @role, 'Active', @alamat, @telp, @email, @kota, @kec)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user.Username);
                        cmd.Parameters.AddWithValue("@pass", user.Password);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        AddDetailParams(cmd, user);
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
                        sql = @"UPDATE Users SET username = @user, password = @pass, Role = @role,
                            alamat = @alamat, no_telp = @telp, email = @email, kota = @kota, kecamatan = @kec
                            WHERE id_user = @id";
                    }
                    else
                    {
                        sql = @"UPDATE Users SET username = @user, Role = @role,
                            alamat = @alamat, no_telp = @telp, email = @email, kota = @kota, kecamatan = @kec
                            WHERE id_user = @id";
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

                        AddDetailParams(cmd, user);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Admin protection: ensure at least 1 admin remains
                    string checkSql = "SELECT Role FROM Users WHERE id_user = @id";
                    using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", id);
                        var role = checkCmd.ExecuteScalar()?.ToString();
                        if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            string countSql = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin' AND status = 'Active'";
                            using (NpgsqlCommand countCmd = new NpgsqlCommand(countSql, conn))
                            {
                                int adminCount = Convert.ToInt32(countCmd.ExecuteScalar());
                                if (adminCount <= 1)
                                    return false; // Cannot delete the last admin
                            }
                        }
                    }

                    string sql = "UPDATE Users SET status = 'Inactive' WHERE id_user = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            return false;
        }

        // Helper: map DataReader row to User object
        private static User MapUser(NpgsqlDataReader dr)
        {
            return new User
            {
                Id = Convert.ToInt32(dr["id_user"]),
                Username = dr["username"].ToString() ?? "",
                Password = dr["password"].ToString() ?? "",
                FullName = dr["username"].ToString() ?? "",
                Role = dr["Role"] != DBNull.Value ? dr["Role"].ToString() ?? "" : "",
                Status = dr["status"] != DBNull.Value ? dr["status"].ToString() ?? "" : "",
                Alamat = dr["alamat"] != DBNull.Value ? dr["alamat"].ToString() ?? "" : "",
                NoTelp = dr["no_telp"] != DBNull.Value ? dr["no_telp"].ToString() ?? "" : "",
                Email = dr["email"] != DBNull.Value ? dr["email"].ToString() ?? "" : "",
                Kota = dr["kota"] != DBNull.Value ? dr["kota"].ToString() ?? "" : "",
                Kecamatan = dr["kecamatan"] != DBNull.Value ? dr["kecamatan"].ToString() ?? "" : ""
            };
        }

        // Helper: add nullable detail parameters
        private static void AddDetailParams(NpgsqlCommand cmd, User user)
        {
            cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(user.Alamat) ? DBNull.Value : user.Alamat);
            cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(user.NoTelp) ? DBNull.Value : user.NoTelp);
            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
            cmd.Parameters.AddWithValue("@kota", string.IsNullOrWhiteSpace(user.Kota) ? DBNull.Value : user.Kota);
            cmd.Parameters.AddWithValue("@kec", string.IsNullOrWhiteSpace(user.Kecamatan) ? DBNull.Value : user.Kecamatan);
        }
    }
}
