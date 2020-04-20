namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChuyenMuc_BaiViet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChuyenMucID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BaiVietID { get; set; }

        [StringLength(50)]
        public string TenChuyenMuc { get; set; }

        [ForeignKey("BaiVietID")]
        public virtual BaiViet BaiViet { get; set; }
        [ForeignKey("ChuyenMucID")]
        public virtual ChuyenMuc ChuyenMuc { get; set; }
    }
}
