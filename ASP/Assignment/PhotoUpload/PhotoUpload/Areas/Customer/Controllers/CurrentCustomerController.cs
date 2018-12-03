using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace PhotoUpload.Areas.Customer.Controllers
{
    [Route("api/customer/getInfo")]
    public class CurrentCustomerController : ApiController
    {
        
        public static PhotoUpload.Customer cus = new PhotoUpload.Customer();
        public HttpResponseMessage Get(string email)
        {
            using (AssignmentContext db = new AssignmentContext())
            {
                var v = db.Customers.Where(cus => cus.email == email).FirstOrDefault();
                if (v == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        "Invalid Request!"));
                }
                cus = v;
                return Request.CreateResponse(HttpStatusCode.OK, cus);
            }
        }
    }
}