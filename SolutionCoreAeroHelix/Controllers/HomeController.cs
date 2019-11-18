using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolutionCoreAeroHelix.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Página de inicio de perfil actual
        /// </summary>
        private string PaginaInicio
        {
            get
            {
                var paginaInicio = "";

                if(Session["PaginaInicio"] != null)
                {
                    paginaInicio = Session["PaginaInicio"].ToString();
                }

                return paginaInicio;
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction(PaginaInicio, "Usuarios");
        }
    }
}
