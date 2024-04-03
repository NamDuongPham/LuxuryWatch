using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace LuxyryWatch.Controllers
{
    public class TimKiemController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();  
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KetQuaTimKiem(string txtTuKhoa, int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var result = db.SanPhams.Where(n => n.TenSP.Contains(txtTuKhoa));
            ViewBag.txtTuKhoa = txtTuKhoa;
            ViewBag.listSanPham = db.SanPhams.ToList();
            ChuongTrinhKhuyenMai CTKM = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.NGgayKetThuc > DateTime.Now && x.ApDung == true);
            if (CTKM != null)
            {
                List<SanPhamKhuyenMai> LSPKM = db.SanPhamKhuyenMais.Where(x => x.MACTKM == CTKM.MaCTKM).ToList();
                if (LSPKM.Count() > 0)
                {
                    ViewBag.LSMPK = LSPKM;
                }
                else
                {
                    ViewBag.LSMPK = null;
                }

            }
            ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
            return View(result.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
    }
}
