namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XaPhuong")]
    public partial class XaPhuong
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        public int QuanHuyenId { get; set; }

        [StringLength(100)]
        public string TenXaPhuong { get; set; }

        [ForeignKey("QuanHuyenId")]
        public virtual QuanHuyen QuanHuyen { get; set; }

        [InverseProperty("XaPhuong")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
