using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace PhotoUpload.Areas.Customer.Controllers
{
    public class ImageController : Controller
    {
        // GET: Customer/Image
        public ActionResult Index()
        {
            PhotoUpload.Customer currentCus = CurrentCustomerController.cus;
            using (AssignmentContext db = new AssignmentContext())
            {
                if (db.Images.Where(a => a.Customer_id == currentCus.id).ToList() == null)
                {
                    ViewBag.Message = "Library Empty";
                    return View();
                }
                else
                {
                    return View(db.Images.Where(a => a.Customer_id == currentCus.id).ToList());
                } 
            }  
        }
    }
}