using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectLab06.Areas.Main.Data
{
    public class AccountModel
    {
        private MobileDBDataContext context = null;

        public AccountModel()
        {
            context = new MobileDBDataContext();
        }
        public bool Login(string Email, string Password)
        {
            bool? res = false;
            context.sp_Account_Login_User_Check(Email, Password, ref res);
            return (res ?? false);
        }

        public bool SignUp(string firstname, string lastname, string signup_email)
        {
            bool? res = false;
            context.sp_check_Account(firstname + lastname, signup_email, ref res);
            return (res ?? false);
        }
    }
}