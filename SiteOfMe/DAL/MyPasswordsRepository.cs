using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;
using SiteOfMe.Utils;

namespace SiteOfMe.DAL
{
    public class MyPasswordsRepository : Repository<MyPassword>
    {
        public MyPasswordsRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override void Insert(MyPassword entity)
        {            
            EntityEncryptor.Encrypt(entity);
   
            base.Insert(entity);
        }
    }
}