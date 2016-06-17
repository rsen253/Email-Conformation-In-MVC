using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace EmailConfirmation.ClassHelper
{
    public class ManageEmailService
    {
        public async static Task SendingEmailAsync(string Email, string Subject, string message)
        {
            try
            {
                var _email = "rahulsen1992@hotmail.com";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _displayName = "Rahul";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(Email);
                myMessage.From = new MailAddress(_email, _displayName);
                myMessage.Subject = Subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}