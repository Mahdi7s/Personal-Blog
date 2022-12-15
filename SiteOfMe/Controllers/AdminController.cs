using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.ViewModels;

namespace SiteOfMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new AdminViewModel
                            {
                                WaitingCommentsCount = UnitOfWork.CommentRep.GetAll().Count(x => !x.IsReply && !x.IsVisible),
                                UnpublishedPostsCount = UnitOfWork.PostRep.GetAll().Count(x => !x.IsPublished)
                            }
                );
        }

        [AjaxAction, ChildActionOnly]
        public ActionResult ContactMePartial()
        {
            return PartialView();
        }
    }
}
