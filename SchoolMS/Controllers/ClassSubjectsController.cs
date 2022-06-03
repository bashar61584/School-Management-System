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
    public class ClassSubjectsController : Controller
    {
        private ApplicationDbContext db;
        public ClassSubjectsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: ClassSubjects
        public ActionResult Index()
        {
            var classSubjects = db.ClassSubjects.Include(c => c.ClassSections)
                .Include(c => c.Subjects).Include(c => c.Users);
            return View(classSubjects.ToList());
        }

        // GET: ClassSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classSubject = db.ClassSubjects.Include(c => c.ClassSections)
                .Include(c => c.Subjects).Include(c => c.Users).SingleOrDefault(m=>m.ClassSubjectID==id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            return View(classSubject);
        }

        // GET: ClassSubjects/Create
        public ActionResult Create()
        {
            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: ClassSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassSubject classSubject)
        {
            classSubject.UserID = 1;
            if (ModelState.IsValid)
            {
                var SubjectName = db.Subjects.SingleOrDefault(u => u.SubjectID==classSubject.SubjectID);
                var ClassName = db.ClassSections.SingleOrDefault(u => u.ClassSectionID == classSubject.ClassSectionID);
                classSubject.Name = (SubjectName.Name + " - ( " + ClassName.Name + " ) ");
                db.ClassSubjects.Add(classSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name", classSubject.ClassSectionID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", classSubject.SubjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classSubject.UserID);
            return View(classSubject);
        }

        // GET: ClassSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classSubject = db.ClassSubjects.Find(id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name", classSubject.ClassSectionID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", classSubject.SubjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classSubject.UserID);
            return View(classSubject);
        }

        // POST: ClassSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassSubject classSubject)
        {
            classSubject.UserID = 1;

            if (ModelState.IsValid)
            {
                var SubjectName = db.Subjects.SingleOrDefault(u => u.SubjectID == classSubject.SubjectID);
                var ClassName = db.ClassSections.SingleOrDefault(u => u.ClassSectionID == classSubject.ClassSectionID);
                classSubject.Name = (SubjectName.Name + " - ( " + ClassName.Name + " ) ");
                db.Entry(classSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name", classSubject.ClassSectionID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", classSubject.SubjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classSubject.UserID);
            return View(classSubject);
        }

        // GET: ClassSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classSubject = db.ClassSubjects.Include(c => c.ClassSections)
                .Include(c => c.Subjects).Include(c => c.Users).SingleOrDefault(m => m.ClassSubjectID == id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            return View(classSubject);
        }

        // POST: ClassSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            db.ClassSubjects.Remove(classSubject);
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
