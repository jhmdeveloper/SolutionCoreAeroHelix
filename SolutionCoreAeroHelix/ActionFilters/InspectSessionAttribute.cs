using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SolutionCoreAeroHelix.Helpers;

namespace SolutionCoreAeroHelix.ActionFilters
{
    public class InspectSessionAttribute : ActionFilterAttribute
    {
        private ControllerBase BaseController { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Convert.ToInt32(HttpContext.Current.Session["UserId"]) == 0
                && filterContext.ActionDescriptor.ActionName != "Autenticar"
                && filterContext.ActionDescriptor.ActionName != "Timeout")
            {
                string area = "";
                string controller = "Usuarios";
                string action = (Inspector.SessionStarted.HasValue) ? "Timeout" : "Autenticar";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area, controller, action }));
                Inspector.ClearSessionStart();
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}