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
    public class HomeController : Controller
    {
        DataConnect db = new DataConnect();

        public ActionResult Index(int? _Page)
        {
            int PageSize = 9;
            int PageNumber = (_Page ?? 1);
            return View(db.Saches.Where(n => n.SoLuongTon > 0).OrderBy(n => n.MaSach).ToPagedList(PageNumber, PageSize));
        }

        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Take(15).ToList();
            return PartialView(lstSachMoi);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}