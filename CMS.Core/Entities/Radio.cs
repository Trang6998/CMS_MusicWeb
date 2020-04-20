using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class Radio : BaseEntity
    {
        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string AnhDaiDien { get; set; }

        public string LinkFile { get; set; }

        public DateTime? NgayDang { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotThich { get; set; }

        public int? NhanVienID { get; set; }

        public bool? TrangThai { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
