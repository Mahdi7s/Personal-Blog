using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;

namespace SiteOfMe.Utils
{
    public class FaDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly Type _resourceType;
        public FaDataAnnotationsModelMetadataProvider(Type resourceType)
        {
            _resourceType = resourceType;
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            foreach (var att in attributes)
            {
                var vAtt = att as ValidationAttribute;
                if (vAtt != null)
                {
                    var resKey = vAtt.GetType().Name;
                    if (_resourceType.GetProperty(resKey, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance) != null)
                    {
                        vAtt.ErrorMessage = null;
                        vAtt.ErrorMessageResourceType = _resourceType;
                        vAtt.ErrorMessageResourceName = resKey;
                    }
                    else
                    {
                        //Debug.Fail("there is not any property of name: " + resKey);
                    }
                }
            }

            return modelMetadata;
        }
    }
}