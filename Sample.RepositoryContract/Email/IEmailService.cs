using System.Net.Mail;

namespace Sample.ServiceContract.Email
{
    public interface IEmailService
    {
        /// <summary>
        /// Insert values in mail object.
        /// </summary>
        /// <param name="fromAdd"></param>
        /// <param name="toAdd"></param>
        /// <param name="subject"></param>
        /// <param name="text"></param>
        void MailObject(string fromAdd, string toAdd, string subject, string text);

        /// <summary>
        /// Send email at given address.
        /// </summary>
        /// <param name="objMailMessage"></param>
        void SendMail(MailMessage objMailMessage);
    }
}
