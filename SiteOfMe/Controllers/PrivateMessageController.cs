using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteOfMe.Controllers
{
    public class PrivateMessageController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Create()
        {
            return PartialView();
        }
    }
}
