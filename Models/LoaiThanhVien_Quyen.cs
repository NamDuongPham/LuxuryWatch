namespace LuxyryWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoaiThanhVien_Quyen
    {
        [Key]
        public int MaLTVQ { get; set; }

        public int? MaLoaiTV { get; set; }

        [StringLength(100)]
        public string MaQuyen { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
