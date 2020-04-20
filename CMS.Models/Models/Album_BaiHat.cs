namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album_BaiHat
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlbumID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BaiHatID { get; set; }

        public int? SoThuTu { get; set; }

        [ForeignKey("AlbumID")]
        public virtual Album Album { get; set; }
        [ForeignKey("BaiHatID")]
        public virtual BaiHat BaiHat { get; set; }
    }
}
