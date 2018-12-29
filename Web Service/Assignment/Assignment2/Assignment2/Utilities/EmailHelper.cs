using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Utilities
{
    public class EmailHelper
    {
        /*
         * Config Email
         */
        public static readonly string BASE_URL = "http://localhost:27489/";
        public static readonly string EMAIL_SENDER = "daokhanhblog942@gmail.com";
        public static readonly string EMAIL_PASSWORD = "cqzwqzzlcayokple"; // using application password Google
        public static readonly string SMTP_CLIENT = "smtp.gmail.com";
        public static readonly string DISPLAY_NAME_VERIFY = "Activation Username";
        public static readonly string SUBJECT_VERIFY = "Your account is successfully created!";
        public static readonly int SMTP_PORT = 587;

        /*
         * Sending the message to email ( Active code )
         */
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode)
        {
            var link = BASE_URL + "Travelers/Register/VerifyAccount/" + activationCode;

            var fromEmail = new MailAddress(EMAIL_SENDER, DISPLAY_NAME_VERIFY);
            var toEmail = new MailAddress(email);
            var fromEmailPassword = EMAIL_PASSWORD;
            string subject = SUBJECT_VERIFY;

            string body = "<br/><br/>We are excited to tell you that your account is" +
                          " successfully created. Please click on the below link to verify your account" +
                          " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = SMTP_CLIENT,
                Port = SMTP_PORT,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}