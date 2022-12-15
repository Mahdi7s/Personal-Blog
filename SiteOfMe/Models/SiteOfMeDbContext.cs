using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SiteOfMe.Models
{
    public class SiteOfMeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRelate> PostRelates { get; set; } 
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DownFile> DownFiles { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<BigString> BigStrings { get; set; }
        public DbSet<AnonymousUser> AnonymousUsers { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Appendix> Appendices { get; set; }
        public DbSet<RelatedLink> RelatedLinks { get; set; }
        
        public SiteOfMeDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}