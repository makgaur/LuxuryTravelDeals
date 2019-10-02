// <copyright file="Utils.cs" company="Tetraskelion Softwares Pvt. Ltd.">
// Copyright (c) Tetraskelion Softwares Pvt. Ltd. All rights reserved.
// </copyright>

namespace EnquiryForm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Utils
    /// </summary>
    public class MailSendHelper
    {
        /// <summary>
        /// Occurs when [email sent].
        /// </summary>
        public static event EventHandler<EventArgs> EmailSent;

        /// <summary>
        /// Occurs when [email failed].
        /// </summary>
        public static event EventHandler<EventArgs> EmailFailed;

        /// <summary>
        /// Occurs when [on log].
        /// </summary>
        public static event EventHandler<EventArgs> OnLog;

        /// <summary>
        /// Sends the mail message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="smtpServer">The SMTP server.</param>
        /// <param name="smtpServerPort">The SMTP server port.</param>
        /// <param name="smtpUserName">Name of the SMTP user.</param>
        /// <param name="smtpPassword">The SMTP password.</param>
        /// <param name="enableSsl">The enable SSL.</param>
        /// <returns>SendMailMessage</returns>
        /// <exception cref="ArgumentNullException">message</exception>
        public static string SendMailMessage(MailMessage message, string smtpServer = "", string smtpServerPort = "", string smtpUserName = "", string smtpPassword = "", string enableSsl = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool enableSsl2 = true;
            int port = 587;
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            try
            {
                if (string.IsNullOrEmpty(smtpServer))
                {
                    smtpServer = "smtp.gmail.com";
                }

                if (string.IsNullOrEmpty(smtpUserName))
                {
                    smtpUserName = "samjohn491@gmail.com";
                }

                if (string.IsNullOrEmpty(smtpPassword))
                {
                    smtpPassword = "vinita12";
                }

                if (!string.IsNullOrEmpty(smtpServerPort))
                {
                    port = int.Parse(smtpServerPort);
                }

                if (!string.IsNullOrEmpty(enableSsl))
                {
                    bool.TryParse(enableSsl, out enableSsl2);
                }

                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                if (!string.IsNullOrEmpty(smtpUserName))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                }

                smtpClient.Port = port;
                smtpClient.EnableSsl = enableSsl2;
                smtpClient.Send(message);
                Utils.OnEmailSent(message);
            }
            catch (Exception ex2)
            {
                Utils.OnEmailFailed(message);
                stringBuilder.Append("Error sending email in SendMailMessage: ");
                for (Exception ex = ex2; ex != null; ex = ex.InnerException)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(" ");
                    }

                    stringBuilder.Append(ex.Message);
                }

                Utils.Log(stringBuilder.ToString(), new object[0]);
            }
            finally
            {
                message.Dispose();
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Sends the mail message asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void SendMailMessageAsync(MailMessage message)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Utils.SendMailMessage(message, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            });
        }

        /// <summary>
        /// Logs the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Log(string format, params object[] args)
        {
            if (Utils.OnLog != null)
            {
                Utils.OnLog(string.Format(format, args), EventArgs.Empty);
            }
        }

        private static void OnEmailFailed(MailMessage message)
        {
            if (Utils.EmailFailed != null)
            {
                Utils.EmailFailed(message, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [email sent].
        /// </summary>
        /// <param name="message">The message.</param>
        private static void OnEmailSent(MailMessage message)
        {
            if (Utils.EmailSent != null)
            {
                Utils.EmailSent(message, EventArgs.Empty);
            }
        }
    }
}