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
    [RoutePrefix("api/quanHuyen")]
    public class QuanHuyenController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null, [FromUri]int? tinhThanhID = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<QuanHuyen> results = db.QuanHuyen;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenQuanHuyen.Contains(keyworlds));
                if (tinhThanhID.HasValue)
                    results = results.Where(x => x.TinhThanhId == tinhThanhID);
                results = results.OrderBy(o => o.Id);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{quanHuyenID:int}")]
        public async Task<IHttpActionResult> Get(int quanHuyenID)
        {
            using (var db = new ApplicationDbContext())
            {
                var quanHuyen = await db.QuanHuyen
                    .SingleOrDefaultAsync(o => o.Id == quanHuyenID);

                if (quanHuyen == null)
                    return NotFound();

                return Ok(quanHuyen);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]QuanHuyen quanHuyen)
        {
            if (quanHuyen.Id != 0) return BadRequest("Invalid Id");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.QuanHuyen.Add(quanHuyen);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(quanHuyen);
        }

        [HttpPut, Route("{quanHuyenID:int}")]
        public async Task<IHttpActionResult> Update(int quanHuyenID, [FromBody]QuanHuyen quanHuyen)
        {
            if (quanHuyen.Id != quanHuyenID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(quanHuyen).State = EntityState.Modified;

                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.QuanHuyen.Count(o => o.Id == quanHuyenID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(quanHuyen);
            }
        }

        [HttpDelete, Route("{quanHuyenID:int}")]
        public async Task<IHttpActionResult> Delete(int quanHuyenID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var quanHuyen = await db.QuanHuyen.SingleOrDefaultAsync(o => o.Id == quanHuyenID);

                    if (quanHuyen == null)
                        return NotFound();

                    db.Entry(quanHuyen).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
