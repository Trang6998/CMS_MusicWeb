using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class XaPhuong : BaseEntity
    {
        public int QuanHuyenId { get; set; }

        [StringLength(100)]
        public string TenXaPhuong { get; set; }

        public virtual QuanHuyen QuanHuyen { get; set; }
    }
}
