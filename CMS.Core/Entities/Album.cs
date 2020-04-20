using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class Album : BaseEntity
    {
        [StringLength(50)]
        public string TenAlbum { get; set; }

        public string AnhDaiDien { get; set; }

        public bool? TrangThai { get; set; }

        public virtual IEnumerable<Album_BaiHat> Album_BaiHat { get; set; }
    }
}
