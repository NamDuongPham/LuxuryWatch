using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : AdminBaseController
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/AdminHome
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
            return View();
        }

        public ActionResult UserLoginPartial()
        {
            return PartialView();
        }
    }
}