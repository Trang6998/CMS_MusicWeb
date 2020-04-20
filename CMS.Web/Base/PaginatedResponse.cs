using System;
using System.Collections.Generic;
using System.Linq;

namespace HVITCore.Controllers
{
    public class PaginatedResponse<T>
    {
        public Pagination Pagination { get; set; }
        public List<T> Data { get; set; }
    }
}