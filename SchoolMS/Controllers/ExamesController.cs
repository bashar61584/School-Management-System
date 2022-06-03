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
    public class ExamesController : Controller
    {
        private ApplicationDbContext db;
        public ExamesController()
        {
            db= new ApplicationDbContext();
        }

        // GET: Exames
        public ActionResult Index()
        {
            var exames = db.Exames.Include(e => e.ExamTypes).Include(e => e.Users);
            return View(exames.ToList());
        }

        // GET: Exames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var exame = db.Exames.Include(e=>e.ExamTypes).Include(e=>e.Users).SingleOrDefault();
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // GET: Exames/Create
        public ActionResult Create()
        {
            ViewBag.ExamTypeID = new SelectList(db.ExamTypes, "ExamTypeID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Exames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exame exame)
        {
            if (ModelState.IsValid)
            {
                var exametypeInDB = db.ExamTypes.Single(e => e.ExamTypeID == exame.ExamTypeID);
                exame.Name = exametypeInDB.Name + " ( " + exame.StartDate.Year + " )";
                exame.UserID = 1; 
                db.Exames.Add(exame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamTypeID = new SelectList(db.ExamTypes, "ExamTypeID", "Name", exame.ExamTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", exame.UserID);
            return View(exame);
        }

        // GET: Exames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             var exame = db.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamTypeID = new SelectList(db.ExamTypes, "ExamTypeID", "Name", exame.ExamTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", exame.UserID);
            return View(exame);
        }

        // POST: Exames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exame exame)
        {
            if (ModelState.IsValid)
            {
                var exametypeInDB = db.ExamTypes.Single(e => e.ExamTypeID == exame.ExamTypeID);
                exame.Name = exametypeInDB.Name + " ( " + exame.StartDate.Year + " )";
                exame.UserID = 1;
                db.Entry(exame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamTypeID = new SelectList(db.ExamTypes, "ExamTypeID", "Name", exame.ExamTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", exame.UserID);
            return View(exame);
        }

        // GET: Exames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var exame = db.Exames.Include(e => e.ExamTypes).Include(e => e.Users).SingleOrDefault();
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var exame = db.Exames.Include(e => e.ExamTypes).Include(e => e.Users).SingleOrDefault();
            db.Exames.Remove(exame);
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
