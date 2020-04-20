using CMS.Services.Interfaces;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class BaiHatService : ServiceBase<BaiHat>, IBaiHatService
    {
        public IQueryable<BaiHat> GetBaiHat(string keywords)
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.BaiHat.AsQueryable();
                if (!string.IsNullOrWhiteSpace(keywords))
                    query = query.Where(x => x.TenBaiHat.Contains(keywords));
                return query;
            }
        }

        public BaiHat GetBaiHatById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var baiHat = db.BaiHat.FirstOrDefault(x => x.BaiHatID == id);
                return baiHat;
            }
        }

    }
}
