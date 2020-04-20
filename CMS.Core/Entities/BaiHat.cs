using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class BaiHat : BaseEntity
    {
        [StringLength(50)]
        public string TenBaiHat { get; set; }

        public string LinkFileNhac { get; set; }

        public string AnhDaiDien { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public DateTime? NgayDang { get; set; }

        public int? TheLoaiID { get; set; }

        public bool? HienThiTrangChu { get; set; }

        public virtual IEnumerable<Album_BaiHat> Album_BaiHat { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        public virtual IEnumerable<CaSy_BaiHat> CaSy_BaiHat { get; set; }
    }
}
