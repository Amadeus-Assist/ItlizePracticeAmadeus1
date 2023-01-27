using DAL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Layer
{
    public class DAL_LayerClass
    {
        private string _connStr;
        public DAL_LayerClass(string ConnStr) 
        {
            _connStr = ConnStr;
        }

        public bool AddUser(UserModel newUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO dbo.USERINFO (USERNAME, PASSWORD, EMAIL) VALUES(@username, @password, @email)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", newUser.UserName);
                        cmd.Parameters.AddWithValue("@password", newUser.Password);
                        cmd.Parameters.AddWithValue("@email", newUser.Email);
                        int result = cmd.ExecuteNonQuery();
                        if (result < 0)
                        {
                            conn.Close();
                            return false;
                        }
                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.USERINFO";
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(ds);
                    }
                    conn.Close() ;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return users;
            }
            foreach (DataTable table in ds.Tables)
            {
                var query = from p in table.AsEnumerable()
                            select new
                            {
                                ID = p.Field<int>("ID"),
                                UserName = p.Field<string>("USERNAME"),
                                Password = p.Field<string>("PASSWORD"),
                                Email = p.Field<string>("EMAIL")
                            };
                foreach (var item in query)
                {
                    var user = new UserModel
                    {
                        Id = item.ID,
                        UserName = item.UserName,
                        Password = item.Password,
                        Email = item.Email
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public UserModel GetUserFromUserName(string userName)
        {
            UserModel user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.USERINFO WHERE USERNAME=@userName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@userName", userName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            user = new UserModel();
                            reader.Read();
                            user.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            user.UserName = reader.GetString(reader.GetOrdinal("USERNAME"));
                            user.Password = reader.GetString(reader.GetOrdinal("PASSWORD"));
                            user.Email = reader.GetString(reader.GetOrdinal("EMAIL"));
                        }
                    }
                    conn.Close();
                }
            }  catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return user;
            }
            return user;
        }
    }
}
