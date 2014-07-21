using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForeclosureList.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Hyperlink(this HtmlHelper helper, string url, string linkText)
        {
            return MvcHtmlString.Create(String.Format("<a href='{0}'>{1}</a>", url, linkText));
        }
    }
}