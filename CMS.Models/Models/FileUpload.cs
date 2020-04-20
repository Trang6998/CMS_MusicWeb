namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileUpload")]
    public partial class FileUpload
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FileSize { get; set; }

        public string FilePath { get; set; }

        public string FileKey { get; set; }

        public string FileDescription { get; set; }
    }
}
