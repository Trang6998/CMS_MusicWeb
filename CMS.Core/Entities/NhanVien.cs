using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class NhanVien : BaseEntity
    {

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(200)]
        public string Facebook { get; set; }

        [StringLength(200)]
        public string Zalo { get; set; }

        [StringLength(200)]
        public string Instagram { get; set; }

        public string AnhDaiDien { get; set; }

        public string GioiThieu { get; set; }

        public int? LoaiNhanVienID { get; set; }

        public int? XaPhuongID { get; set; }

        public int? QuanHuyenID { get; set; }

        public int? TinhThanhID { get; set; }

        public virtual IEnumerable<BaiViet> BaiViets { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }

        public virtual LoaiNhanVien LoaiNhanVien { get; set; }

        public virtual IEnumerable<Radio> Radios { get; set; }
    }
}
