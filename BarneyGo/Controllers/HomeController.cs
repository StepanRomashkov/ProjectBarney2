using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarneyGo.Models;
using BarneyGo.DAL;

namespace BarneyGo.Controllers
{
    public class HomeController : Controller
    {
        private BarneyContext db = new BarneyContext();

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

        [Authorize]
        public ActionResult Login()
        {
            var curUserEmail = System.Security.Claims.ClaimsPrincipal.Current.Claims.ElementAt(4).Value;
            var curUserName = System.Security.Claims.ClaimsPrincipal.Current.Claims.ElementAt(3).Value;
            string role = "notFound";
            Admin currentAdmin = null;
            User currentUser = null;

            var connectedAdmin = db.Admins.AsEnumerable().ToList().Where(adm => adm.Email.Equals(curUserEmail));
            var connectedUser = db.Users.AsEnumerable().ToList().Where(u => u.Email.Equals(curUserEmail));
            if (connectedAdmin.Count() != 0)
            {
                currentAdmin = connectedAdmin.First();
                role = "Admin";
            }
            else if (connectedUser.Count() != 0)
            {
                currentUser = connectedUser.First();
                role = "Student";
            }
            else
            {
                TempData["denied"] = "Sorry, you are not one of us. Please, join ExperienceIT.";
                return RedirectToAction("LogOff", "Account");
            }

            ViewBag.Message = "Current user's email: " + curUserEmail + " | " +
                               "Role: " + role + " | Name: " + curUserName;
            if (role.Equals("Student"))
                return RedirectToAction("DisplayDay", currentUser);
            else
                return View();
        }

        public ActionResult DisplayDay(User user)
        {
            
            DateTime today = new DateTime(2017, 11, 16);
            var allDays = db.Days.AsEnumerable().ToList();
            var currentDay = allDays.Where(x => x.Date.CompareTo(today) >= 0);

            ViewBag.Message = "Let's get some data from localdb " + user.LastName;

            return View();
        }
    }
}