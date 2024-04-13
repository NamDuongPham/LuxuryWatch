using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, NhanVien")]
    public class QuanLyLoaiSanPhamController : AdminBaseController
    {
        // GET: Admin/QuanLyLoaiSanPham
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/QuanLyLoaiSanPham
        public ActionResult DanhSachLoaiSanPham(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listLoaiSanPham = db.LoaiSanPhams.ToList();
            if (search != null)
            {
                listLoaiSanPham = db.LoaiSanPhams.Where(x => x.TenLoaiSP.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listLoaiSanPham.OrderBy(n => n.MaLoaiSP).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemLoaiSanPham()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSanPhams.Add(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiSanPham");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaLoaiSanPham(int? MaLoaiSP)
        {
            if (MaLoaiSP == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.LoaiSanPhams.SingleOrDefault(x => x.MaLoaiSP == MaLoaiSP);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiSanPham");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaLoaiSanPham(int? MaLoaiSP)
        {
            if (MaLoaiSP == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.LoaiSanPhams.SingleOrDefault(x => x.MaLoaiSP == MaLoaiSP);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.LoaiSanPhams.Remove(model);
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
        }
    }
}