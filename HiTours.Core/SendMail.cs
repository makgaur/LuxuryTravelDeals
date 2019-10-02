// <copyright file="SendMail.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// SendMail
    /// </summary>
    public class SendMail
    {
        /// <summary>
        /// Mails the send.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="emailto">The emailto.</param>
        /// <param name="bcc">The BCC.</param>
        /// /// <returns>
        /// bool
        /// </returns>
        public static bool MailSend(string subject, string body, string emailto, string bcc = "")
        {
            try
            {
                var client = new SmtpClient(Constants.SmtpClient)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Constants.NetworkCredentialEmail, Constants.NetworkCredentialPwd)
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(Constants.EmailFrom)
                };
                mailMessage.To.Add(emailto);

            if (!string.IsNullOrEmpty(bcc))
            {
                mailMessage.Bcc.Add(bcc);
            }

                client.EnableSsl = true;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Port = 587;
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                string message = e.Message + " - " + e.StackTrace;
                return false;
            }
        }
    }
}