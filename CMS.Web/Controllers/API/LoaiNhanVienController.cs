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
    [RoutePrefix("api/loaiNhanVien")]
    public class LoaiNhanVienController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<LoaiNhanVien> results = db.LoaiNhanVien;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {

                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenLoai.Contains(keyworlds));

                results = results.OrderBy(o => o.LoaiNhanVienID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("{loaiNhanVienID:int}")]
        public async Task<IHttpActionResult> Get(int loaiNhanVienID)
        {
            using (var db = new ApplicationDbContext())
            {
                var loaiNhanVien = await db.LoaiNhanVien
                    .SingleOrDefaultAsync(o => o.LoaiNhanVienID == loaiNhanVienID);

                if (loaiNhanVien == null)
                    return NotFound();

                return Ok(loaiNhanVien);
            }
        }

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]LoaiNhanVien loaiNhanVien)
        {
            if (loaiNhanVien.LoaiNhanVienID != 0) return BadRequest("Invalid LoaiNhanVienID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.LoaiNhanVien.Add(loaiNhanVien);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(loaiNhanVien);
        }

        [AuthorizeUser, HttpPut, Route("{loaiNhanVienID:int}")]
        public async Task<IHttpActionResult> Update(int loaiNhanVienID, [FromBody]LoaiNhanVien loaiNhanVien)
        {
            if (loaiNhanVien.LoaiNhanVienID != loaiNhanVienID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(loaiNhanVien).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.LoaiNhanVien.Count(o => o.LoaiNhanVienID == loaiNhanVienID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(loaiNhanVien);
            }
        }

        [AuthorizeUser, HttpDelete, Route("{loaiNhanVienID:int}")]
        public async Task<IHttpActionResult> Delete(int loaiNhanVienID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var loaiNhanVien = await db.LoaiNhanVien.SingleOrDefaultAsync(o => o.LoaiNhanVienID == loaiNhanVienID);

                    if (loaiNhanVien == null)
                        return NotFound();

                    db.Entry(loaiNhanVien).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
