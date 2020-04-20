namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiNhanVien")]
    public partial class LoaiNhanVien
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int LoaiNhanVienID { get; set; }

        [StringLength(50)]
        public string TenLoai { get; set; }

        [InverseProperty("LoaiNhanVien")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
