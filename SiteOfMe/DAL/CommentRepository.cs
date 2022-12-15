using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class CommentRepository : Repository<Comment>
    {
        private readonly SiteOfMeDbContext _siteOfMeDbContext;

        public CommentRepository(SiteOfMeDbContext dbContext)
            : base(dbContext)
        {
            _siteOfMeDbContext = dbContext;
        }

        public override Comment GetByID(object id)
        {
            return Get(x => x.CommentId.Equals((int) id), includeProperties: "Body,Reply.Body").FirstOrDefault();
        }

        public override void Insert(Comment entity)
        {
            base.Insert(entity);
        }

        public int GetCommentsCount(int postId)
        {
            return _siteOfMeDbContext.Comments.Count(x => x.PostId.Equals(postId) && (!x.IsReply) && x.IsVisible);
        }

        public IEnumerable<Comment> GetInVisibleComments()
        {
            return _siteOfMeDbContext.Comments.Where(x => !x.IsVisible).ToList();
        }
    }
}