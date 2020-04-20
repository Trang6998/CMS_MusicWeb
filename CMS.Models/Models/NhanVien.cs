namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int NhanVienID { get; set; }

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

        [InverseProperty("NhanVien")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }

        [InverseProperty("NhanVien")]
        public virtual ICollection<Event> Events { get; set; }

        [ForeignKey("LoaiNhanVienID")]
        public virtual LoaiNhanVien LoaiNhanVien { get; set; }

        [InverseProperty("NhanVien")]
        public virtual ICollection<Radio> Radios { get; set; }

        [InverseProperty("NhanVien")]
        public virtual ICollection<User> Users { get; set; }
    }
}
