using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLyDoiTacThanhToanController : Controller
    {
        // GET: Admin/QuanLyDoiTacThanhToan
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        /*
         WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyLoaiSanPham
        public ActionResult DanhSachDoiTacThanhToan(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listDoiTacThanhToan = db.DoiTacThanhToans.ToList();
            if (search != null)
            {
                listDoiTacThanhToan = db.DoiTacThanhToans.Where(x => x.TenDTTT.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listDoiTacThanhToan.OrderBy(n => n.MaDTTT).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemDoiTacThanhToan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDoiTacThanhToan(DoiTacThanhToan doiTacThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.DoiTacThanhToans.Add(doiTacThanhToan);
                db.SaveChanges();
                return RedirectToAction("DanhSachDoiTacThanhToan");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaDoiTacThanhToan(int? MaDTTT)
        {
            if (MaDTTT == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.DoiTacThanhToans.SingleOrDefault(x => x.MaDTTT == MaDTTT);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaDoiTacThanhToan(DoiTacThanhToan doiTacThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doiTacThanhToan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachDoiTacThanhToan");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaDoiTacThanhToan(int? MaDTTT)
        {
            if (MaDTTT == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.DoiTacThanhToans.SingleOrDefault(x => x.MaDTTT == MaDTTT);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.DoiTacThanhToans.Remove(model);
            db.SaveChanges();
            return Content("<script>window.location.reload();</script>");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
        public ActionResult DanhSachDoiTacThanhToan(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listDoiTacThanhToan = db.DoiTacThanhToans.ToList();
            if (search != null)
            {
                listDoiTacThanhToan = db.DoiTacThanhToans.Where(x => x.TenDTTT.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listDoiTacThanhToan.OrderBy(n => n.MaDTTT).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ThemDoiTacThanhToan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDoiTacThanhToan(DoiTacThanhToan model) { 
        
            if (ModelState.IsValid)
            {
                db.DoiTacThanhToans.Add(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachDoiTacThanhToan");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaDoiTacThanhToan(int? MaDTTT)
        {
            if (MaDTTT == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.DoiTacThanhToans.SingleOrDefault(x => x.MaDTTT == MaDTTT);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SuaDoiTacThanhToan(DoiTacThanhToan model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachDoiTacThanhToan");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
    }
}