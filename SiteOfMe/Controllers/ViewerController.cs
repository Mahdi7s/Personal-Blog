using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteOfMe.ViewModels;

namespace SiteOfMe.Controllers
{
    public class ViewerController : ControllerBase
    {
        //
        // GET: /Viewer/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view">is a partialView that contains the contents of showing</param>
        /// <param name="type">type of showing content(like html)</param>
        /// <returns></returns>
        public ActionResult Index(string view, string type)
        {
            var viewName = string.Empty;
            switch (type)
            {
                case "":
                    break;
                case "htm":
                    viewName = "HtmViewer";
                    break;
            }

            return View(viewName, new{partialToShow=view});
        }

        //we need a html.helper to simpilfy addressing & linking 'demos'
    }
}
