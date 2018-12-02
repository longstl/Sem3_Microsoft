using LINQASP.MVC5.Context;
using LINQASP.MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQASP.MVC5.Controllers
{
    public class ProductController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Product
        public ActionResult Index()
        {
            var products = db.products.ToList();
            return View(products);
        }

        public ActionResult OrderByName()
        {
            var products = from p in db.products
                           orderby p.Name ascending
                           select p;
            return View(products);
        }

        public ActionResult OrderByPrice()
        {
            var products = from p in db.products
                           orderby p.Price ascending
                           select p;
            return View(products);
        }
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
