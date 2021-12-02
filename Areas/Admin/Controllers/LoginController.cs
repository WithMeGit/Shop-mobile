using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLab06.Areas.Admin.Data;

namespace ProjectLab06.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            var check = new AccountModel().Create(model.User, model.Emailaddress);

            if (model.UserName == null && model.Password == null || model.Emailaddress == null && model.Password == null)
            {
                ModelState.AddModelError("", "Please input your Username or Password or Emailaddress");
            }
            else
            if (model.User == null)
            {
                if (result || ModelState.IsValid)
                {
                    Session.Add("emailaddress", model.Emailaddress);
                    return RedirectToAction("Index", "PageMain", new { Area = "Admin" });
                }
                else if (model.Emailaddress != null)
                {
                    ModelState.AddModelError("", "Please input your name");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is incorrect");
                }
            }
            else
            {
                if (check && ModelState.IsValid)
                {
                    ModelState.AddModelError("", "UserName or Password or Emailaddress already exists.");
                }
                else if (model.Password == null)
                {
                    ModelState.AddModelError("", "Please input your Pass");
                }
                else if (model.Emailaddress == null)
                {
                    ModelState.AddModelError("", "Please input your Email");
                }
                else
                {
                    MobileDBDataContext context = new MobileDBDataContext();
                    context.Create_Account_Admin(model.User, model.Password, model.Emailaddress);
                    context.SubmitChanges();
                }
            }
            return View(model);
        }
    }
}