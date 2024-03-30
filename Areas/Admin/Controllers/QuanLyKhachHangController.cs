using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLyKhachHangController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();

        // GET: Admin/QuanLyKhachHang/DanhSachKhachHang
        public ActionResult DanhSachKhachHang(int? page, string search)
        {
            // Thiết lập kích thước trang và số trang
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var listKhachHang = db.KhachHangs.ToList();

            // Tìm kiếm khách hàng nếu có
            if (!string.IsNullOrEmpty(search))
            {
                listKhachHang = db.KhachHangs.Where(x => x.TenKH.Contains(search)).ToList();
                ViewBag.search = search;
            }

            return View(listKhachHang.OrderBy(n => n.Makh).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/QuanLyKhachHang/SuaKhachHang/5
        public ActionResult SuaKhachHang(int? Makh)
        {
            if (Makh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.KhachHangs.SingleOrDefault(x => x.Makh == Makh);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaTV = new SelectList(db.ThanhViens.OrderBy(n => n.MaTV), "MaTV", "Hoten");

            return View(model);
        }

        // POST: Admin/QuanLyKhachHang/SuaKhachHang/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DanhSachKhachHang");
            }

            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View(khachHang);
        }

        // GET: Admin/QuanLyKhachHang/XoaKhachHang/5
        public ActionResult XoaKhachHang(int? Makh)
        {
            if (Makh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.KhachHangs.SingleOrDefault(x => x.Makh == Makh);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Admin/QuanLyKhachHang/XoaKhachHang/5
        [HttpPost, ActionName("XoaKhachHang")]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanXoaKhachHang(int Makh)
        {
            var model = db.KhachHangs.Find(Makh);

            if (model == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.KhachHangs.Remove(model);
                db.SaveChanges();

                return RedirectToAction("DanhSachKhachHang");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đã xảy ra lỗi khi xóa: " + ex.Message;

                return View(model);
            }
        }
    }
}
