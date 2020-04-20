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
    [RoutePrefix("api/suKien")]
    public class EventController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Event> results = db.Event;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(x => x.TinhThanh).Include(x => x.QuanHuyen).Include(x => x.XaPhuong);
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TieuDe.Contains(keyworlds));

                results = results.OrderBy(o => o.EventID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{suKienID:int}")]
        public async Task<IHttpActionResult> Get(int suKienID)
        {
            using (var db = new ApplicationDbContext())
            {
                var suKien = await db.Event
                    .SingleOrDefaultAsync(o => o.EventID == suKienID);

                if (suKien == null)
                    return NotFound();

                return Ok(suKien);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]Event suKien)
        {
            if (suKien.EventID != 0) return BadRequest("Invalid EventID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Event.Add(suKien);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(suKien);
        }

        [HttpPut, Route("{suKienID:int}")]
        public async Task<IHttpActionResult> Update(int suKienID, [FromBody]Event suKien)
        {
            if (suKien.EventID != suKienID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(suKien).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.Event.Count(o => o.EventID == suKienID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(suKien);
            }
        }

        [HttpDelete, Route("{suKienID:int}")]
        public async Task<IHttpActionResult> Delete(int suKienID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var suKien = await db.Event.SingleOrDefaultAsync(o => o.EventID == suKienID);

                    if (suKien == null)
                        return NotFound();

                    db.Entry(suKien).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
