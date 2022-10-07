using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class QuanLyNXBController : Controller
    {
        // GET: QuanLyNXB
        DataConnect db = new DataConnect();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.NhaXuatBans.ToList().OrderBy(n => n.MaNXB).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(NhaXuatBan _NhaXuatBan)
        {
            db.NhaXuatBans.Add(_NhaXuatBan);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(int _MaNXB)
        {
            NhaXuatBan nhaxuatban = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == _MaNXB);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(NhaXuatBan _NhaXuatBan)
        {
            if (!ModelState.IsValid)
            {
                return View(_NhaXuatBan);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Xoa(int _MaNXB)
        {
            NhaXuatBan nhaxuatban = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == _MaNXB);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(int _MaNXB)
        {
            NhaXuatBan nhaxuatban = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == _MaNXB);
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == _MaNXB).ToList();
            if ((nhaxuatban == null) || (lstSach.Count > 0))
            {
                if (nhaxuatban == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                if (lstSach.Count > 0)
                {
                    return View(nhaxuatban);
                }
            }
            db.NhaXuatBans.Remove(nhaxuatban);
            return RedirectToAction("Index");
        }

        public ActionResult HienThi(int _MaNXB)
        {
            NhaXuatBan nhaxuatban = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == _MaNXB);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }
    }
}