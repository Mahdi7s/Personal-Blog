using System;
using System.Linq;
using System.Web.Security;
using Mvc7S;
using SiteOfMe.DAL;

namespace SiteOfMe.Membership
{
    public sealed class UserRoleProvider : RoleProvider
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public UserRoleProvider()
        {
            
        }

        #region No Usage

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override string[] GetRolesForUser(string username)
        {
            var user = _unitOfWork.UserRep.GetByUserName(username);
            if (user == null) return new[] { "" };
            return user.Roles.Select(x => x.Name).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _unitOfWork.UserRep.IsInRole(username, roleName);
        }
    }
}