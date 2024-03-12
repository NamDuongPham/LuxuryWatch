using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxyryWatch.Models
{
    public class ItemGioHang
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal? DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal? ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public ItemGioHang(int MaSP)
        {
            using (LuxuryWatch_DB db = new LuxuryWatch_DB())
            {
                this.MaSP = MaSP;
                this.TenSP = db.SanPhams.Single(x => x.MaSP == MaSP).TenSP;
                this.DonGia = db.SanPhams.Single(x => x.MaSP == MaSP).DonGia;
                ChuongTrinhKhuyenMai CTKM = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.NGgayKetThuc > DateTime.Now && x.ApDung == true);
                if (CTKM != null)
                {
                    SanPhamKhuyenMai SPKM = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MaSP == MaSP && x.MACTKM == CTKM.MaCTKM);
                    if (SPKM != null)
                    {
                        decimal giatrigiam = (decimal)SPKM.GiaTriGiam;
                        this.DonGia = this.DonGia * ((100 - giatrigiam) / 100);
                    }
                }
                this.HinhAnh = null;
                IEnumerable<AnhSanPham> LASP = db.AnhSanPhams.Where(x => x.MaSP == MaSP).ToList();
                if (LASP.Count() > 0)
                {
                    AnhSanPham ASP = LASP.First();
                    this.HinhAnh = ASP.TenAnhSP;
                }
                this.SoLuong = 1;
                this.ThanhTien = DonGia;
            }

        }

        public ItemGioHang(int MaSP, int SoLuong)
        {
            using (LuxuryWatch_DB db = new LuxuryWatch_DB())
            {
                this.MaSP = MaSP;
                this.TenSP = db.SanPhams.Single(x => x.MaSP == MaSP).TenSP;
                this.DonGia = db.SanPhams.Single(x => x.MaSP == MaSP).DonGia;
                ChuongTrinhKhuyenMai CTKM = db.ChuongTrinhKhuyenMais.Single(x => x.NGgayKetThuc > DateTime.Now && x.ApDung == true);
                if (CTKM != null)
                {
                    SanPhamKhuyenMai SPKM = db.SanPhamKhuyenMais.Single(x => x.MaSP == MaSP && x.MACTKM == CTKM.MaCTKM);
                    if (SPKM != null)
                    {
                        decimal giatrigiam = (decimal)SPKM.GiaTriGiam;
                        this.DonGia = this.DonGia * ((100 - giatrigiam) / 100);
                    }
                }
                this.HinhAnh = null;
                IEnumerable<AnhSanPham> LASP = db.AnhSanPhams.Where(x => x.MaSP == MaSP).ToList();
                if (LASP.Count() > 0)
                {
                    AnhSanPham ASP = LASP.First();
                    this.HinhAnh = ASP.TenAnhSP;
                }
                this.SoLuong = SoLuong;
                this.ThanhTien = DonGia * SoLuong;
            }

        }
    }
}