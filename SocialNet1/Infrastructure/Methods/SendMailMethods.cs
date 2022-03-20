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
        public static bool CheckEmail(string email) =>
            new Regex(@"\b[A-Za-z][A-Za-z0-9]{2,15}[@][a-z]{4,10}[.][a-z]{2,4}\b").IsMatch(email);

        public static async Task SendEmailAsync(string senderEmail, string SenderName, string senderPassword, string recicerEmail, string subj, string body)
        {
            var from = new MailAddress(senderEmail, SenderName);
            var to = new MailAddress(recicerEmail);
            var m = new MailMessage(from, to)
            {
                Subject = subj,
                Body = $"{body} \n{DateTime.Now:f}" //<p></p>
            };

            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
    }
}
