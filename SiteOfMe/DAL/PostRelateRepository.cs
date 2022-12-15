using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class PostRelateRepository : Repository<PostRelate>
    {
        private readonly SiteOfMeDbContext _siteOfMeDbContext;

        public PostRelateRepository(SiteOfMeDbContext dbContext) : base(dbContext)
        {
            _siteOfMeDbContext = dbContext;
        }

        public PostRelate GetRelateForPost(int postId)
        {
            return _dbSet.Single(x => x.PostId.Equals(postId));
        }

        public Post GetPrevPost(int postId)
        {
            var currentPostDate = _siteOfMeDbContext.Posts.Single(x => x.PostId.Equals(postId)).PublishDate;
            return _siteOfMeDbContext.Posts.OrderBy(x => x.PublishDate).FirstOrDefault(x => !x.PostId.Equals(postId) && x.PublishDate < currentPostDate);
        }

        public Post GetNextPost(int postId)
        {
            var currentPostDate = _siteOfMeDbContext.Posts.Single(x => x.PostId.Equals(postId)).PublishDate;
            return _siteOfMeDbContext.Posts.OrderByDescending(x => x.PublishDate).FirstOrDefault(x => !x.PostId.Equals(postId) && x.PublishDate > currentPostDate);
        }
    }
}