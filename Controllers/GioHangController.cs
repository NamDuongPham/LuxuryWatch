using LuxyryWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Controllers
{
    public class GioHangController : Controller
    {
        LuxuryWatch_DB db= new LuxuryWatch_DB();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongTien = TinhTongThanhTien();
            List<ItemGioHang> listGioHang = LayGioHang();
            return PartialView(listGioHang);
        }
        //public ActionResult HienThiGioHang()
        //{
        //    ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
        //    List<ItemGioHang> listGioHang = LayGioHang();
        //    bool IsLoggedIn = Session["TaiKhoan"] != null;

        //    // Truyền biến IsLoggedIn vào ViewBag
        //    ViewBag.IsLoggedIn = IsLoggedIn;
        //    if (Session["TaiKhoan"] == null)
        //    {
        //        ViewBag.uuDai = 0;
        //    }
        //    else
        //    {
        //        ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
        //        LoaiThanhVien ltv = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == tv.MaLoaiTV);
        //        ViewBag.Hoten = tv.Hoten;
        //        ViewBag.Email = tv.Email;
        //        ViewBag.SDT = tv.SoDienThoai;
        //        ViewBag.uuDai = ltv.uuDai;
        //    }
        //    return View(listGioHang);
        //}
        public ActionResult HienThiGioHang()
        {
            ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
            List<ItemGioHang> listGioHang = LayGioHang();
            bool IsLoggedIn = Session["TaiKhoan"] != null;

            // Truyền biến IsLoggedIn vào ViewBag
            ViewBag.IsLoggedIn = IsLoggedIn;

            ThanhVien tv = LayThongTinThanhVien();

            if (tv != null)
            {
                ViewBag.Hoten = tv.Hoten;
                ViewBag.Email = tv.Email;
                ViewBag.SDT = tv.SoDienThoai;

                // Lấy thông tin loại thành viên
                LoaiThanhVien ltv = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == tv.MaLoaiTV);
                if (ltv != null)
                {
                    ViewBag.uuDai = ltv.uuDai;
                }
                else
                {
                    ViewBag.uuDai = 0; // Set a default value if ltv is null
                }
            }
            return View(listGioHang);
        }


        public List<ItemGioHang> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (listGioHang == null)
            {
                //Nếu giỏ hàng chư tồn tại
                listGioHang = new List<ItemGioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }


        public ActionResult ThemGioHang(int MaSP, string strUrl)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                //Đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> listGioHang = LayGioHang();
            ItemGioHang checkSP = listGioHang.SingleOrDefault(x => x.MaSP == MaSP);
            if (checkSP != null)
            {
                if (sp.SoLuongTon <= 0)
                {
                    return Redirect(strUrl);
                }
                if (sp.SoLuongTon <= checkSP.SoLuong)
                {
                    return Redirect(strUrl);
                }
                checkSP.SoLuong++;
                checkSP.ThanhTien = checkSP.DonGia * checkSP.SoLuong;
                return Redirect(strUrl);
            }
            if (sp.SoLuongTon > 0)
            {
                ItemGioHang itemGioHang = new ItemGioHang(MaSP);
                listGioHang.Add(itemGioHang);
            }

            return Redirect(strUrl);
        }
        public int TinhTongSoLuong()
        {
            List<ItemGioHang> listGioHang = LayGioHang();
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.SoLuong);
        }
        public decimal? TinhTongThanhTien()
        {
            List<ItemGioHang> listGioHang = LayGioHang();
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.ThanhTien);
        }

        public ActionResult XoaItemGioHang(int MaSP)
        {
            List<ItemGioHang> list = LayGioHang();
            ItemGioHang item = list.SingleOrDefault(x => x.MaSP == MaSP);
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            list.Remove(item);
            return RedirectToAction("HienThiGioHang");
        }

        //[HttpPost]
        //public ActionResult CapNhatSoLuong(int MaSP, int soLuong)
        //{
        //    // Tìm sản phẩm trong cơ sở dữ liệu
        //    List<ItemGioHang> list = LayGioHang();
        //    ItemGioHang item = list.SingleOrDefault(x => x.MaSP == MaSP);
        //    if (item != null)
        //    {
        //        // Cập nhật số lượng mới của sản phẩm
        //        item.SoLuong = soLuong;
        //        // Lưu thay đổi vào cơ sở dữ liệu
        //        db.SaveChanges();
        //        // Trả về kết quả thành công
        //        return Json(new { success = true });
        //    }
        //    // Trả về kết quả thất bại nếu không tìm thấy sản phẩm
        //    return Json(new { success = false });
        //}
        public ActionResult CapNhatSoLuong(int MaSP, int SoLuong)
        {
            List<ItemGioHang> list = LayGioHang();
            ItemGioHang item = list.SingleOrDefault(x => x.MaSP == MaSP);
            if (item != null)
            {
                item.SoLuong = SoLuong;
                item.ThanhTien = item.DonGia * item.SoLuong;
            }
            return Json(new { TongSoLuong = TinhTongSoLuong(), TongThanhTien = TinhTongThanhTien() });
        }
        private ThanhVien LayThongTinThanhVien()
        {
            if (Session["TaiKhoan"] != null)
            {
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                return tv;
            }
            return null;
        }

        public ActionResult DatHang(string HoTen, string SDT, string Email, string DiaChi, string GhiChu)
        {
            KhachHang KH = new KhachHang();
            LoaiThanhVien ltv = null;
            double uudai = 0;
            string temp = "";
            if (Session["TaiKhoan"] == null)
            {
                KhachHang NKH = new KhachHang();
                NKH.TenKH = HoTen;
                NKH.SoDienThoai = SDT;
                NKH.MaTV = null;
                NKH.DiaChi = DiaChi;
                NKH.Email = Email;
                temp = Email;
                db.KhachHangs.Add(NKH);
                db.SaveChanges();
                KH = NKH;
                
            }
            else
            {
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                ltv = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == tv.MaLoaiTV);
                temp = tv.Email;
                //Kiểm tra khách hàng đã tồn tại trong csdl hay chưa
                KhachHang checkKH = db.KhachHangs.SingleOrDefault(x => x.MaTV == tv.MaTV);
                KH = checkKH;
                if (checkKH == null)
                {
                    KhachHang nkh = new KhachHang();
                    nkh.TenKH = tv.Hoten;
                    nkh.Email = tv.Email;
                    temp = tv.Email;
                    nkh.DiaChi = tv.DiaChi;
                    nkh.SoDienThoai = tv.SoDienThoai;
                    nkh.MaTV = tv.MaTV;
                    db.KhachHangs.Add(nkh);
                    db.SaveChanges();
                    KH = nkh;
                }
            }
            if (ltv == null)
            {
                uudai = 0;
            }
            else
            {
                uudai = (double)ltv.uuDai;
                
            }

            //Tạo mới đơn hàng
            DonDatHang dDH = new DonDatHang();
            List<ItemGioHang> list = LayGioHang();
            dDH.NgayDat = DateTime.Now;
            dDH.TinhTrangGiaoHang = false;
            dDH.NgayGiao = null;
            dDH.DaThanhToan = false;
            dDH.DaHuy = false;
            dDH.HoanThanh = false;
            dDH.MAKH = KH.Makh;
            dDH.DiaChiNhanHang = DiaChi;
            dDH.GhiChu = GhiChu;
            dDH.UuDai = uudai;
            dDH.TongThanhToan = list.Sum(x => x.ThanhTien).Value - ((decimal?)((double)list.Sum(x => x.ThanhTien).Value * uudai) / 100);
            db.DonDatHangs.Add(dDH);
            db.SaveChanges();
            //Thêm chi tiết đơn đặt hàng
            foreach (var item in list)
            {
                SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == item.MaSP);
                sp.SoLanMua++;
                sp.SoLuongTon -= item.SoLuong;
                db.SaveChanges();
                ChiTietDonDatHang cTDDH = new ChiTietDonDatHang();
                cTDDH.MaDDH = dDH.MaDDH;
                cTDDH.MaSP = item.MaSP;
                cTDDH.SoLuong = item.SoLuong;
                cTDDH.DonGia = item.DonGia;
                cTDDH.ThanhTien = item.ThanhTien;
                db.ChiTietDonDatHangs.Add(cTDDH);
            }
            db.SaveChanges();

            // thuc hien chuc nang sendMail cho khach hang 
            // duyet don hang len form
            var strSanPham = "";
            foreach (var sp in list)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>" + sp.TenSP + "</td>";
                strSanPham += "<td>" + sp.SoLuong + "</td>";
                strSanPham += "<td>" + sp.DonGia.Value.ToString("#,##") + "</td>";
                strSanPham += "<tr>";


            }

            //string contentCustumer = Server.MapPath("~/Content/ckeditor/samples/send2.html");
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/ckeditor/samples/send2.html"));
            contentCustomer = contentCustomer.Replace("{{MaDon}}", dDH.MaDDH.ToString());
            contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", dDH.NgayDat.ToString());
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", dDH.KhachHang.TenKH);
            contentCustomer = contentCustomer.Replace("{{Phone}}", dDH.KhachHang.SoDienThoai);
            contentCustomer = contentCustomer.Replace("{{Email}}",temp);
            contentCustomer = contentCustomer.Replace("{{DiaChiGiaoHang}}", dDH.KhachHang.DiaChi);
            contentCustomer = contentCustomer.Replace("{{TongTien}}", dDH.TongThanhToan.Value.ToString("#,##"));
            // gia tri temp de kiem tra khach hang co phai thanh vien hay khong neu khong phai thi lay email tren form dien 
            GuiEmail("Xác nhận đơn hàng của hệ thống", temp, "avanh090@gmail.com", "jhvpedzqhrnyamsr", contentCustomer);
            Session["gioHang"] = null;
            return RedirectToAction("HienThiGioHang");
        }

        public void GuiEmail(string title, string toEmail, string fromEmail, string passWord, string content)
        {
            try
            {
                // Tạo đối tượng MailMessage
                MailMessage mail = new MailMessage();
                mail.To.Add(toEmail); // Địa chỉ nhận
                mail.From = new MailAddress(fromEmail); // Địa chỉ gửi
                mail.Subject = title; // Tiêu đề gửi
                mail.Body = content; // Nội dung
                mail.IsBodyHtml = true;

                // Cấu hình thông tin SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; // Host gửi của Gmail
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromEmail, passWord); // Tài khoản và mật khẩu người gửi
                smtp.EnableSsl = true; // Kích hoạt giao tiếp an toàn SSL

                // Gửi email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}