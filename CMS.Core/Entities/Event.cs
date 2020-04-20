using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class Event : BaseEntity
    {
        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string AnhDaiDien { get; set; }

        [StringLength(200)]
        public string DiaDiem { get; set; }

        public DateTime? ThoiGianTu { get; set; }

        public DateTime? ThoiGianDen { get; set; }

        public int? NhanVienID { get; set; }

        public int? XaPhuongID { get; set; }

        public int? QuanHuyenID { get; set; }

        public int? TinhThanhID { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
