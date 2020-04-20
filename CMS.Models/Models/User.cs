namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("auth.Users")]
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(5)]
        public string PasswordSalt { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastLogin { get; set; }

        [StringLength(2)]
        public string Lang { get; set; }

        public bool Active { get; set; }
        public int? NhanVienID { get; set; }

        [ForeignKey("NhanVienID")]
        public virtual NhanVien NhanVien { get; set; }

    }
}
