﻿/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreDATA
{
    public class DALBookCatalog
    {
        SqlConnection conn;
        DataSet dsBooks;
        public DALBookCatalog()
        {
            conn = new SqlConnection(Properties.Settings.Default.maconnection);
        }
        public DataSet GetBookInfo()
        {
            try
            {
                String strSQL = "Select CategoryID, Name, Description from Category";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Category");            //Get category info
                String strSQL2 = "Select ISBN, CategoryID, Title," +
                    "Author, Price, Year, Edition, Publisher from BookData";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                daBook.Fill(dsBooks, "Books");                  //Get Books info
                DataRelation drCat_Book = new DataRelation("drCat_Book",
                dsBooks.Tables["Category"].Columns["CategoryID"],
                dsBooks.Tables["Books"].Columns["CategoryID"], false);
                dsBooks.Relations.Add(drCat_Book);       //Set up the table relation
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return dsBooks;
        }
    }
}
