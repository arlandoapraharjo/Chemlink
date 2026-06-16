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
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_user, username, password, Role, fullname, status, alamat, no_telp, email, kecamatan FROM Users WHERE username = @user AND status = 'Active'";

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

            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_user, username, password, Role, fullname, status, alamat, no_telp, email, kecamatan FROM Users WHERE status = 'Active' ORDER BY id_user ASC";

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
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
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
                            string reactivateSql = @"UPDATE Users SET password = @pass, Role = @role, fullname = @fullname, status = 'Active',
                                alamat = @alamat, no_telp = @telp, email = @email, kecamatan = @kec
                                WHERE id_user = @id";
                            using (NpgsqlCommand updateCmd = new NpgsqlCommand(reactivateSql, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@id", Convert.ToInt32(existingId));
                                updateCmd.Parameters.AddWithValue("@pass", user.Password);
                                updateCmd.Parameters.AddWithValue("@role", user.Role);
                                updateCmd.Parameters.AddWithValue("@fullname", string.IsNullOrWhiteSpace(user.FullName) ? user.Username : user.FullName);
                                AddDetailParams(updateCmd, user);
                                updateCmd.ExecuteNonQuery();
                            }
                            return;
                        }
                    }

                    // No inactive duplicate — insert as new user using stored procedure sp_tambah_user
                    string sql = "CALL sp_tambah_user(@user, @pass, @role, @fullname, @alamat, @kec, @telp, @email)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user.Username);
                        cmd.Parameters.AddWithValue("@pass", user.Password);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        cmd.Parameters.AddWithValue("@fullname", string.IsNullOrWhiteSpace(user.FullName) ? user.Username : user.FullName);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(user.Alamat) ? DBNull.Value : user.Alamat);
                        cmd.Parameters.AddWithValue("@kec", string.IsNullOrWhiteSpace(user.Kecamatan) ? DBNull.Value : user.Kecamatan);
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(user.NoTelp) ? DBNull.Value : user.NoTelp);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(User user)
        {
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql;
                    if (!string.IsNullOrWhiteSpace(user.Password))
                    {
                        sql = @"UPDATE Users SET username = @user, password = @pass, Role = @role, fullname = @fullname, status = @status,
                            alamat = @alamat, no_telp = @telp, email = @email, kecamatan = @kec
                            WHERE id_user = @id";
                    }
                    else
                    {
                        sql = @"UPDATE Users SET username = @user, Role = @role, fullname = @fullname, status = @status,
                            alamat = @alamat, no_telp = @telp, email = @email, kecamatan = @kec
                            WHERE id_user = @id";
                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@user", user.Username);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        cmd.Parameters.AddWithValue("@fullname", string.IsNullOrWhiteSpace(user.FullName) ? user.Username : user.FullName);
                        cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(user.Status) ? "Active" : user.Status);

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
            using (NpgsqlConnection? conn = ConnectDB.GetConn())
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

                    string sql = "CALL sp_update_status_user(@id, 'Inactive')";
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
            int idxId = dr.GetOrdinal("id_user");
            int idxUser = dr.GetOrdinal("username");
            int idxPass = dr.GetOrdinal("password");
            int idxFull = dr.GetOrdinal("fullname");
            int idxRole = dr.GetOrdinal("Role");
            int idxStat = dr.GetOrdinal("status");
            int idxAlamat = dr.GetOrdinal("alamat");
            int idxTelp = dr.GetOrdinal("no_telp");
            int idxEmail = dr.GetOrdinal("email");
            int idxKec = dr.GetOrdinal("kecamatan");

            return new User
            {
                Id = !dr.IsDBNull(idxId) ? dr.GetInt32(idxId) : 0,
                Username = !dr.IsDBNull(idxUser) ? dr.GetString(idxUser) : "",
                Password = !dr.IsDBNull(idxPass) ? dr.GetString(idxPass) : "",
                FullName = !dr.IsDBNull(idxFull) ? dr.GetString(idxFull) : (!dr.IsDBNull(idxUser) ? dr.GetString(idxUser) : ""),
                Role = !dr.IsDBNull(idxRole) ? dr.GetString(idxRole) : "",
                Status = !dr.IsDBNull(idxStat) ? dr.GetString(idxStat) : "",
                Alamat = !dr.IsDBNull(idxAlamat) ? dr.GetString(idxAlamat) : "",
                NoTelp = !dr.IsDBNull(idxTelp) ? dr.GetString(idxTelp) : "",
                Email = !dr.IsDBNull(idxEmail) ? dr.GetString(idxEmail) : "",
                Kecamatan = !dr.IsDBNull(idxKec) ? dr.GetString(idxKec) : ""
            };
        }

        // Helper: add nullable detail parameters
        private static void AddDetailParams(NpgsqlCommand cmd, User user)
        {
            cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(user.Alamat) ? DBNull.Value : user.Alamat);
            cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(user.NoTelp) ? DBNull.Value : user.NoTelp);
            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
            cmd.Parameters.AddWithValue("@kec", string.IsNullOrWhiteSpace(user.Kecamatan) ? DBNull.Value : user.Kecamatan);
        }
    }
}
