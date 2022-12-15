using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    public class AppendixController : ControllerBase
    {
        public ActionResult Index(int? postId = null)
        {
            return RedirectToAction("Insert", postId);
        }

        [HttpGet]
        public ActionResult AppendicesPartial(int postId)
        {
            return PartialView(UnitOfWork.AppendixRep.GetAll().Where(x => x.PostId.Equals(postId)));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Insert(int? postId = null)
        {
            if (postId.HasValue)
            {
                var post = UnitOfWork.PostRep.GetByID(postId);
                ViewBag.Appendices = post.Appendices;
            }
            ViewBag.Posts = UnitOfWork.PostRep.GetAll().Select(x => new {Text = x.Title, Val = x.PostId}).ToArray().Select(x => new SelectListItem {Text = x.Text, Value = x.Val.ToString(), Selected = postId.HasValue && x.Val == postId.Value}).ToArray();

            return View();
        }

        [HttpPost]
        public ActionResult Insert(Appendix model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnitOfWork.AppendixRep.Insert(model);
                    UnitOfWork.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("Insert", new { postId = model.PostId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int appendixId, int postId)
        {
            try
            {
                UnitOfWork.AppendixRep.DeleteById(appendixId);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("Insert", new { postId = postId });
        }

        [HttpGet]
        [AjaxAction]
        public ActionResult LoadAppendix(int id)
        {
            var model = UnitOfWork.AppendixRep.GetByID(id);
            return PartialView(model);
        }
    }
}
