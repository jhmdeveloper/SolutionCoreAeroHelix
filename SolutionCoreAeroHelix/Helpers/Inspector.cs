using System;
using System.Web;

namespace SolutionCoreAeroHelix.Helpers
{
    public class Inspector
    {
        static readonly string sessionCookieName = "SessionStart";

        public static DateTime? SessionStarted
        {
            get
            {
                DateTime? sessionStarted = null;

                if (HttpContext.Current.Request.Cookies[sessionCookieName] != null)
                {
                    if (DateTime.TryParse(HttpContext.Current.Request.Cookies[sessionCookieName].Value.ToString(), out var dateTime))
                    {
                        sessionStarted = dateTime;
                    }
                }

                return  sessionStarted;
            }
        }

        public static void SetSessionStart()
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(sessionCookieName, DateTime.Now.ToString()));
        }

        public static void ClearSessionStart()
        {
            if (SessionStarted.HasValue)
            {
                HttpContext.Current.Response.Cookies[sessionCookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}