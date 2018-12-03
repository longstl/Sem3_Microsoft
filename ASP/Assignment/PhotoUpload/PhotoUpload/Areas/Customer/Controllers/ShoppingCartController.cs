using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PhotoUpload.Areas.Customer.Controllers
{
    public class ShoppingCartController : Controller
    {
        private PhotoUpload.Customer curCus = CurrentCustomerController.cus;
        private Price curPrice = new Price();
        AssignmentContext db = new AssignmentContext();
        // GET: Customer/ShoppingCart
        public int isExisting(int id)
        {
            List<Order_Detail> cart = (List<Order_Detail>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Image.id == id)
                    return i;
            return -1;
        }

        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Order_Detail> cart = (List<Order_Detail>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }

        public ActionResult OrderNow([FromUri]int id, [FromUri]int MaterialId, [FromUri]int SizeId)
        {
            var v = db.Prices.Where(x => x.Size_id == SizeId && x.Material_id == MaterialId).FirstOrDefault();
            curPrice = v;
            if (Session["cart"] == null)
            {
                List<Order_Detail> cart = new List<Order_Detail>();
                cart.Add(new Order_Detail(db.Images.Find(id), 1, v.price1, v.id));
                Session["cart"] = cart;
            }
            else
            {
                List<Order_Detail> cart = (List<Order_Detail>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Order_Detail(db.Images.Find(id), 1, v.price1, v.id));
                else
                    cart[index].quantity++;
                Session["cart"] = cart;
            }

            return View("Cart");
        }

        public ActionResult Checkout([FromUri] decimal totalPr, [FromUri] string name, [FromUri] string phone, [FromUri] string email, [FromUri] string emailTitle, [FromUri] string emailText, [FromUri] string address)
        {

            Order order = new Order();
            order.Customer_id = curCus.id;
            order.total_Price = totalPr;
            order.Employee_id = 1;
            order.shipName = name;
            order.shipAddress = address;
            order.shipPhone = phone;
            order.shipEmail = email;
            order.email_title = emailTitle;
            order.email_text = emailText;
            db.Orders.Add(order);
            db.SaveChanges();

            var ord = db.Orders.Where(x => x.Customer_id == curCus.id).ToList().OrderByDescending(i => i.id)
                .FirstOrDefault();
            if (ord != null)
            {
                Order_Detail orderDetail = new Order_Detail();
                List<Order_Detail> cart = (List<Order_Detail>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                {
                    orderDetail.Order_id = ord.id;
                    orderDetail.Image_id = cart[i].Image.id;
                    orderDetail.Price_id = cart[i].Price_id;
                    orderDetail.unitPrice = cart[i].unitPrice;
                    orderDetail.quantity = cart[i].quantity;
                    db.Order_Detail.Add(orderDetail);
                    db.SaveChanges();
                }
            }

            return View();
        }
    }
}