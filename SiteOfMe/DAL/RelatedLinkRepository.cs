using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class RelatedLinkRepository:Repository<RelatedLink>
    {
        public RelatedLinkRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RelatedLink> GtPostRelatedLinks(int postId)
        {
            return _dbSet.Where(x => x.PostId.Equals(postId));
        }
    }
}