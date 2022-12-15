using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    public class RelatedLinkController: ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Insert(int? id)
        {
            if(id.HasValue)
            {
                ViewBag.Links = UnitOfWork.RelatedLinkRep.GtPostRelatedLinks(id.Value).ToList();
            }
            ViewBag.Posts = UnitOfWork.PostRep.GetAll().Select(x => new {Text = x.Title, Val = x.PostId}).ToArray().Select(x => new SelectListItem {Text = x.Text, Value = x.Val.ToString(), Selected = id.HasValue && x.Val == id.Value}).ToArray();
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Insert(RelatedLink model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UnitOfWork.RelatedLinkRep.Insert(model);
                    UnitOfWork.SaveChanges();
                }
            }
            catch(Exception exception)
            {
                ModelState.AddModelError("", exception);
            }

            return RedirectToAction("Insert", new {id = model.PostId});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, int postId)
        {
            UnitOfWork.RelatedLinkRep.DeleteById(id);
            UnitOfWork.SaveChanges();

            return RedirectToAction("Insert", new{id=postId});
        }

        public ActionResult PostRelatedLinks(int id)
        {
            return PartialView(UnitOfWork.RelatedLinkRep.GtPostRelatedLinks(id).ToList());
        }
    }
}