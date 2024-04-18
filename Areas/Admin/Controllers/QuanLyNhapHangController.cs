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
    public class QuanLyNhapHangController : AdminBaseController
    {
        // GET: Admin/QuanLyNhapHang
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/QuanLyNhapHang
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.listSanPham = db.SanPhams;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(PhieuNhap phieuNhap, IEnumerable<ChiTietPhieuNhap> chiTietPhieuNhaps)
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.listSanPham = db.SanPhams;
            //Tạo phiếu
            db.PhieuNhaps.Add(phieuNhap);
            db.SaveChanges();
            SanPham sanPham;
            foreach (var item in chiTietPhieuNhaps)
            {
                var sanphamnhap = db.ChiTietPhieuNhaps.SingleOrDefault(x => x.MaSP == item.MaSP && x.MaPN == phieuNhap.MaPN);
                if (sanphamnhap == null)
                {
                    sanPham = db.SanPhams.Single(n => n.MaSP == item.MaSP);
                    sanPham.SoLuongTon += item.SoLuongNhap;
                    item.MaPN = phieuNhap.MaPN;
                    db.ChiTietPhieuNhaps.Add(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("DanhSachPhieuNhap");
        }
        public ActionResult DanhSachPhieuNhap(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listPhieuNhap = db.PhieuNhaps.ToList();
            ViewBag.ChiTietPhieuNhaps = db.ChiTietPhieuNhaps.ToList();
            return View(listPhieuNhap.OrderBy(n => n.MaPN).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult XoaPhieuNhap(int? MaPN)
        {
            if (MaPN == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.PhieuNhaps.SingleOrDefault(x => x.MaPN == MaPN);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.PhieuNhaps.Remove(model);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuNhap");
        }
        public ActionResult ChiTietPhieuNhap(int? MaPN)
        {
            if (MaPN == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var list = db.ChiTietPhieuNhaps.Where(x => x.MaPN == MaPN).ToList();
            if (list.Count <= 0)
            {
                return HttpNotFound();
            }
            ViewBag.MaPN = MaPN;
            return View(list);
        }
        // public ActionResult XoaNhaCungCap(int? MaNCC)
        //{
        //    if (MaNCC == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var model = db.NhaCungCaps.SingleOrDefault(x => x.MaNCC == MaNCC);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // Hiển thị trang xác nhận xóa
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult XoaNhaCungCap(NhaCungCap nhaCungCap)
        //{
        //    var model = db.NhaCungCaps.Find(nhaCungCap.MaNCC);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    try
        //    {
        //        db.NhaCungCaps.Remove(model);
        //        db.SaveChanges();
        //        return RedirectToAction("DanhSachNhaCungCap");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ThongBao = "Đã xảy ra lỗi khi xóa: " + ex.Message;
        //        return View(model);
        //    }
        //}
    }
}