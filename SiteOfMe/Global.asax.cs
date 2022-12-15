using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataAnnotationsExtensions.ClientValidation;
using Mvc7S;
using SiteOfMe.Models;
using System.Web.Http;

namespace SiteOfMe
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
 
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-IR");
        }
    }
}