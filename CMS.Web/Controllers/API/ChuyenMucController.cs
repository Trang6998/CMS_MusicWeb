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
    [RoutePrefix("api/chuyenMuc")]
    public class ChuyenMucController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<ChuyenMuc> results = db.ChuyenMuc;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.ChuyenMuc_BaiViet);
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenChuyenMuc.Contains(keyworlds));

                results = results.OrderBy(o => o.ChuyenMucID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{chuyenMucID:int}")]
        public async Task<IHttpActionResult> Get(int chuyenMucID)
        {
            using (var db = new ApplicationDbContext())
            {
                var chuyenMuc = await db.ChuyenMuc
                    .SingleOrDefaultAsync(o => o.ChuyenMucID == chuyenMucID);

                if (chuyenMuc == null)
                    return NotFound();

                return Ok(chuyenMuc);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]ChuyenMuc chuyenMuc)
        {
            if (chuyenMuc.ChuyenMucID != 0) return BadRequest("Invalid ChuyenMucID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.ChuyenMuc.Add(chuyenMuc);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(chuyenMuc);
        }

        [HttpPut, Route("{chuyenMucID:int}")]
        public async Task<IHttpActionResult> Update(int chuyenMucID, [FromBody]ChuyenMuc chuyenMuc)
        {
            if (chuyenMuc.ChuyenMucID != chuyenMucID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(chuyenMuc).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.ChuyenMuc.Count(o => o.ChuyenMucID == chuyenMucID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(chuyenMuc);
            }
        }

        [HttpDelete, Route("{chuyenMucID:int}")]
        public async Task<IHttpActionResult> Delete(int chuyenMucID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var chuyenMuc = await db.ChuyenMuc.SingleOrDefaultAsync(o => o.ChuyenMucID == chuyenMucID);

                    if (chuyenMuc == null)
                        return NotFound();

                    db.Entry(chuyenMuc).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
