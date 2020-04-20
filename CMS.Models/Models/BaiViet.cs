namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BaiVietID { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string AnhDaiDien { get; set; }

        public bool? TrangThai { get; set; }

        public DateTime NgayDang { get; set; }

        public int? NhanVienID { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotThich { get; set; }

        public string MoTaNgan { get; set; }

        [ForeignKey("NhanVienID")]
        public virtual NhanVien NhanVien { get; set; }

        [InverseProperty("BaiViet")]
        public virtual ICollection<ChuyenMuc_BaiViet> ChuyenMuc_BaiViet { get; set; }
    }
}
