using System;
using System.Collections.Generic;
using System.IO;

namespace SolutionCoreAeroHelix.Helpers
{
    public class Logger
    {
        public static void Write(Exception exception)
        {
            var filename = DateTime.Now.ToString("yyyyMMdd");
            var path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Loggs/") + filename+ ".log";
            var separator = new string('-', 60) + Environment.NewLine;
            var fecha = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var log = new List<string> { separator, fecha, exception.ToString(), separator };
            File.AppendAllLines(path, log);
        }
    }
}