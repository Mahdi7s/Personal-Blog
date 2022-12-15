using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class AnonymousUserRepository : Repository<AnonymousUser>
    {
        public AnonymousUserRepository(SiteOfMeDbContext dbContext) : base(dbContext) { }
    }
}