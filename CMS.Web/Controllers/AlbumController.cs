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
                ViewBag.DanhSachAlbum = db.Album.Where(x => x.TrangThai == true).ToList();
            }
            return View();
        }
        public ActionResult ChiTietAlbum(int id)
        {
            return View();
        }
    }
}
