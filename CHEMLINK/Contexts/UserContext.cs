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
                    string sql = "SELECT * FROM fn_autentikasi_user(@user)";
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
                    string sql = "SELECT * FROM v_user_aktif";
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
                    // Stored procedure handles reactivation of inactive users internally
                    string sql = @"CALL sp_tambah_user(@user, @pass, @role, @fullname, @alamat, @kec, @telp, @email)";
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
                    string sql = @"CALL sp_update_user(@id, @user, @pass, @role, @fullname, @status, @alamat, @telp, @email, @kec)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@user", user.Username);
                        cmd.Parameters.AddWithValue("@pass", string.IsNullOrWhiteSpace(user.Password) ? (object)DBNull.Value : user.Password);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        cmd.Parameters.AddWithValue("@fullname", string.IsNullOrWhiteSpace(user.FullName) ? user.Username : user.FullName);
                        cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(user.Status) ? "Active" : user.Status);
                        cmd.Parameters.AddWithValue("@alamat", string.IsNullOrWhiteSpace(user.Alamat) ? DBNull.Value : user.Alamat);
                        cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(user.NoTelp) ? DBNull.Value : user.NoTelp);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
                        cmd.Parameters.AddWithValue("@kec", string.IsNullOrWhiteSpace(user.Kecamatan) ? DBNull.Value : user.Kecamatan);
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
                    // Stored procedure handles last-admin protection internally
                    string sql = "CALL sp_hapus_user(@id)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToBoolean(dr[0]);
                            }
                        }
                    }
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
    }
}
