using BlogFall.Attributes;
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
            object[] attributes= t.GetCustomAttributes(typeof(BreadCrumbAttributes), true);
            if (attributes.Length ==0)
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
            MethodInfo mi= t.GetMethods().FirstOrDefault(x=> x.Name == action);
            BreadCrumbAttributes ba = mi.GetCustomAttribute(typeof(BreadCrumbAttributes)) as BreadCrumbAttributes;

            if (ba == null)
            {
                return action;
                
            }

            return ba.Name;
        }
    }
}