using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;
using System.ComponentModel.Composition;
using SiteOfMe.Utils;

namespace SiteOfMe.DAL
{
    public class UserRepository : Repository<User>
    {
        private readonly SiteOfMeDbContext _siteOfMeDbContext;

        public UserRepository(SiteOfMeDbContext dbContext)
            : base(dbContext)
        {
            _siteOfMeDbContext = dbContext;
        }

        public override void Insert(User entity)
        {
            entity.Password = Hasher.HashOf(entity.Password);
            base.Insert(entity);
        }

        public User GetByUserName(string username)
        {
            return _siteOfMeDbContext.Users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User GetCurrentUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                return GetByUserName(HttpContext.Current.User.Identity.Name);
            else return null;
        }
        
        public IEnumerable<User> GetUsersInRole(string role)
        {
            return
                _siteOfMeDbContext.Users.Include("Roles").Where(
                    x => x.Roles.Any(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        public bool CanLogin(string username,string password)
        {
            var user = GetByUserName(username);
            if(user != null && !string.IsNullOrEmpty(user.Password))
            {
                return Hasher.VerifyPass(password, user.Password);
            }
            return false;
        }

        public bool IsInRole(string username, string roleName)
        {
            var user = GetByUserName(username);
            if (user != null)
                return user.Roles.Any(x => x.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            return false;
        }
    }
}