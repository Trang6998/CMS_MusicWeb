using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public interface IService<T>
    {
        IQueryable<T> Paging(
            IQueryable<T> query,
            int page,
            int rowsPerPage);
    }
}
