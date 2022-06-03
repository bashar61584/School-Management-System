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
    public class ClassModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassModels
        public ActionResult Index()
        {
            var classModels = db.ClassModels.Include(c => c.Users).Include(u=>u.Programs);
            return View(classModels.ToList());
        }

        // GET: ClassModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classModel = db.ClassModels.Include(u => u.Users).Include(u => u.Programs).Single(u => u.ClassID == id);
            if (classModel == null)
            {
                return HttpNotFound();
            }
            return View(classModel);
        }

        // GET: ClassModels/Create
        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name");
            ViewBag.UserID = new SelectList(db.Programs, "ProgramID", "Name");
            return View();
        }

        // POST: ClassModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassModel classModel)
        {
            classModel.UserID = 1;

            if (ModelState.IsValid)
            {
                var ProgramName = db.Programs.SingleOrDefault(u => u.ProgramID == classModel.ProgramID);
                classModel.Name = (classModel.Name + " - ( " + ProgramName.Name + " ) ");
                db.ClassModels.Add(classModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", classModel.ProgramID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classModel.UserID);
            return View(classModel);
        }

        // GET: ClassModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classModel = db.ClassModels.Find(id);
            if (classModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name",classModel.ProgramID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classModel.UserID);
            return View(classModel);
        }

        // POST: ClassModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassModel classModel)
        {
            classModel.UserID = 1;
            if (ModelState.IsValid)
            {
                var ProgramName = db.Programs.SingleOrDefault(u => u.ProgramID== classModel.ProgramID);
               
                classModel.Name = (classModel.Name + " - ( " + ProgramName.Name+ " ) ");
                
                db.Entry(classModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", classModel.ProgramID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", classModel.UserID);
            return View(classModel);
        }

        // GET: ClassModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classModel = db.ClassModels.Include(u => u.Users).Include(u => u.Programs).Single(u => u.ClassID == id);
            if (classModel == null)
            {
                return HttpNotFound();
            }
            return View(classModel);
        }

        // POST: ClassModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var classModel = db.ClassModels.Find(id);
            db.ClassModels.Remove(classModel);
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
