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
    [RoutePrefix("api/tinhThanh")]
    public class TinhThanhController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<TinhThanh> results = db.TinhThanh;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenTinhThanh.Contains(keyworlds));

                results = results.OrderBy(o => o.Id);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{tinhThanhID:int}")]
        public async Task<IHttpActionResult> Get(int tinhThanhID)
        {
            using (var db = new ApplicationDbContext())
            {
                var tinhThanh = await db.TinhThanh
                    .SingleOrDefaultAsync(o => o.Id == tinhThanhID);

                if (tinhThanh == null)
                    return NotFound();

                return Ok(tinhThanh);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]TinhThanh tinhThanh)
        {
            if (tinhThanh.Id != 0) return BadRequest("Invalid Id");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.TinhThanh.Add(tinhThanh);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(tinhThanh);
        }

        [HttpPut, Route("{tinhThanhID:int}")]
        public async Task<IHttpActionResult> Update(int tinhThanhID, [FromBody]TinhThanh tinhThanh)
        {
            if (tinhThanh.Id != tinhThanhID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(tinhThanh).State = EntityState.Modified;

                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.TinhThanh.Count(o => o.Id == tinhThanhID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(tinhThanh);
            }
        }

        [HttpDelete, Route("{tinhThanhID:int}")]
        public async Task<IHttpActionResult> Delete(int tinhThanhID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var tinhThanh = await db.TinhThanh.SingleOrDefaultAsync(o => o.Id == tinhThanhID);

                    if (tinhThanh == null)
                        return NotFound();

                    db.Entry(tinhThanh).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
