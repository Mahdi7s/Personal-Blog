using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    public class TagController : ControllerBase
    {
        //
        // GET: /Tag/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TagsPartial(int? postId = null)
        {
            IEnumerable<Tag> tags = postId.HasValue ? UnitOfWork.PostRep.GetAll().Where(x => x.PostId == postId.Value).SelectMany(x => x.Tags).ToList() : UnitOfWork.TagRep.Get(x => x.Posts.Any(p => p.IsPublished)).ToList();
            return PartialView(tags);
        }

        [AjaxAction]
        public string GetTags(string tag)
        {
            var tags = UnitOfWork.TagRep.GetAll().Where(x => x.Name.ToLower().StartsWith(tag.ToLower())).ToArray();
            var retval = "[";
            foreach (var t in tags)
            {
                retval += string.Format("{3} \"key\":\"{0}\", \"value\":\"{1}\" {2}, ", t.Name, t.Name, "}", "{");
            }
            retval += "]";
            return retval;
        }

        //[AjaxAction]
        public ActionResult TagsFor(string term)
        {
            var tags = UnitOfWork.TagRep.GetAll().Where(x => x.Name.ToLower().StartsWith(term.ToLower())).ToArray();
            return Json(tags.Select(x => x.Name).ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}
