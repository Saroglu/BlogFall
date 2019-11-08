using BlogFall.Areas.Admin.ViewModels;
using BlogFall.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
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

        public ActionResult New()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            return View("Edit", new PostEditViewModels());
        }

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
}
}