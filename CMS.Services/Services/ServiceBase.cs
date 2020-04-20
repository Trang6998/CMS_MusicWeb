using CMS.Models;
using System.Linq;

namespace CMS.Services
{
    public class ServiceBase<T>: IService<T> where T: class
    {
        protected ApplicationDbContext DbContext;
        public ServiceBase()
        {
            DbContext = new ApplicationDbContext();
        }

        ~ServiceBase()
        {
            DbContext.Dispose();
        }

        public IQueryable<T> Paging(IQueryable<T> query, int page = 1, int rowsPerPage = 20)
        {
            return query.Skip(rowsPerPage * page)
                    .Take(rowsPerPage);
        }
    }
}