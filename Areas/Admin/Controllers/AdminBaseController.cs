using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuxyryWatch.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        // GET: Admin/AdminBase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session["Admin"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AdminLogin", action = "Index", Areas = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}