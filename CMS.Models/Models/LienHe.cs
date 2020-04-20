namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienHe")]
    public partial class LienHe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int LienHeID { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        public string NoiDung { get; set; }

        public bool? HienThi { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
