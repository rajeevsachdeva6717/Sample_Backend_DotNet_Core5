using Sample.ServiceContract.Email;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;

namespace Sample.Service.Email
{
    public class EmailService : BaseService, IEmailService
    {
        public EmailService(IConfiguration configuration)
            : base(null, configuration)
        {
        }

        /// <summary>
        /// Insert values in mail object.
        /// </summary>
        /// <param name="fromAdd"></param>
        /// <param name="toAdd"></param>
        /// <param name="subject"></param>
        /// <param name="text"></param>
        public void MailObject(string fromAdd, string toAdd, string subject, string text)
        {
            MailMessage objectMailMessage = new MailMessage
            {
                From = new MailAddress(fromAdd)
            };

            objectMailMessage.To.Add(toAdd);
            objectMailMessage.Subject = subject;
            objectMailMessage.Body = text;
            objectMailMessage.IsBodyHtml = true;
            objectMailMessage.Priority = MailPriority.High;

            SendMail(objectMailMessage);
        }

        /// <summary>
        /// Send email at given address.
        /// </summary>
        /// <param name="objectMailMessage"></param>
        public void SendMail(MailMessage objectMailMessage)
        {
            System.Net.Mail.SmtpClient objSmtpServer = new System.Net.Mail.SmtpClient(EmailSetting.MailServer)
            {
                Credentials = new System.Net.NetworkCredential(EmailSetting.UserId, EmailSetting.Password),
                Port = Convert.ToInt32(EmailSetting.SMTPPort)
            };

            objSmtpServer.EnableSsl = true;
            objSmtpServer.Send(objectMailMessage);
        }
    }
}
