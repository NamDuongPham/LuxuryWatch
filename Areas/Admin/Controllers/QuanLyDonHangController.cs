using LuxyryWatch.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, NhanVien")]
    public class QuanLyDonHangController : AdminBaseController
    {
        // GET: Admin/QuanLyDonHang
        LuxuryWatch_DB db = new LuxuryWatch_DB();
        public ActionResult DonHangMoi(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == false && x.HoanThanh == false && x.DaHuy == false);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == false && x.HoanThanh == false && x.DaHuy == false && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }

        // don hang dang cho xu ly
        public ActionResult DonDangXuLy(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.HoanThanh == false && x.DaHuy == false);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.HoanThanh == false && x.DaHuy == false && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }

        // don hang da xu ly 
        public ActionResult DonHangDaHoanThanh(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.DaThanhToan == true && x.DaHuy == false && x.HoanThanh == true);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.DaThanhToan == true && x.DaHuy == false && x.HoanThanh == true && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }

        // don hang da huy 
        public ActionResult DonHangDaHuy(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.DaHuy == true);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.DaHuy == true && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CapNhatThongTin(int? MaDDH, string diachi, string ghichu)
        {
            if (MaDDH == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == MaDDH);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.DiaChiNhanHang = diachi;
            model.GhiChu = ghichu;
            db.SaveChanges();
            return Content("<script>alert('Cập thông tin khách hàng thành công!'); window.location.reload();</script>");

            //return Content("<script>window.location.reload();</script>");
        }

        // duyet don hang
        public ActionResult DuyetDonHang(int? MaDDH)
        {
            if (MaDDH == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang model = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == MaDDH);
            if (model == null)
            {
                return HttpNotFound();
            }
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == MaDDH).ToList();
            ViewBag.ListChiTietDH = listChiTietDH;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DuyetDonHang(DonDatHang dDH, string email)
        {

            // Gán danh sách đối tác thanh toán cho ViewBag
            DonDatHang dDHUpdate = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == dDH.MaDDH);
            dDHUpdate.TinhTrangGiaoHang = dDH.TinhTrangGiaoHang;
            dDHUpdate.DaThanhToan = dDH.DaThanhToan;
            dDHUpdate.DaHuy = dDH.DaHuy;
            dDHUpdate.NgayGiao = dDH.NgayGiao;
            dDHUpdate.DaHuy = dDH.DaHuy;
            dDHUpdate.HoanThanh = dDH.HoanThanh;
            dDHUpdate.MaHTTT = dDH.MaHTTT;

            db.SaveChanges();
            DonDatHang ddhkh = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == dDH.MaDDH);
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == dDH.MaDDH).ToList();
            ViewBag.ListChiTietDH = listChiTietDH;
            ViewBag.email = email;
            if (dDH.TinhTrangGiaoHang == true && dDH.HoanThanh == false && dDH.DaHuy == false)
            {

                //GuiEmail("Xác nhận đơn hàng của hệ thống", ddhkh.KhachHang.Email, "avanh090@gmail.com", "jhvpedzqhrnyamsr.", "Đơn hàng của bạn sẽ được giao và ngày: " + dDHUpdate.NgayGiao.Value.ToString("dd/MM/yyyy") + email);
                //return Content("<script>alert('Đơn hàng đang chờ xử lý!'); window.location.reload();</script>");

                return RedirectToAction("DonHangDangXuLy");
            }
            else if (dDH.HoanThanh == true)
            {
                return RedirectToAction("DonHangDaHoanThanh");
            }
            else if (dDH.DaHuy == true)
            {
                foreach (var item in listChiTietDH)
                {
                    SanPham SP = db.SanPhams.SingleOrDefault(x => x.MaSP == item.MaSP);
                    if (SP != null)
                    {
                        SP.SoLuongTon += item.SoLuong;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("DonHangDaHuy");
            }
            else
            {
                return View(dDHUpdate);
            }
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