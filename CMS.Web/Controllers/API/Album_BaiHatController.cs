using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using HVITCore.Controllers;
using System.Data.Entity.Infrastructure;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/album_BaiHat")]
    public class Album_BaiHatController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Album_BaiHat> results = db.Album_BaiHat;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                }

                results = results.OrderBy(x => x.AlbumID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]Album_BaiHat album_BaiHat)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Album_BaiHat.Add(album_BaiHat);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(album_BaiHat);
        }

    }
}
