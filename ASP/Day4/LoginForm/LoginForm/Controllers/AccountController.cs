using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LoginForm.Controllers
{
    public class AccountController : Controller
    {
        AccountContext db = new AccountContext();

        // GET: Account
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            var account1 = db.Accounts.Single(acc => acc.username == account.username && acc.password == account.password);
            if (account1 != null)
            {
                return Redirect("Index");
            }
            else
            {
                return View();
            }
        }
    }
}