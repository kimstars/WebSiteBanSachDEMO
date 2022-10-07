using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        DataConnect db = new DataConnect();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Sach _Sach, HttpPostedFileBase FileUpload)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (FileUpload == null)
            {
                ViewBag.ThongBao = "Chưa chọn Ảnh bìa";
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View(_Sach);
            }
            var FileName = Path.GetFileName(FileUpload.FileName);
            var DuongDan = Path.Combine(Server.MapPath("~/HinhAnhSP"), FileName);
            if (!System.IO.File.Exists(DuongDan))
            {
                FileUpload.SaveAs(DuongDan);
            }
            _Sach.AnhBia = FileUpload.FileName;
            _Sach.NgayCapNhat = DateTime.Now;
            db.Saches.Add(_Sach);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(int _MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == _MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Sach _Sach, HttpPostedFileBase FileUpload)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", _Sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", _Sach.MaNXB);
            if (FileUpload == null)
            {
                ViewBag.ThongBao = "Chưa chọn Ảnh bìa";
                return View(_Sach);
            }
            if (!ModelState.IsValid)
            {
                return View(_Sach);
            }
            var FileName = Path.GetFileName(FileUpload.FileName);
            var DuongDan = Path.Combine(Server.MapPath("~/HinhAnhSP"), FileName);
            if (!System.IO.File.Exists(DuongDan))
            {
                FileUpload.SaveAs(DuongDan);
            }
            _Sach.AnhBia = FileUpload.FileName;
            return RedirectToAction("Index");
        }

        public ActionResult HienThi(int _MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == _MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpGet]
        public ActionResult Xoa(int _MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == _MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(int _MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == _MaSach);
            List<ChiTietDonHang> lstChiTietDonHang = db.ChiTietDonHangs.Where(n => n.MaSach == _MaSach).ToList();
            if ((sach == null) || (lstChiTietDonHang.Count > 0))
            {
                if (sach == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                if (lstChiTietDonHang.Count > 0)
                {
                    return View(sach);
                }
            }
            db.Saches.Remove(sach);
            return RedirectToAction("Index");
        }
    }
}