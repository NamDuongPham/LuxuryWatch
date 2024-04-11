using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}