namespace LuxyryWatch.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhSanPham")]
    public partial class AnhSanPham
    {
        [Key]
        public int MaAnhSP { get; set; }

        public int? MaSP { get; set; }

        public string TenAnhSP { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
