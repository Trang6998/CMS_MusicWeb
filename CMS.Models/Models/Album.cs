namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required] 
        public int AlbumID { get; set; }

        [StringLength(50)]
        public string TenAlbum { get; set; }

        public string AnhDaiDien { get; set; }
        //public string GioiThieu { get; set; }

        public bool? TrangThai { get; set; }

        [InverseProperty("Album")]
        public virtual ICollection<Album_BaiHat> Album_BaiHat { get; set; }
    }
}
