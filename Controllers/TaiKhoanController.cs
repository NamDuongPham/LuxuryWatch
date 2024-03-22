using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        LuxuryWatch_DB db = new LuxuryWatch_DB();

        public ActionResult ThongTinTaiKhoan()
        {
            ThanhVien tv = (ThanhVien)Session["TaiKhoan"];
            return View(tv);
        }
        [HttpPost]
        public ActionResult ThongTinTaiKhoan(ThanhVien tv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ThanhVien tvn = (ThanhVien)Session["TaiKhoan"];
                return View(tvn);
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult DoiMatKhau(string txtMKC, string txtMKM, string txtNLMK)
        {
            string mkc = MaHoa.MD5Hash(txtMKC);
            string mkm = MaHoa.MD5Hash(txtMKM);
            string nlmk = MaHoa.MD5Hash(txtNLMK);
            ThanhVien tv = (ThanhVien)Session["TaiKhoan"];
            if (!tv.MatKhau.Contains(mkc))
            {
                //return Content("Mật khẩu không chính xác!");
                return Content("<script>alert('Mật khẩu không chính xác!'); window.location.href = '/TaiKhoan/ThongTinTaiKhoan';</script>");
            }
            ThanhVien result = db.ThanhViens.Single(x => x.TaiKhoan.Contains(tv.TaiKhoan));
            result.MatKhau = mkm;
            db.SaveChanges();
            //return Content("<script>window.location.reload();</script>");
            return Content("<script>alert('Đổi mật khẩu thành công!'); window.location.href = '/TaiKhoan/ThongTinTaiKhoan';</script>");
        }
    }
}