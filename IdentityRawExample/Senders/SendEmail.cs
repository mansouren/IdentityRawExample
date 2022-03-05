using IdentityRawExample.Infra;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentityRawExample.Senders
{
    public class SendEmail : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        public SendEmail(IOptionsSnapshot<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }
        //public  Task SendAsync(string to,string subject,string body)
        //{
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient(emailSettings.Host);
        //    mail.From = new MailAddress(emailSettings.From, "لاتین مدیا");
        //    mail.To.Add(to);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;

        //    //System.Net.Mail.Attachment attachment;
        //    // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
        //    // mail.Attachments.Add(attachment);

        //    SmtpServer.Port = emailSettings.Port;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential(emailSettings.UserName, emailSettings.Password);
        //    SmtpServer.EnableSsl = true;
        //    //SmtpServer.UseDefaultCredentials = false;

        //    try
        //    {
        //        using (SmtpServer)
        //        {
        //            SmtpServer.Timeout = 20000;
        //            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            SmtpServer.Send(mail);
        //        }
               

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Task.FromResult(0);
        //}

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(emailSettings.Host);
            mail.From = new MailAddress(emailSettings.From, "My WebSite");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = emailSettings.Port; 
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailSettings.UserName, emailSettings.Password);
            SmtpServer.EnableSsl = true;
            

            try
            {
                using (SmtpServer)
                {
                    SmtpServer.Timeout = 20000;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Send(mail);
                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Task.FromResult(0);
        }
    }
}
