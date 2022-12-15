using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    public class DownFileController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(UnitOfWork.DownFileRep.Get().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public ActionResult Create(DownFile model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UnitOfWork.DownFileRep.Insert(model);
                    UnitOfWork.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }
                
        public ActionResult ListPartial()
        {
            return PartialView(UnitOfWork.DownFileRep.Get().ToList());
        }

        public string FileImageSrc(string extension)
        {
            string src = string.Empty;
            if(string.IsNullOrEmpty(extension)) return src;

            switch (extension)
            {
                case "zip":
                    src = Url.Content("~/Content/Images/Extension/zip.png");
                    break;
            }

            return src;
        }
    }
}
