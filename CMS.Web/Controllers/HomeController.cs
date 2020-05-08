using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                using(var db = new ApplicationDbContext())
                {
                    ViewBag.DanhSachChuyenMuc = db.ChuyenMuc.Include(x => x.ChuyenMuc_BaiViet.Select(y => y.BaiViet))
                                                  .Where(x => x.HienThiTrangChu == true && x.TrangThai == true).Take(2).ToList();
                    ViewBag.DanhSachTinMoi = db.BaiViet.Where(x => x.TrangThai == true)
                                                       .OrderByDescending(x => x.NgayDang)
                                                       .Take(5).ToList();
                    ViewBag.DanhSachNhac = db.BaiHat.Include(x => x.CaSy_BaiHat.Select(y => y.CaSy))
                                                    .OrderByDescending(x => x.NgayDang)
                                                    .Take(5).ToList();
                    ViewBag.DanhSachCaSy = db.CaSy.Include(x => x.CaSy_BaiHat).ToList();
                }
                ViewBag.Title = "Trang chủ";
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ThemLienHe()
        {
            LienHe lienHe = new LienHe();
            lienHe.HoTen = Request.Form["message-name"];
            lienHe.SoDienThoai = Request.Form["message-sdt"];
            lienHe.Email = Request.Form["message-email"];
            lienHe.NoiDung = Request.Form["message"];

            using (var db = new ApplicationDbContext())
            {
                db.LienHe.Add(lienHe);
                db.SaveChanges();
            }
            return View();
        }
    }
}
