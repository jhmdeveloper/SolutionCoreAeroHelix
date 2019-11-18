using SolutionCoreAeroHelix.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SolutionCoreAeroHelix.ActionFilters
{
    public class LogErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Logger.Write(filterContext.Exception);
        }
    }
}