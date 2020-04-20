using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class CaSy_BaiHat : BaseEntity
    {
        public int CaSyID { get; set; } 
        public int BaiHatID { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        public int? SoThuTu { get; set; }

        public virtual BaiHat BaiHat { get; set; }

        public virtual CaSy CaSy { get; set; }
    }
}
