using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, NhanVien")]
    public class QuanLyThongTinController : AdminBaseController
    {
        // GET: Admin/QuanLyThonTin
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/QuanLyThongTin
        public ActionResult ThongTinChung()
        {
            List<ThongTin> list = db.ThongTins.ToList();
            if (list.Count() > 0)
            {
                return View(list.First());
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ThongTinChung(ThongTin model)
        {
            if (ModelState.IsValid)
            {
                List<ThongTin> list = db.ThongTins.ToList();
                if (list.Count() > 0)
                {
                    ThongTin tt = db.ThongTins.SingleOrDefault(x => x.MaTT == model.MaTT);
                    tt.SDT = model.SDT;
                    tt.DiaChi = model.DiaChi;
                    tt.Email = model.Email;
                    tt.Map = model.Map;
                    db.SaveChanges();
                }
                else
                {
                    db.ThongTins.Add(model);
                    db.SaveChanges();
                }
            }
            List<ThongTin> listN = db.ThongTins.ToList();
            return View(listN.First());
        }

        public ActionResult GioiThieu()
        {
            List<ThongTin> list = db.ThongTins.ToList();
            if (list.Count() > 0)
            {
                return View(list.First());
            }
            else
            {
                return View();
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult GioiThieu(ThongTin model)
        {
            if (ModelState.IsValid)
            {
                List<ThongTin> list = db.ThongTins.ToList();
                if (list.Count() > 0)
                {
                    ThongTin tt = db.ThongTins.SingleOrDefault(x => x.MaTT == model.MaTT);
                    tt.GioiThieu = model.GioiThieu;
                    db.SaveChanges();
                }
                else
                {
                    db.ThongTins.Add(model);
                    db.SaveChanges();
                }
            }
            List<ThongTin> listN = db.ThongTins.ToList();
            return View(listN.First());
        }
    }
}