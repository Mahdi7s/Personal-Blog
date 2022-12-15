using System;
using Mvc7S;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using SiteOfMe.Models;
using SiteOfMe.ViewModels;

namespace SiteOfMe.Controllers
{    
    public class QuoteController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(UnitOfWork.QuoteRep.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Quoters = UnitOfWork.QuoterRep.GetAll().ToArray();
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Create(Quote model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UnitOfWork.QuoteRep.Insert(model);
                    UnitOfWork.SaveChanges();

                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);             
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(UnitOfWork.QuoteRep.GetByID(id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Delete(Quote model)
        {
            try
            {
                UnitOfWork.QuoteRep.DeleteEntity(model);
                UnitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.QuoteRep.GetByID(id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Edit(Quote model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnitOfWork.QuoteRep.Update(model);
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

        public ActionResult CurrentQuotePartial()
        {
            var quote = UnitOfWork.QuoteRep.GetAll().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            var postsIds = UnitOfWork.PostRep.GetAll().Where(x=>x.IsPublished).OrderBy(x => x.PublishDate).Select(x => x.PostId).ToArray();
            if (!postsIds.IsNullOrEmpty())
            {
                var currentPostIdStr = Request.QueryString["postId"];
                int currentPostId;
                if (int.TryParse(currentPostIdStr, out currentPostId))
                {
                    int postIdIndex = 0;
                    foreach (var postsId in postsIds)
                    {
                        if (postsId == currentPostId) break;
                        ++postIdIndex;
                    }

                    ViewBag.Navigator = new NavModel
                                            {
                                                PrevPostId = postIdIndex > 0 ? postsIds[postIdIndex - 1] : -1,
                                                NextPostId =
                                                    postIdIndex < postsIds.Length - 1
                                                        ? postsIds[postIdIndex + 1]
                                                        : -1
                                            };
                }
                else
                {
                    var rand = new Random();
                    ViewBag.Navigator = new NavModel
                                            {
                                                PrevPostId = postsIds[rand.Next(0, postsIds.Length/2)],
                                                NextPostId = postsIds[rand.Next(postsIds.Length/2, postsIds.Length/2)]
                                            };
                }
            }
            return PartialView(quote);
        }
    }
}
