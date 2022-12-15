using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    public class UserController : ControllerBase
    {
        [Authorize(Roles = "User,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [AjaxAction]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Profile()
        {
            return PartialView(UnitOfWork.UserRep.GetCurrentUser());
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult EditProfile()
        {
            var currentUser = UnitOfWork.UserRep.GetCurrentUser();
            currentUser.PasswordConfirm = currentUser.Password;

            return PartialView(currentUser);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult EditProfile(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UnitOfWork.UserRep.GetByID(model.UserId);
                    UpdateModel(user);
                    UnitOfWork.UserRep.Update(user);
                    UnitOfWork.SaveChanges();

                    return PartialView("SuccessfulPartial", "ویرایش با موفقیت انجام شد.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = (24 * 60 * 60) * 10)]
        public ActionResult AboutPartial(int userId)
        {
            return PartialView(UnitOfWork.UserRep.GetCurrentUser());
        }

        public ActionResult About(int userId)
        {
            return View(UnitOfWork.UserRep.GetByID(userId));
        }
    }
}
