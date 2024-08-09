using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BKTra.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckLogin(string username, string password)
        {
            if (username == "admin" && password == "123456")
            {
                Session["user"] = username;
                return RedirectToAction("Index", "Home");
            }
            if (username == "")
                ViewBag.msg = "Username cannot be empty.";
            else
                ViewBag.msg = "Invalid username or password. ";
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return View("Index");
        }
    }
}