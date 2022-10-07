using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        DataConnect db = new DataConnect();
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Where(n => n.SoLuongTon > 0).Take(3).ToList();
            return PartialView(lstSachMoi);
        }

        public ViewResult XemChitiet(int _MaSach = 0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == _MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.TenNhaXuatBan = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            //List<TacGia> tacgia = db.TacGias.Select(n => n.Ma);
            return View(sach);
        }
    }
}