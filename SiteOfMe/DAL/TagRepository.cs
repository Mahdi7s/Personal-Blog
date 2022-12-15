using System;
using System.Linq;
using Mvc7S;
using SiteOfMe.Models;
using System.Collections.Generic;

namespace SiteOfMe.DAL
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(SiteOfMeDbContext dbContext)
            : base(dbContext)
        {
        }

        public Tag GetByName(string tagName)
        {
            if (!string.IsNullOrEmpty(tagName))
            {
                return _dbSet.FirstOrDefault(x => x.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

        public Tag GetByNameAddIfNotInDb(string tagName)
        {
            var retVal = GetByName(tagName);
            if (retVal == null)
            {
                retVal = _dbSet.Add(new Tag { Name = tagName });
                _dbContext.SaveChanges();
            }
            return retVal;
        }
    }
}