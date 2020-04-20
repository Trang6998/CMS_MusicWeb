namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuyenMuc")]
    public partial class ChuyenMuc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ChuyenMucID { get; set; }

        [StringLength(50)]
        public string TenChuyenMuc { get; set; }

        public bool? TrangThai { get; set; }

        public bool? HienThiTrangChu { get; set; }

        [InverseProperty("ChuyenMuc")]
        public virtual ICollection<ChuyenMuc_BaiViet> ChuyenMuc_BaiViet { get; set; }
    }
}
