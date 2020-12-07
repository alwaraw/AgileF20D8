/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookStoreDATA;
using BookStoreLIB;
using System.Data;
using System.Collections.ObjectModel;

namespace BookStoreGUI
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        DataSet dsBookCat;
        UserData UserData;
        BookOrder bookOrder;

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginButton.Content.Equals("Login"))
            {
                LoginDialog dlg = new LoginDialog();
                dlg.Owner = this;
                dlg.ShowDialog();

                if (dlg.nameTextBox.Text == "" || dlg.passwordTextBox.Password == "")
                    statusTextBlock.Text = "Please fill in all slots.";
                else if (UserData.LogIn(dlg.nameTextBox.Text, dlg.passwordTextBox.Password) == true)
                {
                    statusTextBlock.Text = "You are logged in as: " + UserData.LoginName;
                    loginButton.Content = "Logout";
                }
                else
                    statusTextBlock.Text = "You could not be verified. Please try again.";
            }
            else
            {
                loginButton.Content = "Login";
                statusTextBlock.Text = "Please login before proceeding to checkout.";
                UserData.Logout();
                //reset user name and password when logging out
                global.usernm = "";
                global.pass = "";
            }
        }
        private void exitButton_Click(object sender, RoutedEventArgs e) { this.Close(); }
        public MainWindow() { InitializeComponent(); }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            this.DataContext = dsBookCat.Tables["Category"];
            bookOrder = new BookOrder();
            UserData = new UserData();
            this.orderListView.ItemsSource = bookOrder.OrderItemList; 
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OrderItemDialog orderItemDialog = new OrderItemDialog();
            DataRowView selectedRow;
            selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
            orderItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
            orderItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();

            orderItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
            orderItemDialog.Owner = this;
            orderItemDialog.ShowDialog();
            if (orderItemDialog.DialogResult == true)
            {
                string isbn = orderItemDialog.isbnTextBox.Text;
                string title = orderItemDialog.titleTextBox.Text;
                double unitPrice = double.Parse(orderItemDialog.priceTextBox.Text);
                int quantity = int.Parse(orderItemDialog.quantityTextBox.Text);
                bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, quantity));
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderListView.SelectedItem != null)
            {
                var selectedOrderItem = this.orderListView.SelectedItem as OrderItem;
                bookOrder.RemoveItem(selectedOrderItem.BookID);
            }
        }
        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {
            int orderId;
            if (UserData.LoggedIn)
            {
                if (!bookOrder.isEmpty())
                {
                    orderId = bookOrder.PlaceOrder(UserData.UserID);
                    bookOrder.addToHistory(UserData.UserID, orderId);
                    MessageBox.Show("Your order has been placed. Your order id is " +
                    orderId.ToString());
                }
                else
                {
                    MessageBox.Show("Please add an order to cart to proceed to checkout.");
                }
            }
            else
            {
                MessageBox.Show("Please register and/or login to proceed to checkout.");
            }
        }

        private void categoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void purchaseButton_Click(object sender, RoutedEventArgs e)
        {
            purchaseHistory ph = new purchaseHistory();
            ph.userId = UserData.UserID;
            ph.Show();
        }
        private void updateTotal()
        {
            double total = 0.0;
            total = bookOrder.GetOrderTotal();
            totalPrice.Text = total.ToString();
            return;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            registerPage dlg = new registerPage();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void totalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void buttonTotalPrice_Click(object sender, RoutedEventArgs e)
        {
            updateTotal();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            returnPage rp = new returnPage();
            rp.Show();
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfile up = new UserProfile();
            up.Show();
        }
    }
}
