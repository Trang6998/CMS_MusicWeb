using CMS.Models;
using HVIT.Security;
using HVITCore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CMS.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginForm loginForm)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                User user = db.User.Include(x => x.NhanVien).Include(x => x.NhanVien.LoaiNhanVien).SingleOrDefault(x => x.UserName == loginForm.Username);
                if (user != null)
                {
                    string passwordSalt = user.PasswordSalt;
                    string passwordInput = AuthenticationHelper.GetMd5Hash(passwordSalt + loginForm.Password);
                    string passwordUser = user.Password;

                    if (String.Equals(passwordInput, passwordUser, StringComparison.InvariantCulture) && user.Active == true)
                    {
                        TokenProvider tokenProvider = new TokenProvider();
                        TokenIdentity token = tokenProvider.GenerateToken(user.UserId, loginForm.Username,
                            Request.Headers.UserAgent.ToString(),
                            "", Guid.NewGuid().ToString(),
                            DateTime.Now.Ticks);
                        token.SetAuthenticationType("Custom");
                        token.SetIsAuthenticated(true);
                        db.AccessToken.Add(new AccessToken()
                        {
                            Token = token.Token,
                            EffectiveTime = new DateTime(token.EffectiveTime),
                            ExpiresIn = token.ExpiresTime,
                            IP = token.IP,
                            UserAgent = token.UserAgent,
                            UserName = token.Name
                        });
                        db.SaveChanges();
                        return Ok(
                            new
                            {
                                AccessToken = token,
                                Profile = new
                                {
                                    UserId = user.UserId,
                                    Username = user.UserName,
                                    Email = user.Email,
                                    LoaiTaiKhoanID = user.NhanVien.LoaiNhanVienID,
                                    LoaiTaiKhoan = user.NhanVien.LoaiNhanVien.TenLoai
                                }
                            });
                    }
                }
                return Ok("Login failed!");
            }
        }

        [AuthorizeUser, HttpGet]
        [Route("validate-token")]
        public IHttpActionResult ValidateToken()
        {
            TokenIdentity tokenIdentity = ClaimsPrincipal.Current.Identity as TokenIdentity;
            return Ok();
        }
    }
}
