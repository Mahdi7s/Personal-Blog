using System;
using Mvc7S;
using SiteOfMe.DAL;

namespace SiteOfMe.Models
{
    public static class ModelExtensions
    {
        private static UnitOfWork UnitOfWork
        {
            get { return IoC.Get<UnitOfWork>(); }
        }

        

        //public static Comment New(this Comment comment)
        //{
        //    return new Comment
        //               {
                           
        //               }
        //}
    }
}