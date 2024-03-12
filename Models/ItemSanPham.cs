using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxyryWatch.Models
{
    public class ItemSanPham
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal? DonGia { get; set; }
        public string HinhAnh { get; set; }
        public bool? Moi { get; set; }

        // contructor
        public ItemSanPham(int MaSP)
        {
            using(LuxuryWatch_DB db = new LuxuryWatch_DB())
            {
                this.MaSP = MaSP;
                this.TenSP = db.SanPhams.Single(x => x.MaSP == MaSP).TenSP;
                this.DonGia = db.SanPhams.Single(x => x.MaSP == MaSP).DonGia;
                this.Moi = db.SanPhams.Single(x => x.MaSP== MaSP).Moi;

                ChuongTrinhKhuyenMai CTKM = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.NGgayKetThuc > DateTime.Now && x.ApDung == true);
                if(CTKM != null)
                {

                    SanPhamKhuyenMai SPKM = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MaSP == MaSP && x.MACTKM == CTKM.MaCTKM);
                    if (SPKM != null)
                    {
                        decimal giaTriGiam = (decimal)SPKM.GiaTriGiam;
                        // tinh gia sau khi giam
                        this.DonGia = this.DonGia * ((100 - giaTriGiam) / 100);
                    }
                }
                this.HinhAnh = null;
                IEnumerable<AnhSanPham> LASP = db.AnhSanPhams.Where(x => x.MaSP == MaSP).ToList();
                if (LASP.Count() > 0)
                {
                    AnhSanPham ASP = LASP.First();
                    this.HinhAnh = ASP.TenAnhSP;
                }
            }
        }
    }
}