using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Assignment2.Areas.Traveller.Controllers
{
    [RoutePrefix("api/v1")]
    public class GetCurrentUserController : ApiController
    {
        Assignment2_ServicesContext db = new Assignment2_ServicesContext();
        [Route("GetCurrentUser")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri] string email)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var v = db.Travelers.Where(a => a.email == email).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, v);
        }
    }
}
