using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using PhotoUpload.Utilities;

namespace PhotoUpload.Areas.Customer.Controllers
{
    public class LoginController : Controller
    { 

        // GET: Customer/Login
        /*
         * Login Function
         */
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /*
         *  Login function pass data from view and get data from model
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PhotoUpload.Customer cus, string ReturnUrl = "")
        {
            string message = "";
            using (AssignmentContext db = new AssignmentContext())
            {
                var v = db.Customers.Where(a => a.email == cus.email).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }
                    if (string.Compare(HashPassword.CreateMD5(cus.password, v.salt), v.password) == 0)
                    {
                        if (v.status == 1)
                        {
                            var ticket = new FormsAuthenticationTicket(cus.email, true, 525600);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(525600);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);
                            
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Login");
                            }
                        }
                        else if (v.status == 0 && v.IsEmailVerified == false)
                        {
                            message = "Your account can't active. Please active it.";
                        }
                        else if (v.status == -1)
                        {
                            message = "Your account has been ban.";
                        }  
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }

                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        /*
         * Logout function
         */
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}