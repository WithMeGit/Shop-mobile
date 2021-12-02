using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectLab06.Areas.Admin.Data
{
    public class LoginModel
    {
        [Required]

        public string User { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Emailaddress { get; set; }
    }
}