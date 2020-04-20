using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class AccessToken : BaseEntity
    {
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

        [Required]
        [StringLength(20)]
        public string IP { get; set; }
    }
}
