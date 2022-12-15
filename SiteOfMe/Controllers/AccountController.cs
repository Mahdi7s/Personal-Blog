using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Elmah;
using Mvc7S;
using SiteOfMe.Models;
using SiteOfMe.ViewModels;

namespace SiteOfMe.Controllers
{
    public class AccountController : ControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if(!string.IsNullOrEmpty(model.UserName))
                {
                    model.UserName = model.Email;
                }
                UnitOfWork.UserRep.Insert(model);
                UnitOfWork.SaveChanges();

                return RedirectToAction("Index", "Post");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View(model);
            }
        }
        
        public ActionResult Login()
        {
            return View(new LoginViewModel{RememberMe = true});
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if(UnitOfWork.UserRep.CanLogin(model.UserName, model.Password))
            {                
                //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                   
                //if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                //else return RedirectToAction("Index", "Home");
                FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور نادرست می باشد.");
                return View(model);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [AjaxAction]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        [AjaxAction]
        public ActionResult RegisterPartial()
        {
            return PartialView("_RegisterPartial");
        }

        [AjaxAction]
        public ActionResult CheckUserName(string username)
        {
            var res = false;
            if(User.Identity.IsAuthenticated && User.Identity.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
                res = true;
            else
                res = (!UnitOfWork.UserRep.GetAll().Any(x => x.UserName.ToLower().Equals(username.ToLower())));

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [AjaxAction]
        public ActionResult CheckEmail(string email)
        {
            var res = false;
            if (User.Identity.IsAuthenticated)
                res = !UnitOfWork.UserRep.GetAll().Any(x => !x.UserName.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) && x.Email.ToLower().Equals(email.ToLower()));
            else
                res = !UnitOfWork.UserRep.GetAll().Any(x => x.Email.ToLower().Equals(email.ToLower()));

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        //[OutputCache(Duration = 60*60*30, Location = OutputCacheLocation.Any, VaryByParam = "id")]
        public ActionResult UserInfoPartial(int id)
        {
            var user = UnitOfWork.UserRep.GetByID(id);
            return PartialView(user);
        }
    }
}
