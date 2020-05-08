using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Net;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Security.Claims;
using CMS.Models;

namespace HVITCore.Controllers
{
    public class BaseApiController : ApiController
    {
        private NhanVien nhanVien;

        protected NhanVien GetNhanVien()
        {
            if (nhanVien == null)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    if (this.User != null)
                    {
                        nhanVien = db.NhanVien
                            .Include(x => x.Users)
                            .FirstOrDefault(x => x.Users.Any(y => y.UserName.Equals(this.User.Identity.Name)));
                    }
                }
            }
            return nhanVien;
        }
        protected async Task<PaginatedResponse<T>> GetPaginatedResponseAsync<T>(IQueryable<T> query, Pagination pagination)
        {
            if (pagination == null) pagination = new Pagination();

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pagination.itemsPerPage);

            pagination.page = pagination.page < 1 ? 0 : pagination.page - 1;

            var results = await (pagination.itemsPerPage <= 0
                ? query.ToListAsync()
                : query.Skip(pagination.itemsPerPage * pagination.page)
                    .Take(pagination.itemsPerPage)
                    .ToListAsync());

            var paginationHeader = new Pagination ()
            {
                page = pagination.page,
                itemsPerPage = pagination.itemsPerPage,
                records = results.Count,
                totalItems = totalRecords,
                totalPages = totalPages
            };

            HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return new PaginatedResponse<T>() {
                Pagination = paginationHeader,
                Data = results
            };
        }
    }
}