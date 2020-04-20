namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int EventID { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string AnhDaiDien { get; set; }

        [StringLength(200)]
        public string DiaDiem { get; set; }
        [StringLength(200)]
        public string MoTaNgan { get; set; }

        public DateTime ThoiGianTu { get; set; }

        public DateTime ThoiGianDen { get; set; }

        public int? NhanVienID { get; set; }

        public int? XaPhuongID { get; set; }

        public int? QuanHuyenID { get; set; }

        public int? TinhThanhID { get; set; }

        [ForeignKey("NhanVienID")]
        public virtual NhanVien NhanVien { get; set; }
        [ForeignKey("XaPhuongID")]
        public virtual XaPhuong XaPhuong { get; set; }
        [ForeignKey("QuanHuyenID")]
        public virtual QuanHuyen QuanHuyen { get; set; }
        [ForeignKey("TinhThanhID")]
        public virtual TinhThanh TinhThanh { get; set; }
    }
}
