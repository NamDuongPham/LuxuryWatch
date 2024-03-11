namespace LuxyryWatch.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongTrinhKhuyenMai")]
    public partial class ChuongTrinhKhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhKhuyenMai()
        {
            SanPhamKhuyenMais = new HashSet<SanPhamKhuyenMai>();
        }

        [Key]
        public int MaCTKM { get; set; }

        [StringLength(255)]
        public string TenCTKM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGgayKetThuc { get; set; }

        public int? SoLuongSanPham { get; set; }

        public bool? ApDung { get; set; }

        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
    }
}
