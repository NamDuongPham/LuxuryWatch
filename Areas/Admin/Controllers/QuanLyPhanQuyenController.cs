using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyPhanQuyenController : AdminBaseController
    {
        // GET: Admin/QuanLyPhanQuyen
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: Admin/QuanLyPhanQuyen
        public ActionResult DanhSachPhanQuyen(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listThanhVien = db.LoaiThanhViens.ToList();
            if (search != null)
            {
                listThanhVien = db.LoaiThanhViens.Where(x => x.TenLoaiTV.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listThanhVien.OrderBy(n => n.MaLoaiTV).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PhanQuyen(int? MaLoaiTV)
        {
            if (MaLoaiTV == null)
            {
                Response.StatusCode = 404;
            }
            var result = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == MaLoaiTV);
            if (result == null)
            {
                return HttpNotFound();
            }
            //Lấy danh sách quyền
            ViewBag.listQuyen = db.Quyens;
            //Lấy danh sách quyền của loại thành viên
            ViewBag.listLoaiTVQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == MaLoaiTV);
            return View(result);
        }
        [HttpPost]
        public ActionResult PhanQuyen(int? MaLoaiTV, IEnumerable<LoaiThanhVien_Quyen> listPhanQuyen)
        {
            if (MaLoaiTV == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var daPhanQuyen = db.LoaiThanhVien_Quyen.Where(x => x.MaLoaiTV == MaLoaiTV);
            if (daPhanQuyen.Count() > 0)
            {
                db.LoaiThanhVien_Quyen.RemoveRange(daPhanQuyen);
                db.SaveChanges();
            }
            //Kiểm tra list danh sách quyền được check
            if (listPhanQuyen != null)
            {
                foreach (var item in listPhanQuyen)
                {
                    item.MaLoaiTV = int.Parse(MaLoaiTV.ToString());
                    //Nếu được check thì insert dữ liệu vào bảng phân quyền
                    db.LoaiThanhVien_Quyen.Add(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("DanhSachPhanQuyen");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}