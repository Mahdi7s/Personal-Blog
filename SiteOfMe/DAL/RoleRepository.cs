using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class RoleRepository : Repository<Role>
    {
        private readonly SiteOfMeDbContext _siteOfMeDbContext;

        public RoleRepository(SiteOfMeDbContext context):base(context)
        {
            _siteOfMeDbContext = context;
        }

        public Role GetByName(string name)
        {
            return _siteOfMeDbContext.Roles.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsThereAnyInRole(string role)
        {
            return _dbSet.Any(x => x.Name.Equals(role, StringComparison.OrdinalIgnoreCase));
        }
    }
}