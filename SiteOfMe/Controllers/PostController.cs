using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.Models;
using SiteOfMe.Utils;
using SiteOfMe.ViewModels;

namespace SiteOfMe.Controllers
{
    public class PostController : ControllerBase
    {
        public ActionResult Index(string k = null, string c = null)
        {
            return View(UnitOfWork.PostRep.GetPublishedPosts(k, c).ToList());
        }

        [AjaxAction, Authorize(Roles = "Admin")]
        public ActionResult UnpublishedPostsPartial()
        {
            return
                PartialView(UnitOfWork.PostRep.GetAll().Where(x => !x.IsPublished).Include(x=>x.User).OrderBy(x => x.PublishDate));
        }

        [HttpGet]
        public ActionResult Details(int? id=null, string title = null, int? postId=null)
        {
            id = id ?? postId;
            if(string.IsNullOrEmpty(title))
            {
                title = UnitOfWork.PostRep.GetAll().Where(x => x.PostId.Equals(id.Value)).Select(x => x.Title).FirstOrDefault();
                return RedirectToAction("Details", new { id = id, title = CustomHtmlHelpers.EncodeTitle(null, title) });
            }
            
            ViewData["CommentsCount"] = UnitOfWork.CommentRep.GetCommentsCount(id.Value);
            var post = UnitOfWork.PostRep.GetByID(id.Value);
            if (post != null && (post.IsPublished || User.IsInRole("Admin")))
            {
                ++post.Views;
                UnitOfWork.PostRep.Update(post);
                UnitOfWork.SaveChanges();

                return View(post);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var nPost = Post.New(UnitOfWork.UserRep.GetCurrentUser());
            var temp =
                UnitOfWork.PostRep.GetAll().FirstOrDefault(
                    x => x.Title.Equals(nPost.Title) && x.BodySummary.Value.Equals(nPost.BodySummary.Value));
            if (temp == null)
            {
                UnitOfWork.PostRep.Insert(nPost);
                UnitOfWork.SaveChanges();
            }
            else nPost = temp;

            return View(nPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Post model, bool isAjaxSave)
        {
            return CreateEditPost(model, isAjaxSave);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var toEdit = UnitOfWork.PostRep.GetByID(id);
            return View(toEdit);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Post model, bool isAjaxSave)
        {
            return CreateEditPost(model, isAjaxSave);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = UnitOfWork.PostRep.GetAll().Where(x => x.PostId.Equals(id)).Include(x => x.User).First();
            return View(model);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Delete(Post model)
        {
            try
            {
                UnitOfWork.PostRep.DeleteById(model.PostId);
                UnitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Preview(int postId)
        {
            return RedirectToAction("Details", new {postId });
        }

        [ChildActionOnly]
        public ActionResult TopRecentPostLinksPartial(int postsCount = 6)
        {
            var model = UnitOfWork.PostRep.GetPublishedPosts(null, null).Take(postsCount).Select(x => new RecentPostViewModel { PostId = x.PostId, PostTitle = x.Title }).ToArray();
            return PartialView(model);
        }

        // --------------------------------------Helpers-------------------------------------

        private ActionResult CreateEditPost(Post model, bool isAjaxSave)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    UnitOfWork.PostRep.Attach(model);
                    UnitOfWork.PostRep.LoadCollectionProperty(model, x => x.Tags);
                    if (model.IsPublished)
                    {
                        var isPublishedBefore =
                        UnitOfWork.PostRep.GetAll().Select(x => new { Id = x.PostId, IsPublished = x.IsPublished }).First(
                            x => x.Id.Equals(model.PostId)).IsPublished;

                        if (!isPublishedBefore)
                            model.PublishDate = DateTime.Now;
                    }
                    else
                    {
                        model.PublishDate = DateTime.Now;
                    }
                    SetPostTags(model);
                    UnitOfWork.PostRep.Update(model);
                    UnitOfWork.SaveChanges();

                    if (isAjaxSave)
                    {
                        return Json(new { IsSuccessful = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            if (isAjaxSave)
            {
                var errors = ModelState.SelectMany(ms => ms.Value.Errors).Aggregate(string.Empty,
                                                                                    (current, error) =>
                                                                                    current + (error.ErrorMessage + "\r\n"));
                return Json(new { IsSuccessful = false, Errors = errors }, JsonRequestBehavior.AllowGet);
            }
            return View(model);
        }

        private void SetPostTags(Post model)
        {
            var tagsLst = Request.Form["tagsAutocomplete"];
            if (!string.IsNullOrEmpty(tagsLst))
            {
                var tags = tagsLst.Split(new[] {','}).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).Distinct().ToArray();
                model.Tags.Where(t => tags.All(x => !t.Name.Equals(x))).ToArray().Apply(x => model.Tags.Remove(x));//remove additional tags
                foreach (var tag in tags)
                {
                    if (!model.Tags.Any(x => x.Name.Equals(tag, StringComparison.OrdinalIgnoreCase)))
                    {
                        var tagEntity = UnitOfWork.TagRep.GetByNameAddIfNotInDb(tag);
                        model.Tags.Add(tagEntity);
                    }
                }
            }
        }
    }
}
