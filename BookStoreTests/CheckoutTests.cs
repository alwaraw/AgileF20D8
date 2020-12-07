using System;
using BookStoreDATA;
using BookStoreLIB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStoreTests
{
    /** This class tests the proceed to checkout functionality
     *  of the application.
     **/
    [TestClass]
    public class CheckoutTests
    {
        UserData userData = new UserData();
        DALUserInfo dbUser = new DALUserInfo();
        BookOrder userOrder = new BookOrder();
        string inputUser;
        string inputPassword;
        Boolean actualLoginStatus;
        
        // Testing for user that is not logged in
        // INVALID PARTITION
        [TestMethod]
        public void InvalidUnregisteredUserTest()
        {
            // test inputs
            inputUser = "dube113";
            inputPassword = "";

            //expected results
            Boolean userLoggedIn = false;

            //obtain actual inputs
            userData.LogIn(inputUser, inputPassword);
            actualLoginStatus = userData.LoggedIn;

            //verify results
            Assert.AreEqual(userLoggedIn, actualLoginStatus);
        }

        // Testing a user that is logged in but has an empty cart
        // INVALID PARTITION
        [TestMethod]
        public void EmptyCartTest()
        {
            // test inputs
            inputUser = "dube113";
            inputPassword = "ld1234";

            // expected results
            Boolean userLoggedIn = true;
            Boolean isBookOrderEmpty = true;

            // obtain actual inputs
            userData.LogIn(inputUser, inputPassword);
            actualLoginStatus = userData.LoggedIn;
            Boolean actualOrderStatus = userOrder.isEmpty();

            //verify results
            Assert.AreEqual(userLoggedIn, actualLoginStatus);
            Assert.AreEqual(isBookOrderEmpty, actualOrderStatus);

        }
        // Testing a user that is logged in and the cart is not empty
        // VALID PARTITION
        [TestMethod]
        public void ProceedToCheckoutTest()
        {
            // test inputs
            inputUser = "dube113";
            inputPassword = "ld1234";
            string isbn = "1617290890";
            string title = "book title";
            double unitPrice = 57.34;
            int quantity = 2;

            // expected results
            Boolean userLoggedIn = true;
            Boolean isBookOrderEmpty = false;

            // obtain actual inputs
            userData.LogIn(inputUser, inputPassword);
            userOrder.AddItem(new OrderItem(isbn, title, unitPrice, quantity));

            actualLoginStatus = userData.LoggedIn;
            Boolean actualOrderStatus = userOrder.isEmpty();

            //verify results
            Assert.AreEqual(userLoggedIn, actualLoginStatus);
            Assert.AreEqual(isBookOrderEmpty, actualOrderStatus);

        }
    }
}
