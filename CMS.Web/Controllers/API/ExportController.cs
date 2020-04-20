using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using CMS.Models;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace G02Apis.Controllers
{
    public class FileUploadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public FileResult Download(string key)
        {
            var path = db.FileUpload.Where(x => x.FileKey == key).ToList();
            if (path == null || path.Count == 0)
                throw new HttpException(404, "File not found!");
            System.IO.FileInfo fi = new System.IO.FileInfo(path[0].FilePath);
            return File(path[0].FilePath, fi.GetType().ToString(), path[0].FileName + "." + path[0].FileType);
        }

        public FileResult Excel(string key)
        {
            string root = @"~/FileUpload";
            string fPath = root + "\\" + key + ".xlsx";
            System.IO.FileInfo fi = new System.IO.FileInfo(fPath);
            return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "export" + fi.Extension);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
