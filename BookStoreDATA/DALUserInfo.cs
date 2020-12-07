/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace BookStoreDATA
{
    public class DALUserInfo
    {
        public int LogIn(string userName, string password)
        {
            var conn = new SqlConnection(Properties.Settings.Default.maconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select UserID from UserData where "
                    + " UserName = @UserName and Password = @Password ";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    return -1;
                }
                int userId = (int)cmd.ExecuteScalar();
                if (userId > 0) return userId;
                else return -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int register(string username, string password, string fullName, string email, string phone)
        {
            var conn = new SqlConnection(Properties.Settings.Default.maconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                /*cmd.CommandText = "Select UserID from UserData where "
                    + " UserName = @UserName and Password = @Password ";*/
                cmd.CommandText = "INSERT INTO UserData(UserName, Password, Type, Manager, FullName, Email, PhoneNumber) " + "VALUES (@UserName, @Password, NULL, NULL, @FullName, @Email, @PhoneNumber)";
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PhoneNumber", phone);
                conn.Open();

                cmd.ExecuteNonQuery();
                return 1;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
