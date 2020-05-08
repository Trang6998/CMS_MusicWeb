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
    [RoutePrefix("api/baiViet")]
    public class BaiVietController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, 
                                                    [FromUri]string keyworlds = null, 
                                                    [FromUri]int? chuyenMucID = null,
                                                    [FromUri]DateTime? ngayDangTu = null,
                                                    [FromUri]DateTime? ngayDangDen = null,
                                                    [FromUri]int? nguoiDangID = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<BaiViet> results = db.BaiViet;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.NhanVien)
                                     .Include(o => o.ChuyenMuc_BaiViet)
                                     .Include(o => o.ChuyenMuc_BaiViet.Select(x => x.ChuyenMuc));
                }

                if (!string.IsNullOrWhiteSpace(keyworlds))
                    results = results.Where(x => x.TieuDe.Contains(keyworlds));
                if (chuyenMucID.HasValue)
                    results = results.Where(x => x.ChuyenMuc_BaiViet.Any(y => y.ChuyenMucID == chuyenMucID));
                if (ngayDangTu.HasValue)
                    results = results.Where(x => x.NgayDang >= ngayDangTu);
                if (ngayDangDen.HasValue)
                    results = results.Where(x => x.NgayDang <= ngayDangDen);
                if (nguoiDangID.HasValue)
                    results = results.Where(x => x.NhanVienID == nguoiDangID);
                results = results.OrderByDescending(o => o.NgayDang);

                var res = results.Select(x => new
                {
                    x.BaiVietID,
                    x.TieuDe,
                    x.NgayDang,
                    x.NhanVienID,
                    x.AnhDaiDien,
                    x.LuotThich,
                    x.MoTaNgan,
                    x.NoiDung,
                    x.NhanVien.HoTen,
                    x.LuotXem,
                    x.TrangThai,
                    ChuyenMuc = x.ChuyenMuc_BaiViet.Select(y => new { 
                        y.ChuyenMuc.TenChuyenMuc
                    })
                });

                return Ok((await GetPaginatedResponseAsync(res, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("{baiVietID:int}")]
        public async Task<IHttpActionResult> Get(int baiVietID)
        {
            using (var db = new ApplicationDbContext())
            {
                var baiViet = await db.BaiViet.Include(x=>x.ChuyenMuc_BaiViet)
                    .SingleOrDefaultAsync(o => o.BaiVietID == baiVietID);

                if (baiViet == null)
                    return NotFound();

                return Ok(baiViet);
            }
        }

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]BaiViet baiViet)
        {
            if (baiViet.BaiVietID != 0) return BadRequest("Invalid BaiVietID");

            using (var db = new ApplicationDbContext())
            {
                NhanVien nhanVien = GetNhanVien();

                using (var transaction = db.Database.BeginTransaction())
                {
                    baiViet.NgayDang = DateTime.Now;
                    baiViet.LuotXem = 0;
                    baiViet.LuotThich = 0;
                    baiViet.NhanVienID = nhanVien.NhanVienID;

                    var lstChuyenMucBaiViet = baiViet.ChuyenMuc_BaiViet.ToArray();
                    baiViet.ChuyenMuc_BaiViet = null;

                    db.BaiViet.Add(baiViet);

                    for (int i = 0; i < lstChuyenMucBaiViet.Length; i++)
                    {
                        lstChuyenMucBaiViet[i].BaiVietID = baiViet.BaiVietID;
                    }
                    db.ChuyenMuc_BaiViet.AddRange(lstChuyenMucBaiViet);

                    await db.SaveChangesAsync();

                    transaction.Commit();
                }
            }

            return Ok(baiViet);
        }

        [AuthorizeUser, HttpPut, Route("{baiVietID:int}")]
        public async Task<IHttpActionResult> Update(int baiVietID, [FromBody]BaiViet baiViet)
        {
            if (baiViet.BaiVietID != baiVietID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                if (baiViet.ChuyenMuc_BaiViet != null)
                {
                    var lstChuyenMucBaiVietTraVe = baiViet.ChuyenMuc_BaiViet;
                    var lstChuyenMucBaiVietBanDau = db.ChuyenMuc_BaiViet.Where(x => x.BaiVietID == baiViet.BaiVietID).ToList();
                    var lstChuyenMucBaiVietCanThem = lstChuyenMucBaiVietTraVe
                        .Where(x => !lstChuyenMucBaiVietBanDau.Select(y => y.ChuyenMucID).Contains(x.ChuyenMucID));
                    var lstChuyenMucBaiVietCanXoa = lstChuyenMucBaiVietBanDau
                        .Where(x => !lstChuyenMucBaiVietTraVe.Select(y => y.ChuyenMucID).Contains(x.ChuyenMucID));

                    db.ChuyenMuc_BaiViet.RemoveRange(lstChuyenMucBaiVietCanXoa);
                    db.ChuyenMuc_BaiViet.AddRange(lstChuyenMucBaiVietCanThem);
                    baiViet.ChuyenMuc_BaiViet = null;
                }

                db.Entry(baiViet).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.BaiViet.Count(o => o.BaiVietID == baiVietID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(baiViet);
            }
        }

        [AuthorizeUser, HttpDelete, Route("{baiVietID:int}")]
        public async Task<IHttpActionResult> Delete(int baiVietID)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var baiViet = await db.BaiViet.SingleOrDefaultAsync(o => o.BaiVietID == baiVietID);
                    var chuyenMuc_baiViet = db.ChuyenMuc_BaiViet.Where(o => o.BaiVietID == baiVietID);

                    if (baiViet == null)
                        return NotFound();

                    db.ChuyenMuc_BaiViet.RemoveRange(chuyenMuc_baiViet);
                    db.Entry(baiViet).State = EntityState.Deleted;
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return Ok();
                }
            }
        }

    }
}
