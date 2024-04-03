using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String txtTaiKhoan, String txtMatKhau)
        {
            txtMatKhau = MaHoa.MD5Hash(txtMatKhau);
            var result = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan.Contains(txtTaiKhoan) && x.MatKhau.Contains(txtMatKhau));
            if (result == null)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác!";
                return View();
            }
            var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == result.MaLoaiTV).ToList();
            //Duyệt list Quyền
            string Quyen = "";
            if (lstQuyen.Count != 0)
            {
                foreach (var item in lstQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);//Cắt dấu "," cuối cùng
                //PhanQuyen(result.TaiKhoan, Quyen);
            }
            //Session["Admin"] = result;
            return RedirectToAction("Index", "AdminHome");
        }
        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "AdminLogin");
        }
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        //public void PhanQuyen(string TaiKhoan, string Quyen)
        //{
        //    FormsAuthentication.Initialize();
        //    var ticket = new FormsAuthenticationTicket(1,
        //        TaiKhoan,
        //        DateTime.Now,
        //        DateTime.Now.AddHours(3),
        //        false,
        //        Quyen,
        //        FormsAuthentication.FormsCookiePath
        //        );
        //    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
        //    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
        //    Response.Cookies.Add(cookie);
        //}
    }
}