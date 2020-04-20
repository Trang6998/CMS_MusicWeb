using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class RadioController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.DanhSachRadio = db.Radio.Where(x => x.TrangThai == true).OrderByDescending(x => x.NgayDang).ToList();
            }
            return View();
        }
        public ActionResult ChiTietRadio(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.Radio.Include(x => x.NhanVien);
                Radio radio = query.FirstOrDefault(x => x.RadioID == id);

                radio.LuotXem++;

                db.SaveChanges();

                ViewBag.ChiTietRadio = radio;
            }
            return View();
        }
    }
}
