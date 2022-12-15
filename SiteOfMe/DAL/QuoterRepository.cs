using Mvc7S;
using SiteOfMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteOfMe.DAL
{
    public class QuoterRepository: Repository<Quoter>
    {
         private readonly SiteOfMeDbContext _siteOfMeDbContext;
         public QuoterRepository(SiteOfMeDbContext siteOfMeDbContext)
             : base(siteOfMeDbContext)
        {
            _siteOfMeDbContext = siteOfMeDbContext;
        }
    }
}