using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace BookStoreDATA
{
    public class DALPurchase // comment
    {
        public void AddHistory(int userId, int orderId, int quantity, string bookId, string bookName)
        {
            SqlConnection cn = new SqlConnection(
                Properties.Settings.Default.maconnection);

            try
            {
                cn.Open(); 
                SqlCommand cmd = cn.CreateCommand();
                string insert = "INSERT INTO PurchaseHistory (UserId, OrderId, Quantity, ISBN, BookName) VALUES (@userId, @orderId, @quantity, @bookId, @bookName);";
                cmd.CommandText = insert;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@bookName", bookName);
                cmd.ExecuteNonQuery(); 
                cn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close(); 
            }
        }

        public DataView ViewHistory(int userId)
        {
            SqlConnection cn = new SqlConnection(
                Properties.Settings.Default.maconnection);
            DataTable dsPurchaseHistory = new DataTable();
            // Get the history
            try
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                SqlDataAdapter ad = new SqlDataAdapter();
                string view = "SELECT OrderId, BookName, Date, Quantity, ISBN, Status FROM PurchaseHistory WHERE UserId=@userId";
                cmd.CommandText = view;
                cmd.Parameters.AddWithValue("@userId", userId);
                ad.SelectCommand = cmd;
                ad.Fill(dsPurchaseHistory);
                cn.Close();
                return dsPurchaseHistory.DefaultView;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return dsPurchaseHistory.DefaultView;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }
    }
}
