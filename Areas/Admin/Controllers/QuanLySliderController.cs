using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLySliderController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/QuanLySlider
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachSlider(int? page)
        {

            // Tao bien so phan tu tren trang
            int pageSize = 5;
            // Tao bien so trang
            int pageNumber = (page ?? 1);
            var listSliders = db.Sliders.ToList();
            return View(listSliders.OrderBy(n => n.MaSlider).ToPagedList(pageNumber, pageSize));

        }

        public ActionResult CapNhatSlider()
        {
            List<Slider> LS = db.Sliders.ToList();
            if (LS.Count() > 0)
            {
                return View(LS);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult CapNhatSlider(string listAnh)
        {
            List<Slider> LS = db.Sliders.ToList();
            if (LS != null)
            {
                foreach (Slider item in LS)
                {
                    db.Sliders.Remove(item);
                    db.SaveChanges();
                }
            }

            if (listAnh != "")
            {
                string[] arrListStr = listAnh.Split(',');
                foreach (string item in arrListStr)
                {
                    Slider slider = new Slider();
                    slider.Anh = item.Substring(23);
                    db.Sliders.Add(slider);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("DanhSachSlider");
        }
    }
}