using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectLab06.Areas.Admin.Data
{
    public class AccountModel
    {
        private MobileDBDataContext context = null;

        public AccountModel()
        {
            context = new MobileDBDataContext();
        }
        public bool Login(string UserName, string Password)
        {
            bool? res = false;
            context.sp_Account_Login_Admin_Check(UserName, Password, ref res);
            return (res ?? false);
        }
        public bool Create(string User, string Emailaddress)
        {
            bool? res = false;
            context.sp_check_Account(User, Emailaddress, ref res);
            return (res ?? false);
        }
    }
}