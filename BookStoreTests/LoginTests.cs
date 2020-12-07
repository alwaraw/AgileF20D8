using System;
using BookStoreLIB;
using BookStoreDATA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using BookStoreGUI;

namespace BookStoreTests
{
    [TestClass]
    public class LoginTests
    {
        UserData userData = new UserData();
        DALUserInfo dbUser = new DALUserInfo();
        string inputUser;
        string inputPassword;
        [TestMethod]
        public void TestMethod1()
        {
            inputUser = "pham114";
            inputPassword = "sp1234";

            // Expected return values
            int expectedUserID = 2;
            Boolean expectedReturn = true;

            //obtain actual inputs
            Boolean actualReturn = userData.LogIn(inputUser, inputPassword);
            int actualUserId = userData.UserID;

            // Verify results
            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserID, actualUserId);

        }

        // Test to check length of password and if password starts with letter
        [TestMethod]
        public void TestMethod2()
        {
            // Short password with length of 5
            string shortPassword = "sp123";
            Boolean shortPasswordResult = userData.CheckPassword(shortPassword);

            // Password that doesn't start with a letter
            string noLetterPassword = "1sp1234";
            Boolean noLetterPasswordResult = userData.CheckPassword(noLetterPassword);


            // Valid password with letter start and length of greater/equal to 6
            string validPassword = "sp1234";
            Boolean validPasswordResult = userData.CheckPassword(validPassword);

            // Asserts for all three cases
            Assert.AreEqual(false, shortPasswordResult);
            Assert.AreEqual(false, noLetterPasswordResult);
            Assert.AreEqual(true, validPasswordResult);
        }

        // Test to check if password with only letters fails
        [TestMethod]
        public void TestMethod3()
        {
            string onlyLettersPassword = "stpham";
            Boolean onlyLettersPasswordResult = userData.CheckPassword(onlyLettersPassword);

            // Valid password with letter start and length of greater/equal to 6
            string validPassword = "tevepam123";
            Boolean validPasswordResult = userData.CheckPassword(validPassword);

            Assert.AreEqual(false, onlyLettersPasswordResult);
            Assert.AreEqual(true, validPasswordResult);
        }

        // Test to check if password with only numbers fails
        [TestMethod]
        public void TestMethod4()
        {
            string onlyNumbersPassword = "1233444";
            Boolean onlyNumbersPasswordResult = userData.CheckPassword(onlyNumbersPassword);

            // Valid password with letter start and length of greater/equal to 6
            string validPassword = "stevepam123";
            Boolean validPasswordResult = userData.CheckPassword(validPassword);

            Assert.AreEqual(false, onlyNumbersPasswordResult);
            Assert.AreEqual(true, validPasswordResult);
        }


        // Test to check if password with a character other than a letter and number fails
        [TestMethod]
        public void TestMethod5()
        {
            string nonValidLetterPassword = "hello123@";
            Boolean nonValidLetterPasswordResult = userData.CheckPassword(nonValidLetterPassword);

            string validPassword = "pamsp123";
            Boolean validPasswordResult = userData.CheckPassword(validPassword);

            Assert.AreEqual(false, nonValidLetterPasswordResult);
            Assert.AreEqual(true, validPasswordResult);
        }
    }
}
