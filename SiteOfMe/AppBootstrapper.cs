using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataAnnotationsExtensions.ClientValidation;
using Mvc7S;
using SiteOfMe.Models;
using System.Web.Http;

namespace SiteOfMe
{
    public class AppBootstrapper// : MefBootstrapper
    {
        public void Run()
        {
            AreaRegistration.RegisterAllAreas();
            //RegisterGlobalFilters(GlobalFilters.Filters);

            RegisterRoutes(RouteTable.Routes);
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }

        //protected override IEnumerable<Assembly> SelectAssemblies()
        //{
        //    yield return GetType().Assembly;
        //}

        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}/{title}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, title=UrlParameter.Optional } // Parameter defaults
                );
        }
    }
}