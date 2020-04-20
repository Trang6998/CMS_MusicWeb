namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Radio")]
    public partial class Radio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int RadioID { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string AnhDaiDien { get; set; }

        public string LinkFile { get; set; }

        public DateTime NgayDang { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotThich { get; set; }

        public int? NhanVienID { get; set; }

        public bool? TrangThai { get; set; }

        [ForeignKey("NhanVienID")]
        public virtual NhanVien NhanVien { get; set; }
    }
}
