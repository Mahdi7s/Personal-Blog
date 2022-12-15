using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class SessionRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public SessionRepository(SiteOfMeDbContext siteOfMeDbContext)
        {
            _unitOfWork = new UnitOfWork(siteOfMeDbContext);
        }

        //public User CurrentUser
        //{
        //    get { return GetObject("CurrentUser", () => _unitOfWork.UserRep.GetCurrentUser()); }
        //}

        //--------------------------------------------------------------------------------------------

        private void SetObject(string key, object obj)
        {
            HttpContext.Current.Session[key] = obj;
        }

        private T GetObject<T>(string key, Func<T> providerFunc) where T : class
        {
            if (HttpContext.Current.Session[key] == null)
            {
                HttpContext.Current.Session[key] = providerFunc();
            }
            return (T)HttpContext.Current.Session[key];
        }
    }
}