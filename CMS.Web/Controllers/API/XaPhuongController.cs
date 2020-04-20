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
    [RoutePrefix("api/xaPhuong")]
    public class XaPhuongController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null, [FromUri]int? quanHuyenID = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<XaPhuong> results = db.XaPhuong;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenXaPhuong.Contains(keyworlds));
                if (quanHuyenID.HasValue)
                    results = results.Where(x => x.QuanHuyenId == quanHuyenID);
                results = results.OrderBy(o => o.Id);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{xaPhuongID:int}")]
        public async Task<IHttpActionResult> Get(int xaPhuongID)
        {
            using (var db = new ApplicationDbContext())
            {
                var xaPhuong = await db.XaPhuong
                    .SingleOrDefaultAsync(o => o.Id == xaPhuongID);

                if (xaPhuong == null)
                    return NotFound();

                return Ok(xaPhuong);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]XaPhuong xaPhuong)
        {
            if (xaPhuong.Id != 0) return BadRequest("Invalid Id");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.XaPhuong.Add(xaPhuong);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(xaPhuong);
        }

        [HttpPut, Route("{xaPhuongID:int}")]
        public async Task<IHttpActionResult> Update(int xaPhuongID, [FromBody]XaPhuong xaPhuong)
        {
            if (xaPhuong.Id != xaPhuongID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(xaPhuong).State = EntityState.Modified;

                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.XaPhuong.Count(o => o.Id == xaPhuongID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(xaPhuong);
            }
        }

        [HttpDelete, Route("{xaPhuongID:int}")]
        public async Task<IHttpActionResult> Delete(int xaPhuongID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var xaPhuong = await db.XaPhuong.SingleOrDefaultAsync(o => o.Id == xaPhuongID);

                    if (xaPhuong == null)
                        return NotFound();

                    db.Entry(xaPhuong).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
