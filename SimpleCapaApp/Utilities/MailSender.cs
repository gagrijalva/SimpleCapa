using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApp.Utilities
{
    public class MailSender
    {
        public static void Send(string subject, string htmlBody, MailAddress to)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 26;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "localhost";
            mail.To.Add(to);
            mail.From = new MailAddress("mailer@simplecapa.com");
            mail.Subject = subject;
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;
            client.Send(mail);
        }

        internal static void Send(string v)
        {
            throw new NotImplementedException();
        }
    }
}