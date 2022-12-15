using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Mvc7S;
using System.Data.Entity.Validation;
using SiteOfMe.Utils;
using System.Data.Entity.Migrations;

namespace SiteOfMe.Models
{
    internal class OnlyWorkInitializer : IDatabaseInitializer<SiteOfMeDbContext>
    {

        public void InitializeDatabase(SiteOfMeDbContext context)
        {
            
        }
        
    }

    internal class CreateDatabaseIfNotExistsInitializer : CreateDatabaseIfNotExists<SiteOfMeDbContext>
    {
        protected override void Seed(SiteOfMeDbContext context)
        {
            DbInitializeHelper.Seed(context);
            
            base.Seed(context);
        }
    }

    internal class DropCreateDatabaseIfModelChangesInitializer : DropCreateDatabaseIfModelChanges<SiteOfMeDbContext>
    {
        protected override void Seed(SiteOfMeDbContext context)
        {
            DbInitializeHelper.Seed(context);

            base.Seed(context);
        }        
    }

    internal class DropCreateDatabaseAlwaysInitializer : DropCreateDatabaseAlways<SiteOfMeDbContext>
    {
        protected override void Seed(SiteOfMeDbContext context)
        {
            DbInitializeHelper.Seed(context);

            base.Seed(context);
        }        
    }

    internal static class DbInitializeHelper
    {
        public static void Seed(SiteOfMeDbContext context)
        {
            try
            {
                InitializeRoles(context);
                InitializeTags(context);

                context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                var msg = string.Empty;
                foreach (var dbEntityValidationResult in exception.EntityValidationErrors)
                {
                    foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        msg += string.Format("Property: {0}\tError: {1}", dbValidationError.PropertyName,
                                             dbValidationError.ErrorMessage);
                    }
                }
                Debug.Fail(msg);
            }
            catch(Exception exception)
            {
                Debug.Fail(exception.Message);
            }
        }

        private static void InitializeTags(SiteOfMeDbContext context)
        {
            var tags = new[]
                           {
                               "Asp.Net", "Asp.Net MVC", "Sql Server", "WPF", "Silverlight", "XAML", "Objective-C", "XNA",
                               "Cocos2D", "JavaScript", "JQuery"
                           };
            tags.Apply(x => context.Tags.Add(new Tag {Name = x}));
        }

        private static void InitializeRoles(SiteOfMeDbContext context)
        {
            var roles = new[] {"Admin", "User"};

            var adminRole = context.Roles.Add(new Role {Name = roles[0]});
            var userRole = context.Roles.Add(new Role {Name = roles[1]});
            context.SaveChanges();

            var admin = context.Users.Add(new User
                                              {
                                                  UserName = "Mahdi7s",
                                                  DisplayName = "Mahdi7$",
                                                  Email = "mahdi7s@ymail.com",
                                                  Password = Hasher.HashOf("mwww.vmahdi7s.wcom"),
                                                  About = (BigString) "Be With me & think different !"
                                              });

            admin.PasswordConfirm = admin.Password;

            admin.Roles = new List<Role>(new[] {adminRole});
        }
    }
}