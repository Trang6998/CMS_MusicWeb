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
    [RoutePrefix("api/theLoai")]
    public class TheLoaiController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<TheLoai> results = db.TheLoai;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenTheLoai.Contains(keyworlds));

                results = results.OrderBy(o => o.TheLoaiID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{theLoaiID:int}")]
        public async Task<IHttpActionResult> Get(int theLoaiID)
        {
            using (var db = new ApplicationDbContext())
            {
                var theLoai = await db.TheLoai
                    .SingleOrDefaultAsync(o => o.TheLoaiID == theLoaiID);

                if (theLoai == null)
                    return NotFound();

                return Ok(theLoai);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]TheLoai theLoai)
        {
            if (theLoai.TheLoaiID != 0) return BadRequest("Invalid TheLoaiID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.TheLoai.Add(theLoai);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(theLoai);
        }

        [HttpPut, Route("{theLoaiID:int}")]
        public async Task<IHttpActionResult> Update(int theLoaiID, [FromBody]TheLoai theLoai)
        {
            if (theLoai.TheLoaiID != theLoaiID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(theLoai).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.TheLoai.Count(o => o.TheLoaiID == theLoaiID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(theLoai);
            }
        }

        [HttpDelete, Route("{theLoaiID:int}")]
        public async Task<IHttpActionResult> Delete(int theLoaiID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var theLoai = await db.TheLoai.SingleOrDefaultAsync(o => o.TheLoaiID == theLoaiID);

                    if (theLoai == null)
                        return NotFound();

                    db.Entry(theLoai).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
