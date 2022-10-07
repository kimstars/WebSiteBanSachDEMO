using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class GioHangController : Controller
    {

        QuanLyBanSachModel db = new QuanLyBanSachModel();

        #region Gio hang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int __MaSach, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == __MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang _GioHang = lstGioHang.Find(n => n._MaSach == __MaSach);
            if (_GioHang == null)
            {
                _GioHang = new GioHang(__MaSach);
                lstGioHang.Add(_GioHang);
                return Redirect(strURL);
            }
            else
            {
                if (_GioHang._SoLuong < sach.SoLuongTon)
                {
                    _GioHang._SoLuong++;
                }
                return Redirect(strURL);
            }
        }

        public ActionResult CapNhatGioHang(int __MaSach, FormCollection fc)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == __MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang giohang = lstGioHang.SingleOrDefault(n => n._MaSach == __MaSach);
            if (giohang != null)
            {
                int SoLuong = Convert.ToInt32(fc["txtSoLuong"].ToString());
                if (SoLuong > 0)
                {
                    if (SoLuong >= sach.SoLuongTon)
                    {
                        giohang._SoLuong = Convert.ToInt32(sach.SoLuongTon);
                    }
                    else
                    {
                        giohang._SoLuong = SoLuong;
                    }
                }
                else
                {
                    lstGioHang.RemoveAll(n => n._MaSach == __MaSach);
                }
                if (lstGioHang.Count == 0)
                {
                    Session["GioHang"] = null;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("GioHang");
                }
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang(int __MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == __MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang giohang = lstGioHang.SingleOrDefault(n => n._MaSach == __MaSach);
            if (giohang != null)
            {
                lstGioHang.RemoveAll(n => n._MaSach == __MaSach);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        private int TongSoLuong()
        {
            int _TongsoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                _TongsoLuong = lstGioHang.Sum(n => n._SoLuong);
            }
            return _TongsoLuong;
        }

        private double TongTien()
        {
            double _TongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                _TongTien = lstGioHang.Sum(n => n._ThanhTien);
            }
            return _TongTien;
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }

        public ActionResult GioHangTongTien()
        {
            if (TongTien() <= 0)
            {
                return PartialView();
            }
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion

        #region Dat hang
        [HttpPost]
        public ActionResult DatHang()
        {
            if ((Session["TaiKhoan"] == null) || (Session["TaiKhoan"].ToString() == ""))
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            dh.MaKH = kh.MaKH;
            dh.DaThanhToan = Convert.ToInt32(TongTien());
            dh.TinhTrangGiaoHang = 0;
            dh.NgayDat = DateTime.Now;
            dh.NgayGiao = DateTime.Now;
            db.DonHangs.Add(dh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDonHang = dh.MaDonHang;
                ctdh.MaSach = item._MaSach;
                ctdh.SoLuong = item._SoLuong;
                ctdh.DonGia = item._DonGia.ToString();
                db.ChiTietDonHangs.Add(ctdh);

                Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == item._MaSach);
                sach.SoLuongTon -= item._SoLuong;
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HoaDon()
        {
            if ((Session["TaiKhoan"] == null) || (Session["TaiKhoan"].ToString() == ""))
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.KhachHang = (KhachHang)Session["TaiKhoan"];
            return View(lstGioHang);
        }
        #endregion
    }
}