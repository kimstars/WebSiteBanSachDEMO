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
    public class QuanLyChuDeController : Controller
    {
        //GET: QuanLyChuDe
        DataConnect db = new DataConnect();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.ChuDes.ToList().OrderBy(n => n.MaChuDe).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(ChuDe _ChuDe)
        {
            db.ChuDes.Add(_ChuDe);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(int _MaChuDe)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == _MaChuDe);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(ChuDe _ChuDe)
        {
            if (!ModelState.IsValid)
            {
                return View(_ChuDe);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Xoa(int _MaChuDe)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == _MaChuDe);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(int _MaChuDe)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == _MaChuDe);
            List<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == _MaChuDe).ToList();
            if ((chude == null) || (lstSach.Count > 0))
            {
                if (chude == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                if (lstSach.Count > 0)
                {
                    return View(chude);
                }
            }
            db.ChuDes.Remove(chude);
            return RedirectToAction("Index");
        }
    }
}