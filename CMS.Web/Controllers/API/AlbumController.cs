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
    [RoutePrefix("api/album")]
    public class AlbumController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Album> results = db.Album;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.Album_BaiHat);
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenAlbum.Contains(keyworlds));
                results = results.OrderBy(o => o.AlbumID);

                var res = results.Select(x => new
                {
                    x.AlbumID,
                    x.TenAlbum,
                    x.TrangThai,
                    SoBaiHat = x.Album_BaiHat.Count()
                });

                return Ok((await GetPaginatedResponseAsync(res, pagination)));
            }
        }

        [HttpGet, Route("{albumID:int}")]
        public async Task<IHttpActionResult> Get(int albumID)
        {
            using (var db = new ApplicationDbContext())
            {
                var album = await db.Album
                    .SingleOrDefaultAsync(o => o.AlbumID == albumID);

                if (album == null)
                    return NotFound();

                return Ok(album);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]Album album)
        {
            if (album.AlbumID != 0) return BadRequest("Invalid AlbumID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Album.Add(album);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(album);
        }

        [HttpPut, Route("{albumID:int}")]
        public async Task<IHttpActionResult> Update(int albumID, [FromBody]Album album)
        {
            if (album.AlbumID != albumID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(album).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.Album.Count(o => o.AlbumID == albumID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(album);
            }
        }

        [HttpDelete, Route("{albumID:int}")]
        public async Task<IHttpActionResult> Delete(int albumID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var album = await db.Album.SingleOrDefaultAsync(o => o.AlbumID == albumID);

                    if (album == null)
                        return NotFound();

                    db.Entry(album).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
