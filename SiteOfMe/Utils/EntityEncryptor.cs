using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Mvc7S;

namespace SiteOfMe.Utils
{
    public static class EntityEncryptor
    {
        public static void Encrypt<T>(T entity)
        {
            var entityType = entity.GetType();
            var strType = typeof (string);

            entityType.GetProperties().Where(x => x.PropertyType == strType).Apply(x =>
                                                                                       {
                                                                                           var propVal =
                                                                                               entityType.InvokeMember(
                                                                                                   x.Name,
                                                                                                   BindingFlags.
                                                                                                       GetProperty, null,
                                                                                                   entity, null) as
                                                                                               string;
                                                                                           propVal = EncryptData(propVal);
                                                                                           entityType.InvokeMember(
                                                                                               x.Name,
                                                                                               BindingFlags.SetProperty,
                                                                                               null, entity,
                                                                                               new[] {propVal});
                                                                                       });
        }

        private static string EncryptData(string propVal)
        {
            if(string.IsNullOrEmpty(propVal))
                return string.Empty;
            return propVal;
        }
    }
}