using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolMS.Models;

namespace SchoolMS.Controllers
{
    public class ClassSectionsController : Controller
    {
        private ApplicationDbContext db;
        public ClassSectionsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: ClassSections
        public ActionResult Index()
        {
            var classSections = db.ClassSections.Include(c => c.ClassModels).Include(c => c.Sections).Include(c => c.Users);
            return View(classSections.ToList());
        }

        // GET: ClassSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classSection = db.ClassSections.Include(c => c.ClassModels).Include(c => c.Sections)
                .Include(c => c.Users).Single(u=>u.ClassSectionID==id);
            if (classSection == null)
            {
                return HttpNotFound();
            }
            return View(classSection);
        }

        // GET: ClassSections/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.ClassModels, "ClassID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: ClassSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassSection classSection)
        {
            classSection.UserID = 1; 
            if (ModelState.IsValid)
            {
                var SectionName = db.Sections.SingleOrDefault(u => u.SectionID == classSection.SectionID);
                var ClassName = db.ClassModels.SingleOrDefault(u => u.ClassID== classSection.ClassID);
                classSection.Name = ( ClassName.Name + " - ( " + SectionName.SectionName + " ) ");
                db.ClassSections.Add(classSection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassModels, "ClassID", "Name", classSection.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", classSection.SectionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classSection.UserID);
            return View(classSection);
        }

        // GET: ClassSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSection classSection = db.ClassSections.Find(id);
            if (classSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassModels, "ClassID", "Name", classSection.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", classSection.SectionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classSection.UserID);
            return View(classSection);
        }

        // POST: ClassSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassSection classSection)
        {
            classSection.UserID = 1;

            if (ModelState.IsValid)
            {
                var SectionName = db.Sections.SingleOrDefault(u => u.SectionID == classSection.SectionID);
                var ClassName = db.ClassModels.SingleOrDefault(u => u.ClassID == classSection.ClassID);
                classSection.Name = (ClassName.Name + " - ( " + SectionName.SectionName + " ) ");
                db.Entry(classSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassModels, "ClassID", "Name", classSection.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", classSection.SectionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classSection.UserID);
            return View(classSection);
        }

        // GET: ClassSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classSection = db.ClassSections.Include(c => c.ClassModels).Include(c => c.Sections)
     .Include(c => c.Users).Single(u => u.ClassSectionID == id);
            if (classSection == null)
            {
                return HttpNotFound();
            }
            return View(classSection);
        }

        // POST: ClassSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSection classSection = db.ClassSections.Find(id);
            db.ClassSections.Remove(classSection);
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
