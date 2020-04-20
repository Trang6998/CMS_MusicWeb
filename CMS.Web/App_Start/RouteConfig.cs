using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Trang",
                url: "pages/{pageTitle}-{id}",
                defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "DanhMucSanPham",
            //    url: "danh-muc-san-pham/{tenDanhMucSanPham}-{id}",
            //    defaults: new { controller = "DanhMucSanPham", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "SanPham",
                url: "san-pham",
                defaults: new { controller = "SanPham", action = "Index" }
            );
            routes.MapRoute(
                name: "DanhMucSanPham",
                url: "san-pham/{tenDanhMucSanPham}-{id}",
                defaults: new { controller = "SanPham", action = "DanhMucSanPham", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "ChiTietSanPham",
            //    url: "san-pham/{tenSanPham}-{id}",
            //    defaults: new { controller = "SanPham", action = "ChiTietSanPham", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "ChuyenMucTinTuc",
                url: "chuyen-muc-bai-viet/{tenChuyenMucTinTuc}-{id}",
                defaults: new { controller = "TinTuc", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ChiTietTinTuc",
                url: "bai-viet/{tenTinTuc}-{id}",
                defaults: new { controller = "TinTuc", action = "ChiTiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LienHe",
                url: "lien-he",
                defaults: new { controller = "LienHe", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AlbumAnh",
                url: "du-an",
                defaults: new { controller = "AlbumAnh", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AlbumAnhChiTiet",
                url: "du-an/{tieude}-{id}",
                defaults: new { controller = "AlbumAnh", action = "ChiTiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GiaiPhap",
                url: "giai-phap",
                defaults: new { controller = "GiaiPhap", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GiaiPhapChiTiet",
                url: "giai-phap/{tieude}-{id}",
                defaults: new { controller = "GiaiPhap", action = "ChiTiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Videos",
                url: "videos",
                defaults: new { controller = "Videos", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "VideosChiTiet",
                url: "videos/{tieude}-{id}",
                defaults: new { controller = "Videos", action = "ChiTiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BaoGia",
                url: "bao-gia",
                defaults: new { controller = "BaoGia", action = "Index"}
            );
            routes.MapRoute(
                name: "ThuVien",
                url: "thu-vien",
                defaults: new { controller = "ThuVien", action = "Index" }
            );
            routes.MapRoute(
                name: "ThuVienCatalogs",
                url: "thu-vien/catalogs",
                defaults: new { controller = "ThuVien", action = "Catalogs" }
            );
            routes.MapRoute(
                name: "ThuVienTaiLieuKyThuat",
                url: "thu-vien/tai-lieu-ky-thuat",
                defaults: new { controller = "ThuVien", action = "TaiLieuKyThuat" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
