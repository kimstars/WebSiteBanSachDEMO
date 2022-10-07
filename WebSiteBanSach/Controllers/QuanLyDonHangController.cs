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
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        DataConnect db = new DataConnect();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.DonHangs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ChinhSua(int _MaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == _MaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(donhang);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(DonHang _DonHang)
        {
            if (!ModelState.IsValid)
            {
                return View(_DonHang);
            }
            return RedirectToAction("Index");
        }
    }
}