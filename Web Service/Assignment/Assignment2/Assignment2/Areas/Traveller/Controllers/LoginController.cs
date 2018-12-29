using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Assignment2.Utilities;
using Newtonsoft.Json;

namespace Assignment2.Areas.Traveller.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/traveller")]
    public class LoginController : ApiController
    {
        /*
         *  Login function pass data from view and get data from model
         */

        [System.Web.Http.Route("login")]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Login([FromBody] Traveler traveler)
        {
            using (Assignment2_ServicesContext db = new Assignment2_ServicesContext())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var v = db.Travelers.Where(a => a.email == traveler.email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(HashPassword.CreateMD5(traveler.password, v.salt), v.password) == 0 && v.IsEmailVerified == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, v);
                    }
                    else if (string.Compare(HashPassword.CreateMD5(traveler.password, v.salt), v.password) == 0 && v.IsEmailVerified == false)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email does not active. Please active this email.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Password doesn't match.");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email does not exist.");
                }   
            }
        }
    }
}