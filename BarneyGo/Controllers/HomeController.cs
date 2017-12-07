using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BarneyGo.DAL;
using BarneyGo.Models;

namespace BarneyGo.Controllers
{
    public class HomeController : Controller
    {
        private BarneyContext db = new BarneyContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProgramOverview()
        {
            return View();
        }

        public ActionResult Contact()
        {
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

            if (role.Equals("Admin"))
            {
                TempData["role"] = "Admin";
                return RedirectToAction("Index", currentAdmin);
            }
            else if (role.Equals("Student"))
            {
                TempData["role"] = "Student";
                return RedirectToAction("DisplayDay", currentUser);
            }
            else
                return View();
        }

        public ActionResult DisplayDay()
        {
            User currentUser = GetUserByEmail(System.Security.Claims.ClaimsPrincipal.Current.Claims.ElementAt(4).Value);
            DateTime today = new DateTime(2017, 11, 16);
            var allDays = GetCurrentUserDays(currentUser);
            DateTime upcomingDay = allDays.Where(x => x.Date.CompareTo(today) >= 0).Min(day => day.Date);

            ViewBag.currentDay = allDays.Where(x => x.Date.Equals(upcomingDay)).SingleOrDefault();
            ViewBag.today = today.ToShortDateString();
            ViewBag.userName = currentUser.FirstName + " " + currentUser.LastName;
            ViewBag.userId = currentUser.Id;

            TempData["role"] = "Student";
            return View(currentUser);
        }

        public ActionResult LearningPlan()
        {

            User currentUser = GetUserByEmail(System.Security.Claims.ClaimsPrincipal.Current.Claims.ElementAt(4).Value);
            var days = GetCurrentUserDays(currentUser).ToList();
            days.Sort(delegate (Day x, Day y)
            {
                if (x.Date == null && y.Date == null) return 0;
                else if (x.Date == null) return -1;
                else if (y.Date == null) return 1;
                else return x.Date.CompareTo(y.Date);
            });
            ViewBag.days = days;
            TempData["role"] = "Student";
            return View(days);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.day = db.Days.Find(id);
            if (ViewBag.day == null)
            {
                return HttpNotFound();
            }
            TempData["role"] = "Student";
            return View();
        }


        User GetCurrentUser (int id)
        {
            return db.Users.Where(u => u.Id == id).SingleOrDefault();
        }

        User GetUserByEmail (string email)
        {
            return db.Users.Where(u => u.Email.Equals(email)).SingleOrDefault();
        }

        IQueryable<Day> GetCurrentUserDays (User user)
        {
            return db.Days.Include(d => d.Syllabus).Where(d => d.SyllabusId == user.SyllabusId);
        }

    }
}