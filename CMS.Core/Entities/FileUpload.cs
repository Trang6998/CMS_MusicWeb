using CMS.Core.Features;
using CMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entities
{
    public partial class FileUpload : BaseEntity
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FileSize { get; set; }

        public string FilePath { get; set; }

        public string FileKey { get; set; }

        public string FileDescription { get; set; }
    }
}
