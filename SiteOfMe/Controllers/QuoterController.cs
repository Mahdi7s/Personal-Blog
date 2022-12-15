using SiteOfMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteOfMe.Controllers
{
    [Authorize(Roles="Admin")]
    public class QuoterController : ControllerBase
    {
        //
        // GET: /Quoter/

        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(UnitOfWork.QuoterRep.GetAll().ToList());
        }

        [HttpPost]
        public ActionResult Create(Quoter model) 
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Name)) // insert
            {
                if (UnitOfWork.QuoterRep.GetAll().Any(x => x.Name == model.Name))
                {
                    ModelState.AddModelError("", "this name already exists...");
                }
                else
                {
                    UnitOfWork.QuoterRep.Insert(model);
                    UnitOfWork.SaveChanges();
                }
            }
            return View(UnitOfWork.QuoterRep.GetAll().ToList());
        }

        public ActionResult Delete(int id)
        {
            return View(UnitOfWork.QuoterRep.GetByID(id));
        }

        [HttpPost]
        public ActionResult Delete(Quoter model)
        {
            UnitOfWork.QuoterRep.DeleteEntity(model);
            UnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
