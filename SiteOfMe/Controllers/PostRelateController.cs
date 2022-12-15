using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostRelateController : ControllerBase
    {
        public ActionResult Create(int? postId = null)
        {
            SetInsertedPostRelates(postId);
            ViewData["Posts"] =
                UnitOfWork.PostRep.GetAll().Select(x=> new{PostId=x.PostId, Title=x.Title}).ToList().Select(
                    x =>
                    new SelectListItem {Selected = postId.HasValue && x.PostId.Equals(postId.Value), Text = x.Title, Value = x.PostId.ToString()})
                    .ToList();

            return View(postId.HasValue ? new PostRelate{ PostId = postId.Value } : new PostRelate());
        }

        [HttpPost]
        public ActionResult Create(PostRelate model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UnitOfWork.PostRelateRep.Insert(model);
                    UnitOfWork.SaveChanges();

                    return RedirectToAction("Create", new {postId = model.PostId});
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            SetInsertedPostRelates(model.PostId);
            return View(model);
        }

        [ChildActionOnly, AjaxAction]
        public ActionResult PostRelatesPartial(int postId)
        {
            ViewBag.PrevPostId = UnitOfWork.PostRelateRep.GetPrevPost(postId);
            ViewBag.NextPostId = UnitOfWork.PostRelateRep.GetNextPost(postId);
            return PartialView(UnitOfWork.PostRelateRep.GetRelateForPost(postId));
        }

        private void SetInsertedPostRelates(int? postId)
        {
            ViewBag.InsertedPostRelates = postId.HasValue
                                              ? UnitOfWork.PostRelateRep.GetAll().Where(x => x.PostId.Equals(postId.Value))
                                              : null;
        }
    }
}
