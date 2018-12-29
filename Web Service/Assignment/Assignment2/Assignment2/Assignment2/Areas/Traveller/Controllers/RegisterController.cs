using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Assignment2.Utilities;
using Newtonsoft.Json;

namespace Assignment2.Areas.Traveller.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/traveller")]
    public class RegisterController : ApiController
    {
        private readonly string BASE_URL = "http://localhost:27489/";

        [System.Web.Http.Route("register")]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Register([Bind(Exclude = "IsEmailVerified,ActivationCode")] [FromBody] Traveler traveler)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                #region //Email is already Exist 

                var isExistEmail = IsEmailExist(traveler.email);

                if (isExistEmail)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email already exist.");
                }

                #endregion

                #region Generate Activation Code 

                traveler.ActivationCode = Guid.NewGuid();

                #endregion

                #region  Password Hashing

                traveler.salt = GenerateSalt.saltStr(10);
                traveler.password = HashPassword.CreateMD5(traveler.password, traveler.salt);

                #endregion

                traveler.IsEmailVerified = false;

                #region Save to Database

                using (Assignment2_ServicesContext db = new Assignment2_ServicesContext())
                {
                    traveler.Role_id = 1;
                    traveler.createdAt = DateTime.Now;
                    traveler.updatedAt = DateTime.Now;
                    Debug.WriteLine(JsonConvert.SerializeObject(traveler));
                    db.Travelers.Add(traveler);
                    db.SaveChanges();
                }

                //Send Email to User
                SendVerificationLinkEmail(traveler.email, traveler.ActivationCode.ToString());
                message = "Registration successfully done. Account activation link " +
                          " has been sent to your email id:" + traveler.email;
                return Request.CreateResponse(HttpStatusCode.OK, traveler);
                #endregion
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /*
         * Check exist email
         */
        [System.Web.Http.NonAction]
        private bool IsEmailExist(string email)
        {
            using (Assignment2_ServicesContext db = new Assignment2_ServicesContext())
            {
                var v = db.Travelers.Where(a => a.email == email).FirstOrDefault();
                return v != null;
            }
        }

        /*
         * Sending the message to email ( Active code )
         */
        [System.Web.Http.NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode)
        {
            var link = BASE_URL + "api/v1/traveller/verifyAccount?id=" + activationCode;

            var fromEmail = new MailAddress("daokhanhblog942@gmail.com", "Activation Username");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "cqzwqzzlcayokple";
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your account is" +
                          " successfully created. Please click on the below link to verify your account" +
                          " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
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

        /*
         * Verify email
         */
        [System.Web.Http.Route("verifyAccount")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage VerifyAccount(string id)
        {
            using (Assignment2_ServicesContext db = new Assignment2_ServicesContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var v = db.Travelers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Active Success");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Request.");
                }
            }
        }
    }
}
