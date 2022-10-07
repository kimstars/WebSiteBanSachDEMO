using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: TacGia
        DataConnect db = new DataConnect();
        public ActionResult TacGiaPartial()
        {
            return PartialView(db.ChuDes.Take(3).ToList());
        }

        public ViewResult SachTheoTacGia(int _MaTacGia = 0)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == _MaTacGia);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return View();
            }
            List<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == _MaTacGia && n.SoLuongTon > 0).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có Sách nào có tác giả này!";
            }
            return View(lstSach);
        }

        public ViewResult DanhMucTacGia()
        {
            return View(db.ChuDes.ToList());
        }
    }
}