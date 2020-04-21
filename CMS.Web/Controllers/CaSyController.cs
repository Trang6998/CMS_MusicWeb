using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class CaSyController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.DanhSachCaSy = db.CaSy.Include(x => x.CaSy_BaiHat).ToList();
                ViewBag.DanhSachBaiHatMoi = db.BaiHat.Include(x => x.TheLoai)
                                                     .Include(x => x.CaSy_BaiHat.Select(y => y.CaSy))
                                                     .OrderByDescending(x => x.NgayDang)
                                                     .Take(3).ToList();
            }
            return View();
        }
        public ActionResult ChiTietCaSy(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.CaSy.Include(x => x.CaSy_BaiHat);
                CaSy caSy = query.FirstOrDefault(x => x.CaSyID == id);

                ViewBag.ChiTietCaSy = caSy;
                ViewBag.DanhSachBaiHat = db.BaiHat.Include(x => x.TheLoai)
                                                  .Include(x => x.CaSy_BaiHat.Select(y => y.CaSy))
                                           .Where(x => x.CaSy_BaiHat.Any(y => y.CaSyID == id))
                                           .OrderByDescending(x => x.NgayDang)
                                           .ToList();
                var lstTheLoai = db.TheLoai.Where(x => x.BaiHats.Any(y => y.CaSy_BaiHat.Any(z => z.CaSyID == id)));
                ViewBag.CacCaSyKhac = query.Where(x => x.CaSy_BaiHat.Any(y => lstTheLoai.Select(z => z.TheLoaiID)
                                                                                        .Contains((int)y.BaiHat.TheLoaiID)))
                                           .Take(3).ToList();
            }
            return View();
        }
    }
}
