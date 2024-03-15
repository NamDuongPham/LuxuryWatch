using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Controllers
{
    public class HomeController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
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
        public ActionResult _Layout()
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

        // action dang nhap
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            if (TaiKhoan == null || MatKhau == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //MatKhau = MaHoa.MD5Hash(MatKhau);
            var result = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == TaiKhoan && x.MatKhau == MatKhau);
            if (result == null)
            {
                return Content("Tài khoản hoặc mật khẩu không chính xác!");
            }
            Session["TaiKhoan"] = result;
                return Content("<script>window.location.reload();</script>");
            //return RedirectToAction("Index");
            //return  View();
        }
        // action dang xuat
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("Index");
        }

        // action hien thi san pham
        
        public ActionResult MenuPartial()
        {
            ViewBag.listDMSP = db.LoaiSanPhams.ToList();
            ViewBag.listMenu = db.SanPhams.ToList();
            return PartialView();
        }
        public ActionResult TaiKhoanPartial()
        {
            return PartialView();
        }
        public ActionResult BanerPartial()
        {
            List<Slider> LS = db.Sliders.ToList();
            return PartialView(LS);
        }
    }
}