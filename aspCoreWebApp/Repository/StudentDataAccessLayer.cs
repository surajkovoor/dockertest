using aspCoreWebApp.Utility;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using aspCoreWebApp.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
namespace aspCoreWebApp.Repository

{
    public class StudentDataAccessLayer
    {
        private string connectionString = Utility.ConnectionString.ConnectionStringUser;

        public IEnumerable<User> GetAllUser()
        {
            List<User> lstUser = new List<User>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_display", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User student = new User();
                    student.PersonId = Convert.ToInt32(rdr["PersonId"]);
                    student.name = rdr["name"].ToString();
                    student.email = rdr["email"].ToString();
                    student.contact = rdr["contact"].ToString();
                    student.password = rdr["pasword"].ToString();


                    lstUser.Add(student);
                }
                con.Close();
            }
            return lstUser;
        }
        public void AddUser(User usr)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_user_insert", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string strpassword = Encryptdata(usr.password);
                cmd.Parameters.AddWithValue("@name", usr.name);
                cmd.Parameters.AddWithValue("@email", usr.email);
                cmd.Parameters.AddWithValue("@contact", usr.contact);
                cmd.Parameters.AddWithValue("@password", (strpassword));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private string Encryptdata(string? password)
        {
            string strmsg = string.Empty;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            byte[] encode = new byte[password.Length];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public void UpdateUser(User usr)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string strpassword = Encryptdata(usr.password);
                cmd.Parameters.AddWithValue("@PersonId", usr.PersonId);
                cmd.Parameters.AddWithValue("@name", usr.name);
                cmd.Parameters.AddWithValue("@email", usr.email);
                cmd.Parameters.AddWithValue("@contact", usr.contact);
                cmd.Parameters.AddWithValue("@password", strpassword);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public User GetUserData(int? PersonId)
        {
            User usr = new User();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Users WHERE PersonId= " + PersonId;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    usr.PersonId = Convert.ToInt32(rdr["PersonId"]);
                    usr.name = rdr["name"].ToString();
                    usr.email = rdr["email"].ToString();
                    usr.contact = rdr["contact"].ToString();
                    usr.password = rdr["pasword"].ToString();
                }
                con.Close();
            }
            return usr;
        }

        public void DeleteUser(int? PersonId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_User_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonId", PersonId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
    }
}
