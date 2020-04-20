using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class ChuyenMuc : BaseEntity
    { 

        [StringLength(50)]
        public string TenChuyenMuc { get; set; }

        public bool? TrangThai { get; set; }

        public bool? HienThiTrangChu { get; set; }

        public virtual IEnumerable<ChuyenMuc_BaiViet> ChuyenMuc_BaiViet { get; set; }
    }
}
