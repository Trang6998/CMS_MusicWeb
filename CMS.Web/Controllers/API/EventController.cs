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
    [RoutePrefix("api/suKien")]
    public class EventController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, 
                                                    [FromUri]string keyworlds = null,
                                                    [FromUri]DateTime? ngayDangTu = null,
                                                    [FromUri]DateTime? ngayDangDen = null,
                                                    [FromUri]int? tinhThanhID = null,
                                                    [FromUri]int? quanHuyenID = null,
                                                    [FromUri]int? xaPhuongID = null)
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
                if (ngayDangTu.HasValue)
                    results = results.Where(x => x.ThoiGianTu >= ngayDangTu);
                if (ngayDangDen.HasValue)
                    results = results.Where(x => x.ThoiGianDen <= ngayDangDen);
                if (tinhThanhID.HasValue)
                    results = results.Where(x => x.TinhThanhID == tinhThanhID);
                if (quanHuyenID.HasValue)
                    results = results.Where(x => x.QuanHuyenID == quanHuyenID);
                if (xaPhuongID.HasValue)
                    results = results.Where(x => x.XaPhuongID == xaPhuongID);
                results = results.OrderBy(o => o.EventID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("{suKienID:int}")]
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

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]Event suKien)
        {
            if (suKien.EventID != 0) return BadRequest("Invalid EventID");

            using (var db = new ApplicationDbContext())
            {
                NhanVien nhanVien = GetNhanVien();

                using (var transaction = db.Database.BeginTransaction())
                {
                    suKien.NhanVienID = nhanVien.NhanVienID;
                    db.Event.Add(suKien);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(suKien);
        }

        [AuthorizeUser, HttpPut, Route("{suKienID:int}")]
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

        [AuthorizeUser, HttpDelete, Route("{suKienID:int}")]
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
