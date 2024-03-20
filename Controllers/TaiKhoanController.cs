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
        //public ActionResult DoiMatKhau(string txtMKC, string txtMKM, string txtNLMK)
        //{
        //    string mkc = MaHoa.MD5Hash(txtMKC);
        //    string mkm = MaHoa.MD5Hash(txtMKM);
        //    string nlmk = MaHoa.MD5Hash(txtNLMK);
        //    ThanhVien tv = (ThanhVien)Session["TaiKhoan"];
        //    if (!tv.MatKhau.Contains(mkc))
        //    {
        //        return Content("Mật khẩu không chính xác!");
        //    }
        //    ThanhVien result = db.ThanhViens.Single(x => x.TaiKhoan.Contains(tv.TaiKhoan));
        //    result.MatKhau = mkm;
        //    db.SaveChanges();
        //    return Content("<script>window.location.reload();</script>");
        //}
        [HttpPost]
        public ActionResult DoiMatKhau(string txtMKC, string txtMKM, string txtNLMK)
        {
            // Lấy thông tin thành viên từ session
            ThanhVien tv = (ThanhVien)Session["TaiKhoan"];
            if (tv == null)
            {
                return Content("Không thể tìm thấy thông tin thành viên. Vui lòng đăng nhập lại.");
            }

            // Kiểm tra xem mật khẩu cũ có đúng không
            if (tv.MatKhau != txtMKC)
            {
                return Content("Mật khẩu cũ không đúng!");
            }

            // Kiểm tra xem mật khẩu mới và nhập lại mật khẩu có khớp nhau không
            if (txtMKM != txtNLMK)
            {
                return Content("Mật khẩu mới và nhập lại mật khẩu không khớp!");
            }

            // Lấy thông tin thành viên từ cơ sở dữ liệu
            ThanhVien result = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == tv.TaiKhoan);

            if (result != null)
            {
                // Cập nhật mật khẩu mới
                result.MatKhau = txtMKM;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                // Reload trang
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi thay đổi mật khẩu!" });
            }
        }
    }
    
    
}