using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class SuKienController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.Event.Include(x => x.TinhThanh).Include(x => x.QuanHuyen).Include(x => x.XaPhuong).OrderByDescending(x => x.ThoiGianTu).ToList();
                ViewBag.DanhSachSuKien = query;
                ViewBag.SuKienMoiNhat = query.FirstOrDefault();
            }
            return View();
        }
        public ActionResult ChiTietSuKien(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.Event.Include(x => x.NhanVien).Include(x => x.TinhThanh).Include(x => x.QuanHuyen).Include(x => x.XaPhuong);
                Event suKien = query.FirstOrDefault(x => x.EventID == id);

                db.SaveChanges();

                ViewBag.ChiTietSuKien = suKien;

                ViewBag.SuKienGanDay = db.Event.OrderByDescending(x => x.ThoiGianTu).Take(3).ToList();
            }
            return View();
        }
        [HttpPost]
        public ActionResult TimKiem()
        {
            string tenSuKien = "", diaChi = "";
            tenSuKien = Request.Form["keyworld"];
            diaChi = (Request.Form["diachi"]).ToString().ToLower();
            using (var db = new ApplicationDbContext())
            {
                var query = db.Event.Include(x => x.TinhThanh)
                                    .Include(x => x.QuanHuyen)
                                    .Include(x => x.XaPhuong)
                                    .ToList();
                if (!string.IsNullOrWhiteSpace(tenSuKien))
                    query = query.Where(x => x.TieuDe.Contains(tenSuKien)).ToList();
                if (!string.IsNullOrWhiteSpace(diaChi))
                    query = query.Where(x => x.DiaDiem.ToLower().Contains(diaChi)
                                          || x.QuanHuyen.TenQuanHuyen.ToLower().Contains(diaChi)
                                          || x.XaPhuong.TenXaPhuong.ToLower().Contains(diaChi)
                                          || x.TinhThanh.TenTinhThanh.ToLower().Contains(diaChi)).ToList();
            }
            return View();
        }
    }
}
