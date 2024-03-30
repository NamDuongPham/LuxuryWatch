using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/QuanLySanPham
        public ActionResult DanhSachSanPham(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listSanPham = db.SanPhams.ToList();
            if (search != null)
            {
                listSanPham = db.SanPhams.Where(x => x.TenSP.Contains(search)).ToList();
                ViewBag.search = search;
            }
            ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
            return View(listSanPham.OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemSanPham()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSanPham(SanPham sanpham, String listAnh)
        {
            if (ModelState.IsValid)
            {
                sanpham.NgayCapNhat = DateTime.Now;
                db.SanPhams.Add(sanpham);
                db.SaveChanges();
                if (listAnh != "")
                {
                    string[] arrListStr = listAnh.Split(',');
                    foreach (String item in arrListStr)
                    {
                        AnhSanPham anhsanpham = new AnhSanPham();
                        anhsanpham.MaSP = sanpham.MaSP;
                        anhsanpham.TenAnhSP = item.Substring(23);
                        db.AnhSanPhams.Add(anhsanpham);
                        db.SaveChanges();
                    }
                }


            }
            return RedirectToAction("DanhSachSanPham");
        }
        public ActionResult SuaSanPham(int? MaSP)
        {
            if (MaSP == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.AnhSanPham = db.AnhSanPhams.Where(x => x.MaSP == MaSP).ToList();
            ViewBag.Ngay = model.NgayCapNhat.Value.ToString("dd/MM/yyyy");
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(SanPham sanpham, String listAnh)
        {
            if (ModelState.IsValid)
            {
                sanpham.NgayCapNhat = DateTime.Now;       
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                sanpham.TenSP = sanpham.TenSP.Replace("<p>", "").Replace("</p>", "");
                db.SaveChanges();
                List<AnhSanPham> anhsanpham = db.AnhSanPhams.Where(x => x.MaSP == sanpham.MaSP).ToList();
                foreach (AnhSanPham item in anhsanpham)
                {
                    db.AnhSanPhams.Remove(item);
                    db.SaveChanges();
                }
                if (listAnh != "")
                {
                    string[] arrListStr = listAnh.Split(',');
                    foreach (String item in arrListStr)
                    {
                        AnhSanPham anhsanphamnew = new AnhSanPham();
                        anhsanphamnew.MaSP = sanpham.MaSP;
                        anhsanphamnew.TenAnhSP = item.Substring(23);
                        db.AnhSanPhams.Add(anhsanphamnew);
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("DanhSachSanPham");

            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaSanPham(int? MaSP)
        {
            if (MaSP == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
             model.TenSP = model.TenSP.Replace("<p>", "").Replace("</p>", "");
            if (model == null)
            {
                return HttpNotFound();
            }

            //db.SanPhams.Remove(model);
            //db.SaveChanges();
            //return Content("<script>window.location.reload();</script>");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaSanPham(SanPham sanPham)
        {
            var model = db.SanPhams.Find(sanPham.MaSP);
            if (model == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.SanPhams.Remove(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachSanPham");
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
            base.Dispose(disposing);
        }
    }
}