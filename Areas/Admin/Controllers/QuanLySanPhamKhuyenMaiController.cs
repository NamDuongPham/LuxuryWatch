using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class QuanLySanPhamKhuyenMaiController : Controller
    {
        // GET: Admin/QuanLySanPhamKhuyenMai
        LuxuryWatch_DB db = new LuxuryWatch_DB();

        public ActionResult DanhSachKhuyenMai(int? page, string search)
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
        // danh sach san pham khuyen mai
        public ActionResult DanhSachSanPhamKhuyenMai(int? MaCTKM)
        {
            if(MaCTKM == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // truy suat csdl lay ra san pham co maCTKM
            List<SanPhamKhuyenMai> listSanPhamKhuyenMai = db.SanPhamKhuyenMais.Where(x => x.MaSPKM ==  MaCTKM).ToList();
            ViewBag.MaCTKM = MaCTKM;
            return View(listSanPhamKhuyenMai);
        }
    }
}