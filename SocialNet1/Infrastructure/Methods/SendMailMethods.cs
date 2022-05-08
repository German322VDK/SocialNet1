using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Methods
{
    public static class SendMailMethods
    {
        //public static bool CheckEmail(string email) =>
        //    new Regex(@"\b[A-Za-z][A-Za-z0-9]{2,15}[@][a-z]{4,10}[.][a-z]{2,4}\b").IsMatch(email);

        public static bool CheckEmail(string email) =>
          new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").IsMatch(email);

        //public static bool CheckEmail(string email) =>
        //   new Regex(@"[a-z0-9!#${'$'}%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#${'$'}%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\]").IsMatch(email);

        public static async Task<bool> SendEmailAsync(string senderEmail, string SenderName, string senderPassword, string recicerEmail, string subj, string body)
        {
            try
            {
                var from = new MailAddress(senderEmail, SenderName);
                var to = new MailAddress(recicerEmail);
                var m = new MailMessage(from, to)
                {
                    Subject = subj,
                    Body = $"<div> <p>{body}</p> \n{DateTime.Now:f}</div>" //<p></p>
                };
                m.IsBodyHtml = true;
                var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
