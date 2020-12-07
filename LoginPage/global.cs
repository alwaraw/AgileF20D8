using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreGUI
{
    public class global
    {
       //Global variable for user name
        private static string _usernm = "";
        public static string usernm
        {
            get { return _usernm; }
            set { _usernm = value; }
        }

        //global variable for password
        private static string _pass = "";
        public static string pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

    }
}
