using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class QuanHuyen : BaseEntity
    {

        [StringLength(100)]
        public string TenQuanHuyen { get; set; }

        public int TinhThanhId { get; set; }

        public virtual TinhThanh TinhThanh { get; set; }

        public virtual IEnumerable<XaPhuong> XaPhuongs { get; set; }
    }
}
