using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectLab06.Areas.Admin.Controllers
{
    public class PageMainController : Controller
    {
        MobileDBDataContext _context = new MobileDBDataContext();
        // GET: Admin/PageMain
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndexPageMain(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string destFile = System.IO.Path.Combine(rootPath, "Assets\\Mobile\\images\\" + fileName);
                file.SaveAs(destFile);
            }
            return View();
        }

        public ActionResult ListMobile()
        {
            var mobiles = _context.Mobiles.ToList();
            return Json(mobiles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(mobile.ImagePath);

                mobile.ImagePath = "images/" + fileName;
                _context.Mobiles.InsertOnSubmit(mobile);
                _context.SubmitChanges();
            }
            return Json(mobile, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var mobile = _context.Mobiles.ToList().Find(m => m.Id == id);
            if (mobile != null)
            {
                _context.Mobiles.DeleteOnSubmit(mobile);
                _context.SubmitChanges();
            }
            return Json(mobile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var mobile = _context.Mobiles.ToList().Find(m => m.Id == id);
            string rootPath = Server.MapPath("~/");
            string fileName = System.IO.Path.GetFileName(mobile.ImagePath);
            string destFile = System.IO.Path.Combine(rootPath, "Assets\\Mobile\\images\\" + fileName);
            mobile.ImagePath = destFile;

            return Json(mobile, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(mobile.ImagePath);

                mobile.ImagePath = "images/" + fileName;

                Mobile mobi = (from p in _context.Mobiles
                            where p.Id == mobile.Id
                            select p).SingleOrDefault();

                mobi.MobileName = mobile.MobileName;
                mobi.ImagePath = mobile.ImagePath;
                mobi.Price = mobile.Price;
                mobi.Type = mobile.Type;
                _context.SubmitChanges();
            }
            return Json(mobile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(Mobile mobile)
        {
            var listmobile = (from p in _context.Mobiles select p);

            if (mobile.MobileName != null)
            {
                listmobile = listmobile.Where(q => q.MobileName == mobile.MobileName);
            }
            if (mobile.Type != null)
            {
                listmobile = listmobile.Where(q => q.Type == mobile.Type);
            }
            List<Mobile> mb = listmobile.ToList();

            return Json(mb, JsonRequestBehavior.AllowGet);
        }

    }
}