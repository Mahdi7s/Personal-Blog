using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Recaptcha;

namespace SiteOfMe.Utils
{
    public static class CustomHtmlHelpers
    {
        private static int _lastW8ColorIndex = 0;
        private static Random _random = new Random();
        private static string[,] _w8Colors = {{"blue","white"},
//{"blueLight","darken"},
{"blueDark", "white"},
{"green", "white"},
{"greenLight", "white"},
{"greenDark", "white"},
{"red", "white"},
{"yellow","white"},
{"orange","white"},
{"orangeDark", "white"},
{"pink", "white"},
{"pinkDark", "white"},
{"purple", "white"},
{"darken", "white"},
//{"lighten", "darken"},
//{"white", "darken"},
{"grayDark", "white"},
{"magenta", "white"},
{"teal", "white"},
{"redLight", "white"}
                      };


       public static MvcHtmlString GPluseOne(this HtmlHelper htmlHelper, string title, string url="")
       {
           var gPluseLink = new TagBuilder("div");
           gPluseLink.Attributes.Add("class", "g-plusone");
           gPluseLink.Attributes.Add("data-size", "small");

           if(!string.IsNullOrEmpty(url))
               gPluseLink.Attributes.Add("data-href", url);

           return new MvcHtmlString(gPluseLink.ToString(TagRenderMode.Normal));
       }

       public static string GenerateCaptcha(this HtmlHelper helper)
       {
           var captchaControl = new RecaptchaControl
           {
               ID = "recaptcha",
               Theme = "clean", //http://wiki.recaptcha.net/index.php/Theme
               PublicKey = ConfigurationManager.AppSettings["ReCaptchaPublicKey"],
               PrivateKey = ConfigurationManager.AppSettings["ReCaptchaPrivateKey"]
           };
           var htmlWriter = new HtmlTextWriter(new StringWriter());
           captchaControl.RenderControl(htmlWriter);
           return htmlWriter.InnerWriter.ToString();
       }

       public static string RandomW8BgColor(this HtmlHelper helper, bool generateNewColor = true)
       {
           return generateNewColor ? _w8Colors[_lastW8ColorIndex = _random.Next(0, _w8Colors.GetUpperBound(0)) + 1, 0] : _w8Colors[_lastW8ColorIndex, 0];
       }

       public static string RandomW8FgColor(this HtmlHelper helper)
       {
           return _w8Colors[_lastW8ColorIndex, 1];
       }

        public static string EncodeTitle(this HtmlHelper helper, string title)
        {
            var splits = title.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries).Select(x=>new string(x.Where(char.IsLetterOrDigit).ToArray())).Where(x=> !string.IsNullOrWhiteSpace(x));
            var nonEncodeTitle = String.Join("-", splits);

            return nonEncodeTitle;//HttpUtility.UrlEncode(nonEncodeTitle);
        }
    }
}