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
    public class ExamTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExamTypes
        public ActionResult Index()
        {
            var examTypes = db.ExamTypes.Include(e => e.Users);
            return View(examTypes.ToList());
        }

        // GET: ExamTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var examType = db.ExamTypes.Include(u => u.Users).SingleOrDefault(u => u.ExamTypeID == id); 
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // GET: ExamTypes/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: ExamTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamType examType)
        {
            examType.UserID = 1; 
            if (ModelState.IsValid)
            {
                db.ExamTypes.Add(examType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", examType.UserID);
            return View(examType);
        }

        // GET: ExamTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var examType = db.ExamTypes.Include(u => u.Users).SingleOrDefault(u => u.ExamTypeID == id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", examType.UserID);
            return View(examType);
        }

        // POST: ExamTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExamType examType)
        {
            examType.UserID = 1; 
            if (ModelState.IsValid)
            {
                db.Entry(examType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", examType.UserID);
            return View(examType);
        }

        // GET: ExamTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var examType = db.ExamTypes.Include(u => u.Users).SingleOrDefault(u => u.ExamTypeID == id);

            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // POST: ExamTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamType examType = db.ExamTypes.Find(id);
            db.ExamTypes.Remove(examType);
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
