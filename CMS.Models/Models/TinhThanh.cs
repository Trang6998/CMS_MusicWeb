namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinhThanh")]
    public partial class TinhThanh
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [StringLength(200)]
        public string TenTinhThanh { get; set; }

        [InverseProperty("TinhThanh")]
        public virtual ICollection<QuanHuyen> QuanHuyens { get; set; }

        [InverseProperty("TinhThanh")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
