namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("auth.AccessTokens")]
    public partial class AccessToken
    {
        public int AccessTokenId { get; set; }

        [Required]
        [StringLength(200)]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public DateTimeOffset EffectiveTime { get; set; }

        public int ExpiresIn { get; set; }

        [Required]
        [StringLength(200)]
        public string UserAgent { get; set; }

        [StringLength(20)]
        public string IP { get; set; }
    }
}
