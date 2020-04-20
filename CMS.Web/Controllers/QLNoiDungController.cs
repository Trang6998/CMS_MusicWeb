using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class QLNoiDungController : Controller
    {
        // GET: QLNoiDung
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}