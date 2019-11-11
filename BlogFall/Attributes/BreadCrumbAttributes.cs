using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFall.Attributes
{
    public class BreadCrumbAttributes: Attribute
    {
        public string Name { get; set; }

        public BreadCrumbAttributes(string name)
        {
            Name = name;
        }
    }
}