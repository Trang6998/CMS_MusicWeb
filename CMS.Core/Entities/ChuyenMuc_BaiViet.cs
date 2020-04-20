using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class ChuyenMuc_BaiViet : BaseEntity
    { 
        public int ChuyenMucID { get; set; }
         
        public int BaiVietID { get; set; }

        [StringLength(50)]
        public string TenChuyenMuc { get; set; }

        public virtual BaiViet BaiViet { get; set; }

        public virtual ChuyenMuc ChuyenMuc { get; set; }
    }
}
