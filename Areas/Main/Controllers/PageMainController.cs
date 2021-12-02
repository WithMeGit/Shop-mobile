using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectLab06.Areas.Main.Controllers
{
    public class PageMainController : Controller
    {
        MobileDBDataContext _context = new MobileDBDataContext();
        // GET: Main/PageMain
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListMobile()
        {
            var mobiles = _context.Mobiles.ToList();
            return Json(mobiles, JsonRequestBehavior.AllowGet);
        }
    }
}