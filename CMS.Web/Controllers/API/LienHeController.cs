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
    [RoutePrefix("api/lienHe")]
    public class LienHeController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<LienHe> results = db.LienHe;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.HoTen.Contains(keyworlds) || x.SoDienThoai.Contains(keyworlds) || x.Email.Contains(keyworlds));

                results = results.OrderBy(o => o.LienHeID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("{lienHeID:int}")]
        public async Task<IHttpActionResult> Get(int lienHeID)
        {
            using (var db = new ApplicationDbContext())
            {
                var lienHe = await db.LienHe
                    .SingleOrDefaultAsync(o => o.LienHeID == lienHeID);

                if (lienHe == null)
                    return NotFound();

                return Ok(lienHe);
            }
        }

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]LienHe lienHe)
        {
            if (lienHe.LienHeID != 0) return BadRequest("Invalid LienHeID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.LienHe.Add(lienHe);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(lienHe);
        }

        [AuthorizeUser, HttpPut, Route("{lienHeID:int}")]
        public async Task<IHttpActionResult> Update(int lienHeID, [FromBody]LienHe lienHe)
        {
            if (lienHe.LienHeID != lienHeID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(lienHe).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.LienHe.Count(o => o.LienHeID == lienHeID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(lienHe);
            }
        }

        [AuthorizeUser, HttpDelete, Route("{lienHeID:int}")]
        public async Task<IHttpActionResult> Delete(int lienHeID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var lienHe = await db.LienHe.SingleOrDefaultAsync(o => o.LienHeID == lienHeID);

                    if (lienHe == null)
                        return NotFound();

                    db.Entry(lienHe).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
