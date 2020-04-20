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
    [RoutePrefix("api/caSy")]
    public class CaSyController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<CaSy> results = db.CaSy;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.CaSy_BaiHat);
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.HoTen.Contains(keyworlds));

                results = results.OrderBy(o => o.CaSyID);
                var res = results.Select(x => new
                {
                    x.CaSyID,
                    x.HoTen,
                    x.BietDanh,
                    x.FaceBook,
                    x.SoDienThoai,
                    x.NgaySinh,
                    SoBaiHat = x.CaSy_BaiHat.Count()
                });
                return Ok((await GetPaginatedResponseAsync(res, pagination)));
            }
        }

        [HttpGet, Route("{caSyID:int}")]
        public async Task<IHttpActionResult> Get(int caSyID)
        {
            using (var db = new ApplicationDbContext())
            {
                var caSy = await db.CaSy
                    .SingleOrDefaultAsync(o => o.CaSyID == caSyID);

                if (caSy == null)
                    return NotFound();

                return Ok(caSy);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]CaSy caSy)
        {
            if (caSy.CaSyID != 0) return BadRequest("Invalid CaSyID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.CaSy.Add(caSy);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(caSy);
        }

        [HttpPut, Route("{caSyID:int}")]
        public async Task<IHttpActionResult> Update(int caSyID, [FromBody]CaSy caSy)
        {
            if (caSy.CaSyID != caSyID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(caSy).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.CaSy.Count(o => o.CaSyID == caSyID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(caSy);
            }
        }

        [HttpDelete, Route("{caSyID:int}")]
        public async Task<IHttpActionResult> Delete(int caSyID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var caSy = await db.CaSy.SingleOrDefaultAsync(o => o.CaSyID == caSyID);

                    if (caSy == null)
                        return NotFound();

                    db.Entry(caSy).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
