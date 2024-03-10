using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home_Layout()
        {
            return View();
        }
        public ActionResult HienThiSanPhamTheoLoaiPartial()
        {
            return PartialView();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult MenuPartial()
        {
            //ViewBag.listDMSP = db.LoaiSanPhams.ToList();
            //ViewBag.listMenu = db.SanPhams.ToList();
            return PartialView();
        }
        public ActionResult TaiKhoanPartial()
        {
            return PartialView();
        }
    }
}