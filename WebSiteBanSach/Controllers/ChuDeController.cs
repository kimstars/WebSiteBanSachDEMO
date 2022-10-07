using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using WebSiteBanSach.Models.Metadata;

namespace WebSiteBanSach.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        DataConnect db = new DataConnect();
        public ActionResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(3).ToList());
        }

        public ViewResult SachTheoChuDe(int _MaChuDe=0)
        {
            ChuDe chude=db.ChuDes.SingleOrDefault(n=>n.MaChuDe==_MaChuDe);
            if(chude==null)
            {
                Response.StatusCode=404;
                return View();
            }
            List<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == _MaChuDe && n.SoLuongTon > 0).OrderBy(n => n.GiaBan).ToList();
            if(lstSach.Count==0)
            {
                ViewBag.Sach = "Không có Sách nào thuộc Chủ đề này!";
            }
            return View(lstSach);
        }

        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
    }
}