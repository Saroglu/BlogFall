﻿using BlogFall.Attributes;
using BlogFall.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Helper
{
    public static class HtmlHelpers
    {
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }
        public static string ActionName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();
        }
        public static string BreadCrumbControllerName(this HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ControllerName();
            Type t = Type.GetType("BlogFall.Areas.Admin.Controllers." + controller + "Controller");
            object[] attributes = t.GetCustomAttributes(typeof(BreadCrumbAttributes), true);
            if (attributes.Length == 0)
            {
                return controller;
            }

            var attr = (BreadCrumbAttributes)attributes[0];
            return attr.Name;

        }
        public static string BreadCrumbActionName(this HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ControllerName();
            string action = htmlHelper.ActionName();
            Type t = Type.GetType("BlogFall.Areas.Admin.Controllers." + controller + "Controller");
            MethodInfo mi = t.GetMethods().FirstOrDefault(x => x.Name == action);
            BreadCrumbAttributes ba = mi.GetCustomAttribute(typeof(BreadCrumbAttributes)) as BreadCrumbAttributes;

            if (ba == null)
            {
                return action;

            }

            return ba.Name;
        }

        public static IHtmlString ShowPostIntro(this HtmlHelper htmlHelper, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return htmlHelper.Raw("");
            }
            int pos = content.IndexOf("<hr>");
            if (pos == -1)
            {
                return htmlHelper.Raw(content);
            }

            return htmlHelper.Raw(content.Substring(0, pos));
        }
        public static IHtmlString ShowPost(this HtmlHelper htmlHelper, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return htmlHelper.Raw("");
            }
            int pos = content.IndexOf("<hr>");
            if (pos == -1)
            {
                return htmlHelper.Raw(content);
            }

            return htmlHelper.Raw(content.Remove(pos, 4));
        }
        public static string ProfilePhotoPath(this HtmlHelper htmlHelper)
        {
            string userId = htmlHelper.ViewContext.HttpContext.User.Identity.GetUserId();

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
            if (userId != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.Find(userId);
                    if (user != null && !string.IsNullOrEmpty(user.Photo))
                    {
                        return urlHelper.Content("~/Upload/Profiles/"+ user.Photo);
                    }
                }

            }
            return urlHelper.Content("~/Images/avatar.png");

        }
    }
}