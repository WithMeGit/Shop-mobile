using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLab06.Areas.Main.Data;

namespace ProjectLab06.Areas.Main.Controllers
{
    public class LoginController : Controller
    {
        // GET: Main/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var result = new AccountModel().Login(model.Email, model.Password);
            var check = new AccountModel().SignUp(model.FirstName, model.LastName, model.SingUpEmail);
            if (model.FirstName == null && model.LastName == null || model.Email == null && model.Password == null)
            {
                if (model.Email == null && model.Password == null)
                {
                    ModelState.AddModelError("", "Please input your Emailaddress or Pasword");
                }
            }
            if (model.SingUpEmail == null)
            {
                if (result || ModelState.IsValid)
                {
                    Session.Add("email", model.Email);
                    return RedirectToAction("Index", "PageMain", new { Area = "Main" });
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
            }
            else
            {
                if (check && ModelState.IsValid)
                {
                    ModelState.AddModelError("", "UserName or Password or Emailaddress already exists.");
                }
                else
                {
                    MobileDBDataContext context = new MobileDBDataContext();
                    context.Create_Account_User(model.SingUpEmail, model.Password, model.FirstName, model.LastName);
                    context.SubmitChanges();
                }
            }
            return View(model);
        }
    }
}