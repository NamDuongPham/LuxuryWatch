using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuxyryWatch.Migrations;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyThongKeController : AdminBaseController
    {
        // GET: Admin/QuanLyThongKe
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        public ActionResult Index()
        {
            ViewBag.ThanhVien = db.ThanhViens.Count();
            List<DonDatHang> list = db.DonDatHangs.Where(x => x.HoanThanh == true && x.DaHuy == false && x.DaThanhToan == true).ToList();
            decimal doanhso = 0;
            foreach (var item in list)
            {
                doanhso += (decimal)item.TongThanhToan;
            }
            ViewBag.DoanhSo = doanhso.ToString("#,##");
            ViewBag.DonDatHang = db.DonDatHangs.Count();
            ViewBag.Online = HttpContext.Application["Online"];
            ViewBag.ChuaThanhToan = db.DonDatHangs.Count(x => x.DaThanhToan == false && x.DaHuy == false);
            return View();
        }

        //[HttpPost]
        //public ActionResult ThongKeTheoNgay(DateTime ngayThongKe)
        //{
        //    var donHangTheoNgay = db.DonDatHangs
        //        .Where(x => x.NgayDat == ngayThongKe && x.HoanThanh == true && x.DaHuy == false && x.DaThanhToan == true)
        //        .Select(x => new { NgayDat = x.NgayDat, TongThanhToan = x.TongThanhToan })
        //        .ToList();

        //    return Json(donHangTheoNgay, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult ThongKeTheoNgay(DateTime ngayThongKe)
        {
            var donHangTheoNgay = db.DonDatHangs
                .Where(x => x.NgayDat == ngayThongKe && x.HoanThanh == true && x.DaHuy == false && x.DaThanhToan == true)
                .Select(x => new { NgayDat = x.NgayDat, TongThanhToan = x.TongThanhToan, SoLuongDonHang = 1, SoLuongSanPham = x.ChiTietDonDatHangs.Sum(ct => ct.SoLuong) })
                .ToList();

            // Tính tổng doanh thu, số lượng đơn hàng và số lượng sản phẩm
            //decimal tongDoanhThu = donHangTheoNgay.Sum(dh => dh.TongThanhToan);
            // Tính tổng doanh thu
            decimal tongDoanhThu = donHangTheoNgay.Sum(dh => dh.TongThanhToan.GetValueOrDefault());
            int tongSoDonHang = donHangTheoNgay.Count;
            int tongSoLuongSanPham = donHangTheoNgay.Sum(dh => dh.SoLuongSanPham.GetValueOrDefault());

            // Tạo dữ liệu cho biểu đồ cột
            var chartData = new
            {
                TongDoanhThu = tongDoanhThu,
                TongSoDonHang = tongSoDonHang,
                TongSoLuongSanPham = tongSoLuongSanPham
            };

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

    }
}