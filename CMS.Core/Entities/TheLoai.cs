using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class TheLoai : BaseEntity
    {

        [StringLength(50)]
        public string TenTheLoai { get; set; }

        [StringLength(500)]
        public string GioiThieu { get; set; }

        public virtual IEnumerable<BaiHat> BaiHats { get; set; }
    }
}
