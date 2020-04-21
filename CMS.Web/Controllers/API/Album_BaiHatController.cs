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
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null, [FromUri]int? albumID = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Album_BaiHat> results = db.Album_BaiHat;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(x => x.BaiHat);
                }
                if (albumID.HasValue)
                    results = results.Where(x => x.AlbumID == albumID);
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
        [HttpPut, Route("{albumID:int}/{baiHatID:int}")]
        public async Task<IHttpActionResult> Update(int albumID, int baiHatID, [FromBody]Album_BaiHat album_BaiHat)
        {
            if (album_BaiHat.BaiHatID != baiHatID || album_BaiHat.AlbumID != albumID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(album_BaiHat).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.Album_BaiHat.Count(o => o.BaiHatID == baiHatID && o.AlbumID == albumID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(album_BaiHat);
            }
        }
    }
}
