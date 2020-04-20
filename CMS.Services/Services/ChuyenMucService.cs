using CMS.Services.Interfaces;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class ChuyenMucService : ServiceBase<ChuyenMuc>, IChuyenMucService
    {
        public IQueryable<ChuyenMuc> GetChuyenMuc(string keywords)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.ChuyenMuc.AsQueryable();
                if (!string.IsNullOrWhiteSpace(keywords))
                    query = query.Where(x => x.TenChuyenMuc.Contains(keywords));
                return query;
            }
        }
        
        public IQueryable<ChuyenMuc> GetChuyenMucNoiBat()
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.ChuyenMuc.Include(x => x.ChuyenMuc_BaiViet).Include(x => x.ChuyenMuc_BaiViet.BaiViet);
                
                query = query.Where(x => x.TrangThai == true && x.HienThiTrangChu == true);
                
                return query;
            }
        }
        public ChuyenMuc GetChuyenMucById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var chuyenMuc = db.ChuyenMuc.FirstOrDefault(x => x.ChuyenMucID == id);
                return chuyenMuc;
            }
        }

    }
}
