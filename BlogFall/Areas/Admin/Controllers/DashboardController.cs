using BlogFall.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    [BreadCrumbAttributes("Anasayfa")]
    public class DashboardController : AdminBaseController
    {
        // GET: Admin/Dashboard
        [BreadCrumbAttributes("İndeks")]
        public ActionResult Index()
        {
            return View();
        }
    }
}