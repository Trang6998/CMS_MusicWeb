namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuanHuyen")]
    public partial class QuanHuyen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string TenQuanHuyen { get; set; }

        public int TinhThanhId { get; set; }

        [ForeignKey("TinhThanhId")]
        public virtual TinhThanh TinhThanh { get; set; }

        [InverseProperty("QuanHuyen")]
        public virtual ICollection<XaPhuong> XaPhuongs { get; set; }

        [InverseProperty("QuanHuyen")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
