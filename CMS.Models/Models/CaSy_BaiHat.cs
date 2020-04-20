namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CaSy_BaiHat
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaSyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BaiHatID { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        public int? SoThuTu { get; set; }

        [ForeignKey("BaiHatID")]
        public virtual BaiHat BaiHat { get; set; }
        [ForeignKey("CaSyID")]
        public virtual CaSy CaSy { get; set; }
    }
}
