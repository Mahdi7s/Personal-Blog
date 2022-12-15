using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;
using System.ComponentModel.Composition;

namespace SiteOfMe.DAL
{
    public class PostRepository : Repository<Post>
    {
        private readonly SiteOfMeDbContext _siteOfMeDbContext;
        public PostRepository(SiteOfMeDbContext siteOfMeDbContext):base(siteOfMeDbContext)
        {
            _siteOfMeDbContext = siteOfMeDbContext;
        }

        public override void Insert(Post entity)
        {
            if (entity.Body == null || string.IsNullOrEmpty(entity.Body.Value))
            {
                var nBody = new BigString { Value = "متن مطلب" };
                _siteOfMeDbContext.BigStrings.Add(nBody);
                _siteOfMeDbContext.SaveChanges();

                entity.Body = nBody;                
            }
            if (entity.BodySummary == null || string.IsNullOrEmpty(entity.BodySummary.Value))
            {
                var nBSummary = new BigString { Value = "خلاصه متن" };
                _siteOfMeDbContext.BigStrings.Add(nBSummary);
                _siteOfMeDbContext.SaveChanges();

                entity.BodySummary = nBSummary;
            }
            if(entity.PostRelates.IsNullOrEmpty())
            {
                entity.PostRelates = new List<PostRelate>();
            }

            base.Insert(entity);
        }

        public override void Update(Post entityToUpdate)
        {
            _siteOfMeDbContext.Entry(entityToUpdate.Body).State = EntityState.Modified;
            _siteOfMeDbContext.Entry(entityToUpdate.BodySummary).State = EntityState.Modified;

            base.Update(entityToUpdate);
        }

        public IQueryable<Post> GetPublishedPosts(string key, string category)
        {
            var resQuery = _dbSet.Where(x => x.IsPublished)/*.Include(x=>x.User).Include(x=>x.BodySummary)*/;
            
            if (!string.IsNullOrEmpty(key))
            {
                category = string.IsNullOrWhiteSpace(category) ? "all" : category;
                //var keys = key.Split(new[] {' '});

                //Expression<Func<Post, bool>> titleCondition = x => keys.Any(k => x.Title.Contains(k));
                //Expression<Func<Post, bool>> tagCondition = x => keys.Any(k => x.Tags.Any(t => t.Name.Contains(k)));
                //Expression<Func<Post, bool>> bodyCondition = x => keys.Any(k => x.Body.Value.Contains(k));

                switch (category)
                {                    
                    case "title":
                        resQuery = resQuery.Where(x=>x.Title.Contains(key));
                        break;
                    case "tag":
                        resQuery = resQuery.Where(x=>x.Tags.Any(t=>t.Name.Equals(key, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "body":
                        resQuery = resQuery.Where(x=>x.Body.Value.Contains(key));
                        break;
                    case "all":
                        resQuery = resQuery.Where(x => x.Title.Contains(key) || x.Tags.Any(t => t.Name.Equals(key, StringComparison.OrdinalIgnoreCase)) || x.Body.Value.Contains(key));
                        break;
                }
            }
            return resQuery.OrderByDescending(x => x.PublishDate);
        }
    }
}