using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class TinhThanh : BaseEntity
    {

        [StringLength(200)]
        public string TenTinhThanh { get; set; }

        public virtual IEnumerable<QuanHuyen> QuanHuyens { get; set; }
    }
}
