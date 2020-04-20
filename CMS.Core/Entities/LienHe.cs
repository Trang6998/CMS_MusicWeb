using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class LienHe : BaseEntity
    {
        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        public string NoiDung { get; set; }

        public bool? HienThi { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
