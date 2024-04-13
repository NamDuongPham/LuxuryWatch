using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyQuyenController : AdminBaseController
    {
        // GET: Admin/QuanLyQuyen
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        public ActionResult DanhSachQuyen(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listQuyen = db.Quyens.ToList();
            if (search != null)
            {
                listQuyen = db.Quyens.Where(x => x.TenQuyen.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listQuyen.OrderBy(n => n.MaQuyen).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemQuyen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemQuyen(Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Quyens.Add(quyen);
                db.SaveChanges();
                return RedirectToAction("DanhSachQuyen");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaQuyen(String MaQuyen)
        {
            if (MaQuyen == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.Quyens.SingleOrDefault(x => x.MaQuyen.Contains(MaQuyen));
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaQuyen(Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyen).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachQuyen");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaQuyen(String MaQuyen)
        {
            if (MaQuyen == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.Quyens.SingleOrDefault(x => x.MaQuyen.Contains(MaQuyen));
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Quyens.Remove(model);
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