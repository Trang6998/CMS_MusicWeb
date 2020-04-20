using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class BaiVietController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    ViewBag.DanhSachChuyenMuc = db.ChuyenMuc.Include(x => x.ChuyenMuc_BaiViet.Select(y => y.BaiViet))
                                                  .Where(x => x.TrangThai == true).ToList();
                    ViewBag.DanhSachTinMoi = db.BaiViet.Where(x => x.TrangThai == true)
                                                       .OrderByDescending(x => x.NgayDang)
                                                       .Take(5).ToList();
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ChiTietBaiViet(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.BaiViet.Include(x => x.NhanVien).Include(x => x.ChuyenMuc_BaiViet.Select(y => y.ChuyenMuc));
                BaiViet baiViet = query.FirstOrDefault(x => x.BaiVietID == id);

                baiViet.LuotXem++;

                db.SaveChanges();

                ViewBag.ChiTietBaiViet = baiViet;
                var chuyenMucID = query.FirstOrDefault(x => x.BaiVietID == id).ChuyenMuc_BaiViet.ToArray()[0].ChuyenMucID;
                ViewBag.BaiVietCungChuyenMuc = db.BaiViet.Where(x => x.ChuyenMuc_BaiViet.Any(y => y.ChuyenMucID == chuyenMucID)).Take(3).ToList();
            }
            return View();
        }
    }
}
