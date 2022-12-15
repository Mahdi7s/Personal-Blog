using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class PrivateMessageRepository : Repository<PrivateMessage>
    {
        public PrivateMessageRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<PrivateMessage> GetUnReplyedMessages()
        {
            return _dbSet.Where(x => !x.ReplyId.HasValue);
        }
    }
}