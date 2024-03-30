using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLyChuongTrinhKhuyenMaiController : Controller
    {
        // GET: Admin/QuanLyChuongTrinhKhuyenMai

        LuxuryWatch_DB db = new LuxuryWatch_DB();
        public ActionResult DanhSachChuongTrinhKhuyenMai(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listChuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.ToList();
            if (search != null)
            {
                listChuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.Where(x => x.TenCTKM.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listChuongTrinhKhuyenMai.OrderBy(n => n.MaCTKM).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ThemChuongTrinhKhuyenMai()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai chuongTrinhKhuyenMai) {

            if (ModelState.IsValid) { 
                // lay ra chuong trinh khuyen mai dang ap dung
                if(chuongTrinhKhuyenMai.ApDung == true)
                {
                    List<ChuongTrinhKhuyenMai> listCTKM = db.ChuongTrinhKhuyenMais.Where(x => x.ApDung == true).ToList();
                    if (listCTKM.Count > 0)
                    {
                        foreach (var item in listCTKM)
                        {
                            item.ApDung = false;
                            db.SaveChanges();
                        }
                    }
                }
                db.ChuongTrinhKhuyenMais.Add(chuongTrinhKhuyenMai);
                db.SaveChanges();
                return RedirectToAction("DanhSachChuongTrinhKhuyenMai");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }

        public ActionResult SuaChuongTrinhKhuyenMai(int? MaCKTM)
        {
            if (MaCKTM == null)
            {
                Response.StatusCode = 404;
            }

            var model = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCKTM);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SuaChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                if (chuongTrinhKhuyenMai.ApDung == true)
                {
                    List<ChuongTrinhKhuyenMai> list = db.ChuongTrinhKhuyenMais.Where(x => x.ApDung == true && x.MaCTKM != chuongTrinhKhuyenMai.MaCTKM).ToList();
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.ApDung = false;
                            db.SaveChanges();
                        }
                    }
                }
                db.Entry(chuongTrinhKhuyenMai).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachChuongTrinhKhuyenMai");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }

        //public ActionResult XoaChuongTrinhKhuyenMai(int? MaCTKM)
        //{
        //    if (MaCTKM == null)
        //    {
        //        Response.StatusCode = 404;
        //    }
        //    var model = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.ChuongTrinhKhuyenMais.Remove(model);
        //    db.SaveChanges();
        //    return Content("<script>window.location.reload();</script>");
        //}

        public ActionResult XoaChuongTrinhKhuyenMai(int? MaCTKM)
        {
            if (MaCTKM == null)
            {
                return HttpNotFound();
            }

            var model = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
            if (model == null)
            {
                return HttpNotFound();
            }

            // Hiển thị trang xác nhận xóa
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            var model = db.ChuongTrinhKhuyenMais.Find(chuongTrinhKhuyenMai.MaCTKM);
            if (model == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.ChuongTrinhKhuyenMais.Remove(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachChuongTrinhKhuyenMai");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đã xảy ra lỗi khi xóa: " + ex.Message;
                return View(model);
            }
        }
    }
}