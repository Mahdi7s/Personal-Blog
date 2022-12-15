using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.Models;
using SiteOfMe.Utils;
using SiteOfMe.ViewModels;
using System.Data.Entity.Infrastructure;

namespace SiteOfMe.Controllers
{
    public class CommentController : ControllerBase
    {
        [AjaxAction, Authorize(Roles = "Admin")]
        public ActionResult WaitingCommentsPartial()
        {
            return
                PartialView(UnitOfWork.CommentRep.GetAll().Where(x => !x.IsVisible).Include(x=>x.Post).Include(x=>x.Reply).Include(x=>x.AnonymousUser).OrderBy(x => x.PublishDate));
        }

        public ActionResult CreateNewPartial(int postId)
        {
            ViewBag.IsCommentSent = false;
            return PartialView(new Comment { PostId = postId });
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [CaptchaValidator]
        public ActionResult CreateNewPartial(Comment comment, bool captchaValid)
        {
            if (ModelState.IsValid && captchaValid)
            {
                if (comment.Body.Value.Length > 800)
                {
                    ModelState.AddModelError("", "طول متن نمی تواند بیش از 800 کاراکتر باشد");
                    return PartialView(comment);
                }

                try
                {                    
                    comment.IsVisible = false;
                    comment.IsReply = false;
                    comment.PublishDate = DateTime.Now;                    
                    comment.AnonymousUser.UserIP = VisitorIP.GetIP();
                    
                    //UnitOfWork.AnonymousUserRep.Insert(comment.AnonymousUser);
                    //UnitOfWork.SaveChanges();
                    //comment.AnonymousUserId = comment.AnonymousUser.AnonymousUserId;

                    UnitOfWork.CommentRep.Insert(comment);                    
                    UnitOfWork.SaveChanges();

                    ViewBag.IsCommentSent = true;
                    return PartialView();
                }
                catch(Exception ex)
                {                    
                    ModelState.AddModelError("", ex.Message);
                }
            }            
            ViewBag.IsCommentSent = false;

            return PartialView(comment);
        }

        public ActionResult PostCommentsPartial(int postId)
        {
            var model = UnitOfWork.CommentRep.GetAll().Where(x => x.PostId.Equals(postId)).Include(x => x.Body).Include(x => x.AnonymousUser).OrderBy(x => x.PublishDate).ToList();
            ViewBag.PostTitle =
                UnitOfWork.PostRep.GetAll().Select(x => new {Id = x.PostId, Title = x.Title}).First(
                    x => x.Id.Equals(postId)).Title;
            return PartialView(model);
        }

        [Authorize(Roles = "Admin"), AjaxAction]
        public ActionResult Delete(int id, int postId)
        {
            Comment comment = null;
            try
            {
                comment = UnitOfWork.CommentRep.GetByID(id);
                UnitOfWork.CommentRep.DeleteEntity(comment);
                UnitOfWork.SaveChanges();
            }
            catch
            {
                return PartialView("_CommentPartial", comment);
            }
            return new HttpStatusCodeResult((int) HttpStatusCode.OK);
        }
        
        [Authorize(Roles = "Admin"), AjaxAction]
        public ActionResult SetVisible(int id)
        {
            try
            {
                var comment = UnitOfWork.CommentRep.GetByID(id);                    
                comment.IsVisible = true;
                UnitOfWork.CommentRep.Update(comment);
                
                UnitOfWork.SaveChanges();

                return PartialView("_CommentPartial", comment);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateReply(int id)
        {
            return PartialView(new CommentReplyViewModel{CommentId = id});
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult CreateReply(CommentReplyViewModel model)
        {
            var currentUser = UnitOfWork.UserRep.GetCurrentUser();
            var comment = UnitOfWork.CommentRep.GetByID(model.CommentId);
            try
            {
                comment.Reply = new Comment
                                    {
                                        PostId = comment.PostId,
                                        Body = (BigString) model.Reply,
                                        IsVisible = true,
                                        IsReply = true,
                                        PublishDate = DateTime.Now,
                                        Title = string.Format("پاسخ : {0}", comment.Title),
                                        AnonymousUser = new AnonymousUser{Name = currentUser.DisplayName}
                                    };
                UnitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return PartialView("_CommentPartial", comment);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditReply(int commentId)
        {
            var model = UnitOfWork.CommentRep.GetByID(commentId);
            return PartialView(new CommentReplyViewModel {CommentId = model.CommentId, Reply = model.Reply.Body.Value});
        }

        [HttpPost, Authorize(Roles = "Admin")]        
        public ActionResult EditReply(CommentReplyViewModel model)
        {
            Comment comment = null;
            try
            {
                if (ModelState.IsValid)
                {
                    comment = UnitOfWork.CommentRep.GetByID(model.CommentId);
                    comment.Reply.Body.Value = model.Reply;
                    comment.Reply.AnonymousUser.Name = UnitOfWork.UserRep.GetCurrentUser().DisplayName;

                    UnitOfWork.SaveChanges();
                }
            }
            catch
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
            return PartialView("_CommentPartial", comment);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.CommentRep.GetByID(id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Edit(Comment model)
        {
            try
            {
                var anonymous = model.AnonymousUser;
                anonymous.AnonymousUserId = model.AnonymousUserId;

                var comment = model;
                comment.AnonymousUser = null;

                UnitOfWork.AnonymousUserRep.Attach(anonymous);
                UnitOfWork.AnonymousUserRep.Update(anonymous);                

                UnitOfWork.CommentRep.Attach(comment);
                UnitOfWork.CommentRep.Update(comment);
                UnitOfWork.CommentRep.Update(comment.Body);

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View(model);
            }

            return RedirectToAction("Details", "Post", new {postId = model.PostId});
        }

        [ChildActionOnly]
        public ActionResult TopRecentCommentsPartial(int commentsCount = 6)
        {
            var model = UnitOfWork.CommentRep.GetAll().Where(x => x.IsVisible && !x.IsReply && x.Post.IsPublished).OrderByDescending(x => x.PublishDate).Take(commentsCount).Select(x => new RecentCommentViewModel { CommentId = x.CommentId, PostId = x.PostId, PostTitle = x.Post.Title, AuthorName = x.AnonymousUser.Name }).ToArray();
            return PartialView(model);
        }
    }
}
