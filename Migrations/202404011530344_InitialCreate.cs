namespace LuxyryWatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnhSanPham",
                c => new
                    {
                        MaAnhSP = c.Int(nullable: false, identity: true),
                        MaSP = c.Int(),
                        TenAnhSP = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.MaAnhSP)
                .ForeignKey("dbo.SanPham", t => t.MaSP, cascadeDelete: true)
                .Index(t => t.MaSP);
            
            CreateTable(
                "dbo.SanPham",
                c => new
                    {
                        MaSP = c.Int(nullable: false, identity: true),
                        TenSP = c.String(maxLength: 255),
                        DonGia = c.Decimal(precision: 18, scale: 2),
                        NgayCapNhat = c.DateTime(storeType: "date"),
                        ThongSo = c.String(),
                        MoTa = c.String(),
                        SoLuongTon = c.Int(),
                        SoLanMua = c.Int(),
                        Moi = c.Boolean(),
                        MaNCC = c.Int(),
                        MaNSX = c.Int(),
                        MaLoaiSP = c.Int(),
                    })
                .PrimaryKey(t => t.MaSP)
                .ForeignKey("dbo.NhaCungCap", t => t.MaNCC)
                .ForeignKey("dbo.LoaiSanPham", t => t.MaLoaiSP)
                .ForeignKey("dbo.NhaSanXuat", t => t.MaNSX)
                .Index(t => t.MaNCC)
                .Index(t => t.MaNSX)
                .Index(t => t.MaLoaiSP);
            
            CreateTable(
                "dbo.ChiTietDonDatHang",
                c => new
                    {
                        MaCTDDT = c.Int(nullable: false, identity: true),
                        MaDDH = c.Int(),
                        MaSP = c.Int(),
                        DonGia = c.Decimal(precision: 18, scale: 2),
                        SoLuong = c.Int(),
                        ThanhTien = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaCTDDT)
                .ForeignKey("dbo.DonDatHang", t => t.MaDDH, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSP)
                .Index(t => t.MaDDH)
                .Index(t => t.MaSP);
            
            CreateTable(
                "dbo.DonDatHang",
                c => new
                    {
                        MaDDH = c.Int(nullable: false, identity: true),
                        MAKH = c.Int(),
                        NgayDat = c.DateTime(storeType: "date"),
                        NgayGiao = c.DateTime(storeType: "date"),
                        MaHTTT = c.Int(),
                        MaDTTT = c.Int(),
                        TinhTrangGiaoHang = c.Boolean(),
                        DaThanhToan = c.Boolean(),
                        DaHuy = c.Boolean(),
                        TongThanhToan = c.Decimal(precision: 18, scale: 2),
                        DiaChiNhanHang = c.String(maxLength: 500),
                        GhiChu = c.String(),
                        HoanThanh = c.Boolean(),
                        UuDai = c.Double(),
                    })
                .PrimaryKey(t => t.MaDDH)
                .ForeignKey("dbo.DoiTacThanhToan", t => t.MaDTTT)
                .ForeignKey("dbo.HinhThucThanhToan", t => t.MaHTTT)
                .ForeignKey("dbo.KhachHang", t => t.MAKH)
                .Index(t => t.MAKH)
                .Index(t => t.MaHTTT)
                .Index(t => t.MaDTTT);
            
            CreateTable(
                "dbo.DoiTacThanhToan",
                c => new
                    {
                        MaDTTT = c.Int(nullable: false, identity: true),
                        TenDTTT = c.String(maxLength: 255),
                        MaHTTT = c.Int(),
                    })
                .PrimaryKey(t => t.MaDTTT)
                .ForeignKey("dbo.HinhThucThanhToan", t => t.MaHTTT, cascadeDelete: true)
                .Index(t => t.MaHTTT);
            
            CreateTable(
                "dbo.HinhThucThanhToan",
                c => new
                    {
                        MaHTTT = c.Int(nullable: false, identity: true),
                        TenHTTT = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MaHTTT);
            
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        Makh = c.Int(nullable: false, identity: true),
                        TenKH = c.String(maxLength: 255),
                        DiaChi = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255, unicode: false),
                        SoDienThoai = c.String(maxLength: 20, unicode: false),
                        MaTV = c.Int(),
                    })
                .PrimaryKey(t => t.Makh)
                .ForeignKey("dbo.ThanhVien", t => t.MaTV)
                .Index(t => t.MaTV);
            
            CreateTable(
                "dbo.ThanhVien",
                c => new
                    {
                        MaTV = c.Int(nullable: false, identity: true),
                        TaiKhoan = c.String(maxLength: 100, unicode: false),
                        MatKhau = c.String(maxLength: 100, unicode: false),
                        Hoten = c.String(maxLength: 255),
                        DiaChi = c.String(maxLength: 255),
                        SoDienThoai = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100, unicode: false),
                        MaLoaiTV = c.Int(),
                    })
                .PrimaryKey(t => t.MaTV)
                .ForeignKey("dbo.LoaiThanhVien", t => t.MaLoaiTV)
                .Index(t => t.MaLoaiTV);
            
            CreateTable(
                "dbo.LoaiThanhVien",
                c => new
                    {
                        MaLoaiTV = c.Int(nullable: false, identity: true),
                        TenLoaiTV = c.String(maxLength: 255),
                        uuDai = c.Double(),
                    })
                .PrimaryKey(t => t.MaLoaiTV);
            
            CreateTable(
                "dbo.LoaiThanhVien_Quyen",
                c => new
                    {
                        MaLTVQ = c.Int(nullable: false, identity: true),
                        MaLoaiTV = c.Int(),
                        MaQuyen = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.MaLTVQ)
                .ForeignKey("dbo.Quyen", t => t.MaQuyen, cascadeDelete: true)
                .ForeignKey("dbo.LoaiThanhVien", t => t.MaLoaiTV, cascadeDelete: true)
                .Index(t => t.MaLoaiTV)
                .Index(t => t.MaQuyen);
            
            CreateTable(
                "dbo.Quyen",
                c => new
                    {
                        MaQuyen = c.String(nullable: false, maxLength: 100, unicode: false),
                        TenQuyen = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MaQuyen);
            
            CreateTable(
                "dbo.ChiTietPhieuNhap",
                c => new
                    {
                        MaCTPN = c.Int(nullable: false, identity: true),
                        MaPN = c.Int(),
                        MaSP = c.Int(),
                        DonGiaNhap = c.Decimal(precision: 18, scale: 2),
                        SoLuongNhap = c.Int(),
                    })
                .PrimaryKey(t => t.MaCTPN)
                .ForeignKey("dbo.PhieuNhap", t => t.MaPN, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSP)
                .Index(t => t.MaPN)
                .Index(t => t.MaSP);
            
            CreateTable(
                "dbo.PhieuNhap",
                c => new
                    {
                        MaPN = c.Int(nullable: false, identity: true),
                        MaNCC = c.Int(),
                        NgayLap = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.MaPN)
                .ForeignKey("dbo.NhaCungCap", t => t.MaNCC, cascadeDelete: true)
                .Index(t => t.MaNCC);
            
            CreateTable(
                "dbo.NhaCungCap",
                c => new
                    {
                        MaNCC = c.Int(nullable: false, identity: true),
                        TenNCC = c.String(maxLength: 255),
                        DiaChi = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255, unicode: false),
                        SoDienThoai = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.MaNCC);
            
            CreateTable(
                "dbo.LoaiSanPham",
                c => new
                    {
                        MaLoaiSP = c.Int(nullable: false, identity: true),
                        TenLoaiSP = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MaLoaiSP);
            
            CreateTable(
                "dbo.NhaSanXuat",
                c => new
                    {
                        MaNSX = c.Int(nullable: false, identity: true),
                        TenNSX = c.String(maxLength: 255),
                        ThongTin = c.String(),
                    })
                .PrimaryKey(t => t.MaNSX);
            
            CreateTable(
                "dbo.SanPhamKhuyenMai",
                c => new
                    {
                        MaSPKM = c.Int(nullable: false, identity: true),
                        MaSP = c.Int(),
                        MACTKM = c.Int(),
                        GiaTriGiam = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaSPKM)
                .ForeignKey("dbo.ChuongTrinhKhuyenMai", t => t.MACTKM, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSP, cascadeDelete: true)
                .Index(t => t.MaSP)
                .Index(t => t.MACTKM);
            
            CreateTable(
                "dbo.ChuongTrinhKhuyenMai",
                c => new
                    {
                        MaCTKM = c.Int(nullable: false, identity: true),
                        TenCTKM = c.String(maxLength: 255),
                        NgayBatDau = c.DateTime(storeType: "date"),
                        NGgayKetThuc = c.DateTime(storeType: "date"),
                        SoLuongSanPham = c.Int(),
                        ApDung = c.Boolean(),
                        Anh = c.String(),
                    })
                .PrimaryKey(t => t.MaCTKM);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        MaSlider = c.Int(nullable: false, identity: true),
                        Anh = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.MaSlider);
            
            CreateTable(
                "dbo.ThongTin",
                c => new
                    {
                        MaTT = c.Int(nullable: false, identity: true),
                        SDT = c.String(maxLength: 50, unicode: false),
                        DiaChi = c.String(maxLength: 50),
                        Email = c.String(maxLength: 250, unicode: false),
                        Map = c.String(unicode: false),
                        GioiThieu = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.MaTT);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhamKhuyenMai", "MaSP", "dbo.SanPham");
            DropForeignKey("dbo.SanPhamKhuyenMai", "MACTKM", "dbo.ChuongTrinhKhuyenMai");
            DropForeignKey("dbo.SanPham", "MaNSX", "dbo.NhaSanXuat");
            DropForeignKey("dbo.SanPham", "MaLoaiSP", "dbo.LoaiSanPham");
            DropForeignKey("dbo.ChiTietPhieuNhap", "MaSP", "dbo.SanPham");
            DropForeignKey("dbo.SanPham", "MaNCC", "dbo.NhaCungCap");
            DropForeignKey("dbo.PhieuNhap", "MaNCC", "dbo.NhaCungCap");
            DropForeignKey("dbo.ChiTietPhieuNhap", "MaPN", "dbo.PhieuNhap");
            DropForeignKey("dbo.ChiTietDonDatHang", "MaSP", "dbo.SanPham");
            DropForeignKey("dbo.ThanhVien", "MaLoaiTV", "dbo.LoaiThanhVien");
            DropForeignKey("dbo.LoaiThanhVien_Quyen", "MaLoaiTV", "dbo.LoaiThanhVien");
            DropForeignKey("dbo.LoaiThanhVien_Quyen", "MaQuyen", "dbo.Quyen");
            DropForeignKey("dbo.KhachHang", "MaTV", "dbo.ThanhVien");
            DropForeignKey("dbo.DonDatHang", "MAKH", "dbo.KhachHang");
            DropForeignKey("dbo.DonDatHang", "MaHTTT", "dbo.HinhThucThanhToan");
            DropForeignKey("dbo.DoiTacThanhToan", "MaHTTT", "dbo.HinhThucThanhToan");
            DropForeignKey("dbo.DonDatHang", "MaDTTT", "dbo.DoiTacThanhToan");
            DropForeignKey("dbo.ChiTietDonDatHang", "MaDDH", "dbo.DonDatHang");
            DropForeignKey("dbo.AnhSanPham", "MaSP", "dbo.SanPham");
            DropIndex("dbo.SanPhamKhuyenMai", new[] { "MACTKM" });
            DropIndex("dbo.SanPhamKhuyenMai", new[] { "MaSP" });
            DropIndex("dbo.PhieuNhap", new[] { "MaNCC" });
            DropIndex("dbo.ChiTietPhieuNhap", new[] { "MaSP" });
            DropIndex("dbo.ChiTietPhieuNhap", new[] { "MaPN" });
            DropIndex("dbo.LoaiThanhVien_Quyen", new[] { "MaQuyen" });
            DropIndex("dbo.LoaiThanhVien_Quyen", new[] { "MaLoaiTV" });
            DropIndex("dbo.ThanhVien", new[] { "MaLoaiTV" });
            DropIndex("dbo.KhachHang", new[] { "MaTV" });
            DropIndex("dbo.DoiTacThanhToan", new[] { "MaHTTT" });
            DropIndex("dbo.DonDatHang", new[] { "MaDTTT" });
            DropIndex("dbo.DonDatHang", new[] { "MaHTTT" });
            DropIndex("dbo.DonDatHang", new[] { "MAKH" });
            DropIndex("dbo.ChiTietDonDatHang", new[] { "MaSP" });
            DropIndex("dbo.ChiTietDonDatHang", new[] { "MaDDH" });
            DropIndex("dbo.SanPham", new[] { "MaLoaiSP" });
            DropIndex("dbo.SanPham", new[] { "MaNSX" });
            DropIndex("dbo.SanPham", new[] { "MaNCC" });
            DropIndex("dbo.AnhSanPham", new[] { "MaSP" });
            DropTable("dbo.ThongTin");
            DropTable("dbo.Slider");
            DropTable("dbo.ChuongTrinhKhuyenMai");
            DropTable("dbo.SanPhamKhuyenMai");
            DropTable("dbo.NhaSanXuat");
            DropTable("dbo.LoaiSanPham");
            DropTable("dbo.NhaCungCap");
            DropTable("dbo.PhieuNhap");
            DropTable("dbo.ChiTietPhieuNhap");
            DropTable("dbo.Quyen");
            DropTable("dbo.LoaiThanhVien_Quyen");
            DropTable("dbo.LoaiThanhVien");
            DropTable("dbo.ThanhVien");
            DropTable("dbo.KhachHang");
            DropTable("dbo.HinhThucThanhToan");
            DropTable("dbo.DoiTacThanhToan");
            DropTable("dbo.DonDatHang");
            DropTable("dbo.ChiTietDonDatHang");
            DropTable("dbo.SanPham");
            DropTable("dbo.AnhSanPham");
        }
    }
}
