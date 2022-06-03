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
    public class StudentPromotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentPromotes
        public ActionResult Index()
        {
            var studentPromotes = db.StudentPromotes.Include(s => s.Annuals).Include(s => s.Students).Include(s => s.Users);
            return View(studentPromotes.ToList());
        }

        // GET: StudentPromotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentPromote = db.StudentPromotes.Include(s => s.Annuals)
                .Include(s => s.Students).Include(s => s.Users).SingleOrDefault(u=>u.StudentPromoteID==id);
            
            if (studentPromote == null)
            {
                return HttpNotFound();
            }
            return View(studentPromote);
        }

        // GET: StudentPromotes/Create
        public ActionResult Create()
        {
            ViewBag.AnnualID = new SelectList(db.Annuals, "AnnualID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: StudentPromotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentPromote studentPromote)
        {
            studentPromote.UserID = 1;

            if (ModelState.IsValid)
            {
              
                db.StudentPromotes.Add(studentPromote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnnualID = new SelectList(db.Annuals, "AnnualID", "Title", studentPromote.AnnualID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentPromote.StudentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", studentPromote.UserID);
            return View(studentPromote);
        }

        // GET: StudentPromotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromote studentPromote = db.StudentPromotes.Find(id);
            if (studentPromote == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnnualID = new SelectList(db.Annuals, "AnnualID", "Title", studentPromote.AnnualID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentPromote.StudentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", studentPromote.UserID);
            return View(studentPromote);
        }

        // POST: StudentPromotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentPromote studentPromote)
        {
            studentPromote.UserID = 1;
            if (ModelState.IsValid)
            {
                db.Entry(studentPromote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnnualID = new SelectList(db.Annuals, "AnnualID", "Title", studentPromote.AnnualID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentPromote.StudentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", studentPromote.UserID);
            return View(studentPromote);
        }

        // GET: StudentPromotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentPromote = db.StudentPromotes.Include(s => s.Annuals)
            .Include(s => s.Students).Include(s => s.Users).SingleOrDefault(u => u.StudentPromoteID == id);
            if (studentPromote == null)
            {
                return HttpNotFound();
            }
            return View(studentPromote);
        }

        // POST: StudentPromotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentPromote studentPromote = db.StudentPromotes.Find(id);
            db.StudentPromotes.Remove(studentPromote);
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
