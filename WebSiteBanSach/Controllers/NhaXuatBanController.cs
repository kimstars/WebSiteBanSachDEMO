using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBan
        DataConnect db = new DataConnect();
        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.Take(5).OrderBy(nxb => nxb.MaNXB).ToList());
        }

        public ViewResult SachTheoNXB(int _MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == _MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return View();
            }
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == _MaNXB && n.SoLuongTon > 0).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có Sách của Nhà xuất bản này!";
            }
            return View(lstSach);
        }

        public ViewResult DanhMucNXB()
        {
            return View(db.NhaXuatBans.ToList());
        }
    }
}