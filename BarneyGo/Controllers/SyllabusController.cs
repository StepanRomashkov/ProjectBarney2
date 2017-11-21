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
    public class SyllabusController : Controller
    {
        private BarneyContext db = new BarneyContext();

        // GET: Syllabus
        public ActionResult Index()
        {
            var syllabuses = db.Syllabuses.Include(s => s.Admin);
            return View(syllabuses.ToList());
        }

        // GET: Syllabus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabus syllabus = db.Syllabuses.Find(id);
            if (syllabus == null)
            {
                return HttpNotFound();
            }
            return View(syllabus);
        }

        // GET: Syllabus/Create
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Email");
            return View();
        }

        // POST: Syllabus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AdminId")] Syllabus syllabus)
        {
            if (ModelState.IsValid)
            {
                db.Syllabuses.Add(syllabus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Email", syllabus.AdminId);
            return View(syllabus);
        }

        // GET: Syllabus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabus syllabus = db.Syllabuses.Find(id);
            if (syllabus == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Email", syllabus.AdminId);
            return View(syllabus);
        }

        // POST: Syllabus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AdminId")] Syllabus syllabus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(syllabus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Email", syllabus.AdminId);
            return View(syllabus);
        }

        // GET: Syllabus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabus syllabus = db.Syllabuses.Find(id);
            if (syllabus == null)
            {
                return HttpNotFound();
            }
            return View(syllabus);
        }

        // POST: Syllabus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Syllabus syllabus = db.Syllabuses.Find(id);
            db.Syllabuses.Remove(syllabus);
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
