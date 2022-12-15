using System.Web.Mvc;
using DataAnnotationsExtensions.ClientValidation;
using SiteOfMe.Utils;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SiteOfMe.App_Start.RegisterClientValidationExtensions), "Start")]
namespace SiteOfMe.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            ModelMetadataProviders.Current = new FaDataAnnotationsModelMetadataProvider(typeof(Resources.FaDataModelMetadata));
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}