using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarneyGo.Models;

namespace BarneyGo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Here should be the Google Login page";

            return View();
        }

        public ActionResult DisplayDay()
        {
            ViewBag.Message = "Let's get some data from localdb";

            return View();
        }
    }
}