using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class CaSy : BaseEntity
    {

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string BietDanh { get; set; }

        public string AnhDaiDien { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string Gmail { get; set; }

        [StringLength(50)]
        public string FaceBook { get; set; }

        [StringLength(50)]
        public string Instagram { get; set; }

        [StringLength(500)]
        public string SoThich { get; set; }

        [StringLength(500)]
        public string TinhCach { get; set; }

        public string TieuSu { get; set; }

        public int? XaPhuongID { get; set; }

        public int? QuanHuyenID { get; set; }

        public int? TinhThanhID { get; set; }

        public virtual IEnumerable<CaSy_BaiHat> CaSy_BaiHat { get; set; }
    }
}
