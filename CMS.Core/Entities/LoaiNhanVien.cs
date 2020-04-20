using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class LoaiNhanVien : BaseEntity
    {

        [StringLength(50)]
        public string TenLoai { get; set; }

        public virtual IEnumerable<NhanVien> NhanViens { get; set; }
    }
}
