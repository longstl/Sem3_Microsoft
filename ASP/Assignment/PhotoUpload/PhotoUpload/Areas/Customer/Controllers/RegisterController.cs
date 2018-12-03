using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PhotoUpload.Utilities;

namespace PhotoUpload.Areas.Customer.Controllers
{
    public class RegisterController : Controller
    {
        /*
         * Register Function ( Get Method - Show View)
         */
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /*
         * Register Function ( Post Method - Get data form View - do with Model )
         * check exist email or username
         * verify email
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IsEmailVerified,ActivationCode")] PhotoUpload.Customer cus)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                #region //Email is already Exist 

                var isExistEmail = IsEmailExist(cus.email);
                var isExistUsername = IsUsernameExist(cus.username);
                if (isExistEmail)
                {
                    ModelState.AddModelError(cus.email, "Email already exist");
                    return View(cus);
                }
                else if(isExistUsername)
                {
                    ModelState.AddModelError(cus.username, "Username already exist");
                    return View(cus);
                }
                #endregion

                #region Generate Activation Code 

                cus.ActivationCode = Guid.NewGuid();

                #endregion

                #region  Password Hashing

                cus.salt = GenerateSalt.saltStr(10);
                cus.password = HashPassword.CreateMD5(cus.password, cus.salt);

                #endregion

                cus.IsEmailVerified = false;

                #region Save to Database

                using (AssignmentContext db = new AssignmentContext())
                {
                    db.Customers.Add(cus);
                    db.SaveChanges();
                }

                //Send Email to User
                SendVerificationLinkEmail(cus.email, cus.ActivationCode.ToString());
                message = "Registration successfully done. Account activation link " +
                          " has been sent to your email id:" + cus.email;
                Status = true;

                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(cus);
        }

        /*
         * Check exist email
         */
        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (AssignmentContext db = new AssignmentContext())
            {
                var v = db.Customers.Where(a => a.email == email).FirstOrDefault();
                return v != null;
            }
        }

        /*
         * Check exist username
         */
        [NonAction]
        public bool IsUsernameExist(string username)
        {
            using (AssignmentContext db = new AssignmentContext())
            {
                var v = db.Customers.Where(a => a.username == username).FirstOrDefault();
                return v != null;
            }
        }

        /*
         * Sending the message to email ( Active code )
         */
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode)
        {
            var verifyUrl = "/Customer/Register/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

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
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (AssignmentContext db = new AssignmentContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false; 
                var v = db.Customers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    v.status = 1;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }
    }
}