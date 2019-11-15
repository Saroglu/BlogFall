using BlogFall.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    [BreadCrumbAttributes("Yorumlar")]
    public class CommentsController : AdminBaseController
    {
        [BreadCrumbAttributes("İndeks")]
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }
    }
}