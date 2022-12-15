using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SiteOfMe.Utils
{
    public static class MailClient
    {
        private static readonly SmtpClient Client;

        static MailClient()
        {
            Client = new SmtpClient
                         {
                             Host = ConfigurationManager.AppSettings["smtpHost"],
                             Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]),
                             DeliveryMethod = SmtpDeliveryMethod.Network
                         };
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"],
                                                       ConfigurationManager.AppSettings["smtpPass"]);
        }

        public static bool SendMessage(string from, string to, string subject, string body)
        {
            using (var mailMsg = new MailMessage(from, to, subject, body))
            {
                try
                {
                    mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    Client.Send(mailMsg);
                    return true;
                }
                catch (Exception exception)
                {
                    // hey we need logger !
                    throw exception;
                }
            }
        }

        public static bool SendWelcomeMail(string email)
        {
            return SendMessage(ConfigurationManager.AppSettings["noReplyEmail"], email, "", "");
        }
    }
}