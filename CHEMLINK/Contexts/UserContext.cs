using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Helpers;

namespace CHEMLINK.Contexts
{
    class UserContext
    {
        public List<User> Read() //Baca Data
        {
            List<User> listuser = new List<User>(); //objek untuk list

            using (NpgsqlConnection conn = ConnectDB.GetConn())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "SELECT id_user, username, Role FROM \"User\" ORDER BY id_user ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)) //objek untuk syntax query
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader()) //syntax untuk menjalankan query
                        {
                            while (dr.Read())
                            {
                                 User user = new User();
                                user.Username = dr["username"].ToString() ?? "";
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
    }
}
