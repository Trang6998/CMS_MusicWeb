namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiHat")]
    public partial class BaiHat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BaiHatID { get; set; }

        [StringLength(50)]
        public string TenBaiHat { get; set; }

        public string LinkFileNhac { get; set; }

        public string AnhDaiDien { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public DateTime NgayDang { get; set; }

        public int? TheLoaiID { get; set; }

        public bool? HienThiTrangChu { get; set; }

        [InverseProperty("BaiHat")]
        public virtual ICollection<Album_BaiHat> Album_BaiHat { get; set; }

        [ForeignKey("TheLoaiID")]
        public virtual TheLoai TheLoai { get; set; }

        [InverseProperty("BaiHat")]
        public virtual ICollection<CaSy_BaiHat> CaSy_BaiHat { get; set; }
    }
}
