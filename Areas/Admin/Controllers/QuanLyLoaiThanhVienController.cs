using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLyLoaiThanhVienController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/Quanlyloaithanhvien
        public ActionResult DanhSachLoaiThanhVien(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listLoaiThanhVien = db.LoaiThanhViens.ToList();
            if (search != null)
            {
                listLoaiThanhVien = db.LoaiThanhViens.Where(x => x.TenLoaiTV.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listLoaiThanhVien.OrderBy(n => n.MaLoaiTV).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemLoaiThanhVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiThanhVien(LoaiThanhVien loaiThanhVien)
        {
            if (ModelState.IsValid)
            {
                db.LoaiThanhViens.Add(loaiThanhVien);
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiThanhVien");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaLoaiThanhVien(int? MaLoaiTV)
        {
            if (MaLoaiTV == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == MaLoaiTV);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaLoaiThanhVien(LoaiThanhVien loaiThanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiThanhVien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiThanhVien");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaLoaiThanhVien(int? MaLoaiTV)
        {
            if (MaLoaiTV == null)
            {
                return HttpNotFound();
            }

            var model = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == MaLoaiTV);
            if (model == null)
            {
                return HttpNotFound();
            }

            // Hiển thị trang xác nhận xóa
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaLoaiThanhVien( LoaiThanhVien loaiThanhVien)
        {
            var model = db.LoaiThanhViens.Find(loaiThanhVien.MaLoaiTV);
            if (model == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.LoaiThanhViens.Remove(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiThanhVien");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đã xảy ra lỗi khi xóa: " + ex.Message;
                return View(model);
            }
        }
    }
}