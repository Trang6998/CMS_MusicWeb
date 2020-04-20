using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class BaiViet : BaseEntity
    {

        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string AnhDaiDien { get; set; }

        public bool? TrangThai { get; set; }

        public DateTime? NgayDang { get; set; }

        public int? NhanVienID { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotThich { get; set; }

        public bool? HienThiTrangChu { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual IEnumerable<ChuyenMuc_BaiViet> ChuyenMuc_BaiViet { get; set; }
    }
}
