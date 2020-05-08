using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class AlbumController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.DanhSachAlbum = db.Album.Include(x => x.Album_BaiHat).Where(x => x.TrangThai == true).ToList();
            }
            return View();
        }
        public ActionResult ChiTietAlbum(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.Album.Include(x => x.Album_BaiHat);
                Album album = query.FirstOrDefault(x => x.AlbumID == id);

                ViewBag.ChiTietAlbum = album;
                ViewBag.DanhSachBaiHat = db.BaiHat.Include(x => x.TheLoai)
                                                  .Include(x => x.CaSy_BaiHat.Select(y => y.CaSy))
                                           .Where(x => x.Album_BaiHat.Any(y => y.AlbumID == id))
                                           .OrderByDescending(x => x.NgayDang)
                                           .ToList();
                var lstTheLoai = db.TheLoai.Where(x => x.BaiHats.Any(y => y.Album_BaiHat.Any(z => z.AlbumID == id)));
                ViewBag.CacAlbumKhac = query.Where(x => x.Album_BaiHat.Any(y => lstTheLoai.Select(z => z.TheLoaiID)
                                                                                        .Contains((int)y.BaiHat.TheLoaiID)))
                                           .Take(3).ToList();
            }
            return View();
        }
    }
}
