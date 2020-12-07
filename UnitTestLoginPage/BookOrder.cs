/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BookStoreDATA;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB
{
    public class BookOrder
    {
        ObservableCollection<OrderItem> orderItemList = new
            ObservableCollection<OrderItem>();
        public ObservableCollection<OrderItem> OrderItemList
        {
            get { return orderItemList; }
        }

        public Boolean isEmpty()
        {
            // returns true if orderList is empty otherwise false
            if (orderItemList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetOrderTotal()
        {
            if (orderItemList.Count == 0)
            {
                return 0.00;
            }
            else
            {
                double total = 0;
                foreach (var item in orderItemList)
                {
                    total += item.SubTotal;
                }
                return total;
            }
        }
        public void AddItem(OrderItem orderItem)
        {
            foreach (var item in orderItemList)
            {
                if (item.BookID == orderItem.BookID)
                {
                    item.Quantity += orderItem.Quantity;
                    return;
                }
            }
            orderItemList.Add(orderItem);
        }
        public void RemoveItem(string productID)
        {
            foreach (var item in orderItemList)
            {
                if (item.BookID == productID)
                {
                    orderItemList.Remove(item);
                    return;
                }
            }
        }
        public int PlaceOrder(int userID)
        {
            string xmlOrder;
            xmlOrder = "<Order UserID='" + userID.ToString() + "'>";
            foreach (var item in orderItemList)
            {
                xmlOrder += item.ToString();
            }
            xmlOrder += "</Order>";
            DALOrder dbOrder = new DALOrder();
            return dbOrder.Proceed2Order(xmlOrder);
        }

        public void addToHistory(int userId, int orderId)
        {
            if (userId <= 0)
            {
                return; 
            }
            DALPurchase purchases = new DALPurchase(); 
            foreach (var item in orderItemList)
            {
                purchases.AddHistory(userId, orderId, item.Quantity, item.BookID, item.BookTitle);
            }
        }

    }
}
