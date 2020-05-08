using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using HVITCore.Controllers;
using System.Data.Entity.Infrastructure;
using CMS.Models;
using System.Collections.Generic;
using HVIT.Security;

namespace CMS.Controllers
{
    public class ChangePassDto {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
    [RoutePrefix("api/users")]
    public class UserController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> SearchAsync([FromUri]Pagination pagination, [FromUri]string q = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<User> results = db.User.Include(x => x.NhanVien).Include(x => x.NhanVien.LoaiNhanVien);

                if (!string.IsNullOrWhiteSpace(q))
                    results = results.Where(o => o.UserName.Contains(q) || o.Email.Contains(q));

                results = results.OrderBy(o => o.UserId);

                var res = await GetPaginatedResponseAsync(results, pagination);
                return Ok(res);
            }
        }

        [AuthorizeUser, HttpGet, Route("{userId:int}")]
        public async Task<IHttpActionResult> GetAsync(int userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var users = await db.User.Include(x => x.NhanVien)
                    .SingleOrDefaultAsync(o => o.UserId == userId);

                if (users == null)
                    return NotFound();

                return Ok(users);
            }
        }

        [AuthorizeUser, HttpPost, Route("")]
        public async Task<IHttpActionResult> InsertAsync([FromBody]User users)
        {
            if (users.UserId != 0)
                return BadRequest("Invalid UserId");

            using (var db = new ApplicationDbContext())
            {

                User userExits = db.User.SingleOrDefault(x => x.UserName == users.UserName);
                if (userExits == null)
                {
                    users.PasswordSalt = AuthenticationHelper.RamdomString(5);
                    if (string.IsNullOrEmpty(users.Password))
                        users.Password = "123456";
                    string passwordHash = AuthenticationHelper.GetMd5Hash(users.PasswordSalt + users.Password);
                    users.Password = passwordHash;
                    users.CreatedTime = DateTime.Now;
                    db.User.Add(users);
                    await db.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Tài khoản đã tồn tại");
                }
            }

            return Ok(users);
        }

        [AuthorizeUser, HttpPut, Route("{userId:int}")]
        public async Task<IHttpActionResult> UpdateAsync(int userId, [FromBody]User users)
        {
            if (users.UserId != userId)
                return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = new ApplicationDbContext())
            {
                User userExits = db.User.Find(userId);
                NhanVien nhanVienExits = users.NhanVien;

                if (userExits.UserName != users.UserName)
                {
                    return BadRequest("Không được thay đổi tài khoản.");
                }

                if (!string.IsNullOrEmpty(users.Password))
                {
                    userExits.PasswordSalt = AuthenticationHelper.RamdomString(5);
                    string passwordHash = AuthenticationHelper.GetMd5Hash(userExits.PasswordSalt + users.Password);
                    userExits.Password = passwordHash;
                }

                userExits.Email = users.Email;
                userExits.Active = users.Active;
                db.Entry(userExits).State = EntityState.Modified;
                db.Entry(nhanVienExits).State = EntityState.Modified;
                var dsAccessTokens = db.AccessToken.Where(x => x.UserName == userExits.UserName);
                db.AccessToken.RemoveRange(dsAccessTokens);
                await db.SaveChangesAsync();

                return Ok(users);
            }
        }


        [AuthorizeUser, HttpPut, Route("{userId:int}/changepass")]
        public async Task<IHttpActionResult> UpdatePasswordAsync(int userId, [FromBody]ChangePassDto changePass)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = new ApplicationDbContext())
            {
                User userExits = db.User.SingleOrDefault(x => x.UserName == User.Identity.Name);
                if (userExits == null)
                {
                    return BadRequest("Tài khoản không tồn tại.");
                }
                string passwordHashOld = AuthenticationHelper.GetMd5Hash(userExits.PasswordSalt + changePass.Password);
                if (passwordHashOld != userExits.Password)
                {
                    return BadRequest("Mật khẩu hiện tại không đúng.");
                }
                if (!string.IsNullOrEmpty(changePass.Password))
                {
                    userExits.PasswordSalt = AuthenticationHelper.RamdomString(5);
                    string passwordHash = AuthenticationHelper.GetMd5Hash(userExits.PasswordSalt + changePass.NewPassword);
                    userExits.Password = passwordHash;
                }
                db.Entry(userExits).State = EntityState.Modified;

                var dsAccessTokens = db.AccessToken.Where(x => x.UserName == userExits.UserName);
                db.AccessToken.RemoveRange(dsAccessTokens);
                await db.SaveChangesAsync();

                return Ok(userExits);
            }
        }

        [AuthorizeUser, HttpDelete, Route("{userId:int}")]
        public async Task<IHttpActionResult> DeleteAsync(int userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var users = await db.User.SingleOrDefaultAsync(o => o.UserId == userId);

                if (users == null)
                    return NotFound();

                db.Entry(users).State = EntityState.Deleted;

                await db.SaveChangesAsync();

                return Ok();
            }
        }

    }
}
