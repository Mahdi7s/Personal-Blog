using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class QuoteRepository : Repository<Quote>
    {
        public QuoteRepository(SiteOfMeDbContext dbContext) : base(dbContext)
        {
        }
    }
}