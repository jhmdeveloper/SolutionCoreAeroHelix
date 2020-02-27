using SolutionCoreAeroHelix.ActionFilters;
using System.Web.Mvc;

namespace SolutionCoreAeroHelix
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InspectSessionAttribute());
            filters.Add(new LogErrorAttribute());
        }
    }
}
