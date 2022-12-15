using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SiteOfMe.Models;
using TS7S.Asp;
using TS7S.Asp.Mvc;

namespace SiteOfMe.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {

            if (UnitOfWork.RoleRep.IsThereAnyInRole("admin"))
            {
                return RedirectToAction("Index", "Post");
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }

        public FeedResult Feed()
        {
            var syndicationItems = UnitOfWork.PostRep.GetPublishedPosts(null, null).ToArray().Select(x =>
                                                                    new SyndicationItem(x.Title, x.BodySummary.Value, new Uri(Url.Action("Details", "Post", new { postId = x.PostId }), UriKind.Relative)));
            return new FeedResult("www.Mahdi7$.com Feed", syndicationItems, "Latest blog entries from www.Mahdi7$.com");
        }
    }
}
