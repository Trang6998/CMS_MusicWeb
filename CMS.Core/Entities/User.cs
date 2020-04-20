using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class User : BaseEntity
    {
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
    }
}
