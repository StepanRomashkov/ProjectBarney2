using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BarneyGo.DAL;
using BarneyGo.Models;

namespace BarneyGo.Controllers
{
    public class DaysController : Controller
    {
        private BarneyContext db = new BarneyContext();

        // GET: Days
        public ActionResult Index()
        {
            var days = db.Days.Include(d => d.Syllabus);
            return View(days.ToList());
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // GET: Days/Create
        public ActionResult Create()
        {
            ViewBag.SyllabusId = new SelectList(db.Syllabuses, "Id", "Name");
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,SyllabusId,Date,Location,Wifi,Topics,Labs,NextClass")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Days.Add(day);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SyllabusId = new SelectList(db.Syllabuses, "Id", "Name", day.SyllabusId);
            return View(day);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            ViewBag.SyllabusId = new SelectList(db.Syllabuses, "Id", "Name", day.SyllabusId);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,SyllabusId,Date,Location,Wifi,Topics,Labs,NextClass")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Entry(day).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SyllabusId = new SelectList(db.Syllabuses, "Id", "Name", day.SyllabusId);
            return View(day);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Day day = db.Days.Find(id);
            db.Days.Remove(day);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
