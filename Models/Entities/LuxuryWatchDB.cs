using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LuxyryWatch.Models.Entities
{
    public partial class LuxuryWatchDB : DbContext
    {
        public LuxuryWatchDB()
            : base("name=LuxuryWatchDB")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public virtual DbSet<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual DbSet<ChuongTrinhKhuyenMai> ChuongTrinhKhuyenMais { get; set; }
        public virtual DbSet<DoiTacThanhToan> DoiTacThanhToans { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<LoaiThanhVien> LoaiThanhViens { get; set; }
        public virtual DbSet<LoaiThanhVien_Quyen> LoaiThanhVien_Quyen { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
        public virtual DbSet<ThongTin> ThongTins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnhSanPham>()
                .Property(e => e.TenAnhSP)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongTrinhKhuyenMai>()
                .HasMany(e => e.SanPhamKhuyenMais)
                .WithOptional(e => e.ChuongTrinhKhuyenMai)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDonDatHangs)
                .WithOptional(e => e.DonDatHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HinhThucThanhToan>()
                .HasMany(e => e.DoiTacThanhToans)
                .WithOptional(e => e.HinhThucThanhToan)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiThanhVien>()
                .HasMany(e => e.LoaiThanhVien_Quyen)
                .WithOptional(e => e.LoaiThanhVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiThanhVien_Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.PhieuNhaps)
                .WithOptional(e => e.NhaCungCap)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PhieuNhap>()
                .HasMany(e => e.ChiTietPhieuNhaps)
                .WithOptional(e => e.PhieuNhap)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.LoaiThanhVien_Quyen)
                .WithOptional(e => e.Quyen)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.AnhSanPhams)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.SanPhamKhuyenMais)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Slider>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTin>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTin>()
                .Property(e => e.Map)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTin>()
                .Property(e => e.GioiThieu)
                .IsUnicode(false);
        }
    }
}
