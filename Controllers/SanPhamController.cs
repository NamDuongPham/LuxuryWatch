using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace LuxyryWatch.Controllers
{
    public class SanPhamController : Controller
    {
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        // GET: SanPham
        public ActionResult HienThiChiTietSanPham(int? MaSP)
        {
            var result = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (result != null)
            {
                ViewBag.AnhSP = db.AnhSanPhams.Where(x => x.MaSP == MaSP).ToList();
                ChuongTrinhKhuyenMai CTKM = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.NGgayKetThuc > DateTime.Now && x.ApDung == true);
                if (CTKM != null)
                {
                    SanPhamKhuyenMai SPKM = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MACTKM == CTKM.MaCTKM && x.MaSP == MaSP);
                    if (SPKM != null)
                    {
                        decimal giatrigiam = (decimal)SPKM.GiaTriGiam;
                        decimal giakhuyenmai = (decimal)result.DonGia * ((100 - giatrigiam) / 100);
                        ViewBag.GiaKhuyenMai = giakhuyenmai;
                    }
                }
            }
            return View(result);
        }
        public ActionResult HienThiDanhSachSanPham(int? MaLoaiSP, int? MaNSX, int? page, string txtTimKiem1, string SapXep, int? Gia)
        {
            List<SanPham> result;
            ViewBag.ListSP = db.SanPhams.ToList();
            if (MaLoaiSP == null && MaNSX == null)
            {
                result = db.SanPhams.ToList();
            }
            else
            {
                if (MaLoaiSP != null && MaNSX == null)
                {
                    result = db.SanPhams.Where(x => x.MaLoaiSP == MaLoaiSP).ToList();
                }
                else if (MaLoaiSP == null && MaNSX != null)
                {
                    result = db.SanPhams.Where(x => x.MaNSX == MaNSX).ToList();
                }
                else
                {
                    result = db.SanPhams.Where(x => x.MaLoaiSP == MaLoaiSP && x.MaNSX == MaNSX).ToList();
                }
            }
            ViewBag.menu = db.SanPhams.ToList();
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int pageSize = 9;
            //Tạo biến thứ 2: Số trang hiển thị
            int pageNumber = (page ?? 1);
            LoaiSanPham LSP = db.LoaiSanPhams.SingleOrDefault(x => x.MaLoaiSP == MaLoaiSP);
            if (LSP != null)
            {
                ViewBag.TenLoaiSP = LSP.TenLoaiSP;
            }

            NhaSanXuat NSX = db.NhaSanXuats.SingleOrDefault(x => x.MaNSX == MaNSX);
            if (NSX != null)
            {
                ViewBag.TenNSX = NSX.TenNSX;
            }

            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
            ViewBag.HangSanXuat = db.NhaSanXuats.ToList();
            ViewBag.LoaiSanPham = db.LoaiSanPhams.ToList();
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
            if (txtTimKiem1 != null)
            {
                result = result.Where(x => x.TenSP.Contains(txtTimKiem1)).ToList();
                ViewBag.Timkiem1 = txtTimKiem1;
            }
            List<ItemSanPham> List = new List<ItemSanPham>();
            foreach (var item in result)
            {
                ItemSanPham itemSanPham = new ItemSanPham(item.MaSP);
                List.Add(itemSanPham);
            }
            if (Gia != null)
            {
                if (Gia == 1)
                {
                    List = List.Where(x => x.DonGia < 5000000).OrderBy(x => x.DonGia).ToList();
                }
                else if (Gia == 2)
                {
                    List = List.Where(x => x.DonGia >= 5000000 && x.DonGia < 10000000).OrderBy(x => x.DonGia).ToList();
                }
                else if (Gia == 3)
                {
                    List = List.Where(x => x.DonGia >= 10000000 && x.DonGia < 20000000).OrderBy(x => x.DonGia).ToList();
                }
                else if (Gia == 4)
                {
                    List = List.Where(x => x.DonGia >= 20000000 && x.DonGia < 50000000).OrderBy(x => x.DonGia).ToList();
                }
                else if (Gia == 5)
                {
                    List = List.Where(x => x.DonGia > 50000000).OrderBy(x => x.DonGia).ToList();
                }
                ViewBag.Gia = Gia;
            }
            if (SapXep != null)
            {
                ViewBag.SapXep = SapXep;
                if (SapXep.Contains("T"))
                {
                    List = List.OrderBy(c => c.DonGia).ToList();
                }
                else if (SapXep.Contains("G"))
                {
                    List = List.OrderByDescending(c => c.DonGia).ToList();
                }
            }
            return View(List.ToPagedList(pageNumber, pageSize));

        }
    }
}