using BlogFall.Areas.Admin.ViewModels;
using BlogFall.Attributes;
using BlogFall.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    [BreadCrumbAttributes("Gönderiler")]
    public class PostsController : AdminBaseController
    {
        [BreadCrumbAttributes("Indeks")]
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View(db.Posts.OrderByDescending(x => x.CreateTime).ToList());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }
            db.Posts.Remove(post);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [BreadCrumbAttributes("Düzenle")]
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            PostEditViewModels vm = db.Posts.Select(x => new PostEditViewModels
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Content = x.Content,
                Title = x.Title
            }).FirstOrDefault(x => x.Id == id);

            return View(vm);
        }

        [BreadCrumbAttributes("Düzenle")]
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditViewModels model)
        {
            if (ModelState.IsValid)
            {
                Post post = db.Posts.Find(model.Id);
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.Title = model.Title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            return View();
        }

        [BreadCrumbAttributes("Yeni")]
        public ActionResult New()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            return View("Edit", new PostEditViewModels());
        }

        [BreadCrumbAttributes("Yeni")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult New(PostEditViewModels model)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    AuthorId = User.Identity.GetUserId(),
                    CreateTime = DateTime.Now
            };
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
        }
        ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            return View("Edit", new PostEditViewModels());
        }

        [HttpPost]
        public ActionResult AjaxImageUpload(HttpPostedFileBase file)
        {
            if (file==null || file.ContentLength==0 || !file.ContentType.StartsWith("image/"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var saveFolderPath = Server.MapPath("~/Upload/Posts");
            var ext = Path.GetExtension(file.FileName);
            var saveFileName = Guid.NewGuid() + ext;
            var saveFilePath = Path.Combine(saveFolderPath, saveFileName);
            file.SaveAs(saveFilePath);

            return Json(new { url= Url.Content("~/Upload/Posts/" + saveFileName) });
        }
}
}