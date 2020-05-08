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
    [RoutePrefix("api/radio")]
    public class RadioController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, 
                                                    [FromUri]string keyworlds = null,
                                                    [FromUri]DateTime? ngayDangTu = null,
                                                    [FromUri]DateTime? ngayDangDen = null,
                                                    [FromUri]int? nguoiDangID = null)
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
                if (ngayDangTu.HasValue)
                    results = results.Where(x => x.NgayDang >= ngayDangTu);
                if (ngayDangDen.HasValue)
                    results = results.Where(x => x.NgayDang <= ngayDangDen);
                if (nguoiDangID.HasValue)
                    results = results.Where(x => x.NhanVienID == nguoiDangID);
                results = results.OrderByDescending(o => o.NgayDang);

                return Ok((await GetPaginatedResponseAsync(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("{radioID:int}")]
        public async Task<IHttpActionResult> Get(int radioID)
        {
            using (var db = new ApplicationDbContext())
            {
                var radio = await db.Radio
                    .SingleOrDefaultAsync(o => o.RadioID == radioID);

                if (radio == null)
                    return NotFound();
                string fileKey = radio.LinkFile.Split('.')[0];
                var file = db.FileUpload.FirstOrDefault(x => x.FileKey == fileKey);

                var res = new
                {
                    radio.AnhDaiDien,
                    radio.RadioID,
                    radio.TrangThai,
                    radio.LinkFile,
                    radio.NgayDang,
                    radio.LuotThich,
                    radio.LuotXem,
                    radio.NhanVien,
                    radio.TieuDe,
                    radio.NhanVienID,
                    radio.NoiDung,
                    radio.SoThuTu,
                    TenFile = file != null ? file.FileName + "." + file.FileType : ""
                };
                return Ok(res);
            }
        }

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> Insert([FromBody]Radio radio)
        {
            if (radio.RadioID != 0) return BadRequest("Invalid RadioID");

            using (var db = new ApplicationDbContext())
            {
                NhanVien nhanVien = GetNhanVien();

                using (var transaction = db.Database.BeginTransaction())
                {
                    radio.LuotXem = 0;
                    radio.NhanVienID = nhanVien.NhanVienID;
                    radio.NgayDang = DateTime.Now;
                    radio.SoThuTu = db.Radio.Count() + 1;

                    db.Radio.Add(radio);

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
            }

            return Ok(radio);
        }

        [AuthorizeUser, HttpPut, Route("{radioID:int}")]
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

        [AuthorizeUser, HttpDelete, Route("{radioID:int}")]
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
