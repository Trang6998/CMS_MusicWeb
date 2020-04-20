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
    [RoutePrefix("api/radio")]
    public class RadioController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Radio> results = db.Radio;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(x => x.NhanVien);
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TieuDe.Contains(keyworlds));

                results = results.OrderBy(o => o.RadioID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{radioID:int}")]
        public async Task<IHttpActionResult> Get(int radioID)
        {
            using (var db = new ApplicationDbContext())
            {
                var radio = await db.Radio
                    .SingleOrDefaultAsync(o => o.RadioID == radioID);

                if (radio == null)
                    return NotFound();

                return Ok(radio);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]Radio radio)
        {
            if (radio.RadioID != 0) return BadRequest("Invalid RadioID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    radio.LuotXem = 0;
                    radio.NhanVienID = 1;
                    radio.NgayDang = DateTime.Now;
                    
                    db.Radio.Add(radio);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(radio);
        }

        [HttpPut, Route("{radioID:int}")]
        public async Task<IHttpActionResult> Update(int radioID, [FromBody]Radio radio)
        {
            if (radio.RadioID != radioID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                radio.NhanVienID = 1;
                radio.NgayDang = DateTime.Now;
                db.Entry(radio).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.Radio.Count(o => o.RadioID == radioID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(radio);
            }
        }

        [HttpDelete, Route("{radioID:int}")]
        public async Task<IHttpActionResult> Delete(int radioID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var radio = await db.Radio.SingleOrDefaultAsync(o => o.RadioID == radioID);

                    if (radio == null)
                        return NotFound();

                    db.Entry(radio).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
