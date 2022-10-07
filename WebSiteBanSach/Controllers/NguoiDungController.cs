using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        DataConnect db = new DataConnect();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang _KhachHang)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            KhachHang khachhang = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == _KhachHang.TaiKhoan);
            if (khachhang != null)
            {
                ViewBag.ThongBao = "Tên Người dùng đã tồn tại";
                return View();
            }
            _KhachHang.Quyen = 0;
            db.KhachHangs.Add(_KhachHang);
            Session["TaiKhoan"] = _KhachHang;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection fc)
        {
            string textTaiKhoan = fc["txtTaiKhoan"].ToString();
            string textMatKhau = fc["txtMatKhau"].ToString();
            KhachHang _KhachHang = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == textTaiKhoan && n.MatKhau == textMatKhau);
            if (_KhachHang != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công";
                Session["TaiKhoan"] = _KhachHang;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Sai Tài khoản hoặc Mật khẩu";
            return View();
        }

        [HttpGet]
        public ActionResult ChinhSua(int _MaKH)
        {
            KhachHang khachhang = db.KhachHangs.SingleOrDefault(n => n.MaKH == _MaKH);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(KhachHang _KhachHang)
        {
            if (!ModelState.IsValid)
            {
                return View(_KhachHang);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HienThi(int _MaKH)
        {
            KhachHang khachhang = db.KhachHangs.SingleOrDefault(n => n.MaKH == _MaKH);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }

        public ActionResult NguoiDungPartial()
        {
            if ((Session["TaiKhoan"] == null) || (Session["TaiKhoan"].ToString() == ""))
            {
                return PartialView();
            }
            ViewBag.KhachHang = (KhachHang)Session["TaiKhoan"];
            return PartialView();
        }

        public ActionResult DangXuat()
        {
            if ((Session["TaiKhoan"] == null) || (Session["TaiKhoan"].ToString() == ""))
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            Session["TaiKhoan"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult NguoiDungQuanTri()
        {
            if ((Session["TaiKhoan"] == null) || (Session["TaiKhoan"].ToString() == ""))
            {
                return PartialView();
            }
            ViewBag.KhachHang = (KhachHang)Session["TaiKhoan"];
            return PartialView();
        }
    }
}