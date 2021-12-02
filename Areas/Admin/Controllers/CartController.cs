using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;

namespace ProjectLab06.Areas.Admin.Controllers
{
    public class CartController : Controller
    {
        MobileDBDataContext _context = new MobileDBDataContext();
        // GET: Admin/Deal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListCart()
        {
            var cart = _context.Carts.ToList();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var cart = _context.Carts.ToList().Find(m => m.Id == id);
            if (cart != null)
            {
                _context.Carts.DeleteOnSubmit(cart);
                _context.SubmitChanges();
            }
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var cart = _context.Carts.ToList().Find(m => m.Id == id);
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Cart cart)
        {
            if (ModelState.IsValid)
            {
                Cart ca = (from p in _context.Carts
                           where p.Id == cart.Id
                           select p).SingleOrDefault();

                ca.UserName = cart.UserName;
                ca.UserEmail = cart.UserEmail;
                ca.CreateDate = cart.CreateDate;
                ca.MobileName = cart.MobileName;
                ca.Quantity = cart.Quantity;
                _context.SubmitChanges();
            }
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(Cart cart)
        {
            var listcart = (from p in _context.Carts select p);
            if (cart.UserName != null)
            {
                listcart = listcart.Where(q => q.UserName == cart.UserName);
            }
            if (cart.UserEmail != null)
            {
                listcart = listcart.Where(q => q.UserEmail == cart.UserEmail);
            }

            List<Cart> lscart = listcart.ToList();

            return Json(lscart, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
        {
            var cart = _context.Carts.ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Report");

                sheet.Cells.LoadFromCollection(cart, true);

                package.Save();

            }
            stream.Position = 0;
            var filename = "ExcelReport.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }

    }
}