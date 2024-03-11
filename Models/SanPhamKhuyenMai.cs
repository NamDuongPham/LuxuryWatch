namespace LuxyryWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPhamKhuyenMai")]
    public partial class SanPhamKhuyenMai
    {
        [Key]
        public int MaSPKM { get; set; }

        public int? MaSP { get; set; }

        public int? MACTKM { get; set; }

        public decimal? GiaTriGiam { get; set; }

        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
