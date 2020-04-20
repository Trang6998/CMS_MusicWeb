using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class Album_BaiHat : BaseEntity
    {
        public int AlbumID { get; set; }
        public int BaiHatID { get; set; }

        public int? SoThuTu { get; set; }

        public virtual Album Album { get; set; }

        public virtual BaiHat BaiHat { get; set; }
    }
}
