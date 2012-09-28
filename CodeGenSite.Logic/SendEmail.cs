using System;
using System.Net.Mail;

namespace CodeGenSite.Logic
{
    public class SendEmail
    {
        /// <summary>
        /// 1 = not ready
        /// 2 = successful
        /// 3 = failed
        /// </summary>
        public int SentSuccessfully { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmail"/> class.
        /// </summary>
        /// <param name="fromAddress">From address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="description">The description.</param>
        /// <param name="name">The name.</param>
        public SendEmail(string fromAddress, string subject, string description, string name)
        {
            string emailContents = BuildEmail(subject, description, name);

            SendEmailFromSite(new MailAddress(fromAddress), new MailAddress("deanhume@gmail.com"), subject, emailContents);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="fromAddress">From address.</param>
        /// <param name="toAddress">To address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        private void SendEmailFromSite(MailAddress fromAddress, MailAddress toAddress, string subject, string body)
        {
            #if (!DEBUG)
            try
            {
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                var client = new SmtpClient("mail.gandi.net");
                client.Credentials = new System.Net.NetworkCredential("contact@codegenerate.me", "404bollocks");
                client.Send(message);

                SentSuccessfully = 2;
            }
            catch (Exception)
            {
                SentSuccessfully = 3;
            }
            #endif

            #if (DEBUG)
                SentSuccessfully = 2;
            #endif
        }

        /// <summary>
        /// Builds the email content.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="description">The description.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private string BuildEmail(string subject, string description, string name)
        {
            // Build up the body
            string body = "<h1>";
            body += subject;
            body += "</h1>";
            body += "<br/>";

            // From
            body += "<h3>";
            body += name;
            body += "</h3>";

            // Description
            body += "<br/>";
            body += "<h2>Description</h2>";
            body += description;
            body += "<br/>";
            body += DateTime.Now;
            
            return body;
        }
    }
}
