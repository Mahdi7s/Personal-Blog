using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SiteOfMe.Utils
{
    public class VisitorIP
    {
        public static string GetIP()
        {
            var request = HttpContext.Current.Request;
            var szRemoteAddr = request.UserHostAddress; // EqualsTo -> request.ServerVariables["REMOTE_ADDR"]
            var szXForwardedFor = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var szIP = string.Empty;

            if(string.IsNullOrEmpty(szXForwardedFor))
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
                if(szIP.IndexOf(',') > 0)
                {
                    return szIP.Split(new[] {','}).First();
                    //foreach (var ip in szIP.Split(new[]{','}))
                    //{
                    //    if (!IsPrivateIP(ip))
                    //        return ip;
                    //}
                }
            }
            return szIP;
        }
    }
}