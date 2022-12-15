using System.ComponentModel.Composition;
using Mvc7S;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{
    public class DownFileRepository : Repository<DownFile>
    {
        public DownFileRepository(SiteOfMeDbContext dbContext):base(dbContext){}
    }
}