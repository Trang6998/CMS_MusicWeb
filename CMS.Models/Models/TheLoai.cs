namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoai")]
    public partial class TheLoai
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TheLoaiID { get; set; }

        [StringLength(50)]
        public string TenTheLoai { get; set; }

        [StringLength(500)]
        public string GioiThieu { get; set; }

        [InverseProperty("TheLoai")]
        public virtual ICollection<BaiHat> BaiHats { get; set; }
    }
}
