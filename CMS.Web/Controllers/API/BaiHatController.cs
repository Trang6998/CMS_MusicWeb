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
    [RoutePrefix("api/baiHat")]
    public class BaiHatController : BaseApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, [FromUri]string keyworlds = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<BaiHat> results = db.BaiHat;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.TheLoai).Include(x => x.CaSy_BaiHat.Select(y=> y.CaSy))
                                                             .Include(x => x.Album_BaiHat.Select(y => y.Album));
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TenBaiHat.Contains(keyworlds));

                results = results.OrderBy(o => o.BaiHatID);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [HttpGet, Route("{baiHatID:int}")]
        public async Task<IHttpActionResult> Get(int baiHatID)
        {
            using (var db = new ApplicationDbContext())
            {
                var baiHat = await db.BaiHat.Include(x => x.CaSy_BaiHat)
                    .SingleOrDefaultAsync(o => o.BaiHatID == baiHatID);

                if (baiHat == null)
                    return NotFound();

                return Ok(baiHat);
            }
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]BaiHat baiHat)
        {
            if (baiHat.BaiHatID != 0) return BadRequest("Invalid BaiHatID");

            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    baiHat.NgayDang = DateTime.Now;

                    var lstCaSyBaiHat = baiHat.CaSy_BaiHat.ToArray();
                    baiHat.CaSy_BaiHat = null;

                    db.BaiHat.Add(baiHat);

                    for (int i = 0; i < lstCaSyBaiHat.Length; i++)
                    {
                        lstCaSyBaiHat[i].BaiHatID = baiHat.BaiHatID;
                    }
                    db.CaSy_BaiHat.AddRange(lstCaSyBaiHat);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(baiHat);
        }

        [HttpPut, Route("{baiHatID:int}")]
        public async Task<IHttpActionResult> Update(int baiHatID, [FromBody]BaiHat baiHat)
        {
            if (baiHat.BaiHatID != baiHatID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                if (baiHat.CaSy_BaiHat != null)
                {
                    var lstChuyenMucBaiHatTraVe = baiHat.CaSy_BaiHat;
                    var lstChuyenMucBaiHatBanDau = db.CaSy_BaiHat.Where(x => x.BaiHatID == baiHat.BaiHatID).ToList();
                    var lstChuyenMucBaiHatCanThem = lstChuyenMucBaiHatTraVe
                        .Where(x => !lstChuyenMucBaiHatBanDau.Select(y => y.CaSyID).Contains(x.CaSyID));
                    var lstChuyenMucBaiHatCanXoa = lstChuyenMucBaiHatBanDau
                        .Where(x => !lstChuyenMucBaiHatTraVe.Select(y => y.CaSyID).Contains(x.CaSyID));

                    db.CaSy_BaiHat.RemoveRange(lstChuyenMucBaiHatCanXoa);
                    db.CaSy_BaiHat.AddRange(lstChuyenMucBaiHatCanThem);
                    baiHat.CaSy_BaiHat = null;
                }
                db.Entry(baiHat).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.BaiHat.Count(o => o.BaiHatID == baiHatID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(baiHat);
            }
        }

        [HttpDelete, Route("{baiHatID:int}")]
        public async Task<IHttpActionResult> Delete(int baiHatID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var baiHat = await db.BaiHat.SingleOrDefaultAsync(o => o.BaiHatID == baiHatID);

                    if (baiHat == null)
                        return NotFound();

                    db.Entry(baiHat).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
