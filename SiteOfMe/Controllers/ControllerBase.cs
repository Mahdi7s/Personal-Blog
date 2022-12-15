using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteOfMe.DAL;
using SiteOfMe.Models;

namespace SiteOfMe.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly UnitOfWork UnitOfWork = new UnitOfWork();
    }
}
