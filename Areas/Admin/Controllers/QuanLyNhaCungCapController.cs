using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLyNhaCungCapController : Controller
    {
        // GET: Admin/QuanLyNhaCungCap
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        public ActionResult DanhSachNhaCungCap(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listNhaCungCap = db.NhaCungCaps.ToList();
            if (search != null)
            {
                listNhaCungCap = db.NhaCungCaps.Where(x => x.TenNCC.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listNhaCungCap.OrderBy(n => n.MaNCC).ToPagedList(pageNumber, pageSize));
        }

        // them nha cung cap
        public ActionResult ThemNhaCungCap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNhaCungCap(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(nhaCungCap);
                db.SaveChanges();
                ViewBag.ThongBao = "Them nha cung cap thanh cong!";
                return RedirectToAction("DanhSachNhaCungCap");
            }

            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }

        // sua nha cung cap 
        public ActionResult SuaNhaCungCap(int? MaNCC)
        {
            if (MaNCC == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.NhaCungCaps.SingleOrDefault(x => x.MaNCC == MaNCC);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaNhaCungCap(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCungCap).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaCungCap");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }

        // xoa nha cung cap 
        //public ActionResult XoaNhaCungCap(int? MaNCC)
        //{
        //    if (MaNCC == null)
        //    {
        //        Response.StatusCode = 404;
        //    }
        //    var model = db.NhaCungCaps.SingleOrDefault(x => x.MaNCC == MaNCC);
        //    var sanPhamTon = db.SanPhams.SingleOrDefault(x => x.MaNCC == MaNCC);
        //    // kiem tra xem ma nha cung cap co null khong
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    // kiem tra xem con san pham nao thuoc ncc can xoa khong
        //    if (sanPhamTon != null)
        //    {
        //        //return Content("<script>alert('Bạn phải xóa sản phẩm thuộc nhà cung cấp trước!'); window.location.href = '/Admin/QuanLyNhaCungCap/DanhSachNhaCungCap';</script>");
        //        return Content("<script>window.location.reload();</script>");
        //    }
        //    db.NhaCungCaps.Remove(model);
        //    db.SaveChanges();

        //    //return View(model);
        //    return Content("<script>window.location.reload();</script>");
        //    //return Content("<script>alert('Xóa thành công nhà cung cấp!'); window.location.href = '/Admin/QuanLyNhaCungCap/DanhSachNhaCungCap';</script>");
        //}
        public ActionResult XoaNhaCungCap(int? MaNCC)
        {
            if (MaNCC == null)
            {
                return HttpNotFound();
            }

            var model = db.NhaCungCaps.SingleOrDefault(x => x.MaNCC == MaNCC);
            if (model == null)
            {
                return HttpNotFound();
            }

            // Hiển thị trang xác nhận xóa
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaNhaCungCap(NhaCungCap nhaCungCap)
        {
            var model = db.NhaCungCaps.Find(nhaCungCap.MaNCC);
            if (model == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.NhaCungCaps.Remove(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaCungCap");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đã xảy ra lỗi khi xóa: " + ex.Message;
                return View(model);
            }
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
        }
    }
}