using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using HVITCore.Controllers;
using System.Data.Entity.Infrastructure;
using CMS.Models;
using HVIT.Security;

namespace CMS.Controllers
{
    [RoutePrefix("api/chuyenMuc_BaiViet")]
    public class ChuyenMuc_BaiVietController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<ChuyenMuc_BaiViet> results = db.ChuyenMuc_BaiViet;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                }

                results = results.OrderBy(x => x.BaiVietID);
                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]ChuyenMuc_BaiViet chuyenMuc_BaiViet)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.ChuyenMuc_BaiViet.Add(chuyenMuc_BaiViet);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(chuyenMuc_BaiViet);
        }

    }
}
