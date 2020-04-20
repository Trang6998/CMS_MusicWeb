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
    [RoutePrefix("api/nhanVien")]
    public class NhanVienController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<NhanVien> results = db.NhanVien;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.HoTen.Contains(keyworlds));

                results = results.OrderBy(o => o.NhanVienID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{nhanVienID:int}")]
        public async Task<IHttpActionResult> Get(int nhanVienID)
        {
            using (var db = new ApplicationDbContext())
            {
                var nhanVien = await db.NhanVien
                    .SingleOrDefaultAsync(o => o.NhanVienID == nhanVienID);

                if (nhanVien == null)
                    return NotFound();

                return Ok(nhanVien);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]NhanVien nhanVien)
        {
            if (nhanVien.NhanVienID != 0) return BadRequest("Invalid NhanVienID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.NhanVien.Add(nhanVien);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(nhanVien);
        }

        [HttpPut, Route("{nhanVienID:int}")]
        public async Task<IHttpActionResult> Update(int nhanVienID, [FromBody]NhanVien nhanVien)
        {
            if (nhanVien.NhanVienID != nhanVienID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(nhanVien).State = EntityState.Modified;

                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.NhanVien.Count(o => o.NhanVienID == nhanVienID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(nhanVien);
            }
        }

        [HttpDelete, Route("{nhanVienID:int}")]
        public async Task<IHttpActionResult> Delete(int nhanVienID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var nhanVien = await db.NhanVien.SingleOrDefaultAsync(o => o.NhanVienID == nhanVienID);

                    if (nhanVien == null)
                        return NotFound();

                    db.Entry(nhanVien).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
