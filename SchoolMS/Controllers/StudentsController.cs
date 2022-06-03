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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db;
        public StudentsController()
        {
            db = new ApplicationDbContext(); 
        }

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Gendertbls).Include(s => s.Users);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = db.Students.Include(s => s.Gendertbls)
      .Include(s => s.Users).SingleOrDefault(u => u.StudentID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            student.UserID = 1; 
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                if (student.PhotoFile != null)
                {
                    var folder = "/Content/StudentPhoto";
                    var file = string.Format("{0}.png", student.StudentID);
                    var response = UploadFile.UploadPhoto(student.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        student.Photo = pic;
                        db.Entry(student).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name", student.GenderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", student.UserID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name", student.GenderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", student.UserID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            student.UserID = 1;

            if (ModelState.IsValid)
            {
                if (student.PhotoFile != null)
                {
                    var folder = "/Content/StudentPhoto";
                    var file = string.Format("{0}.png", student.StudentID);
                    var response = UploadFile.UploadPhoto(student.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        student.Photo = pic;
                    }
                }
                else
                {
                    student.Photo = "/Content/StudentPhoto/" + student.StudentID + ".png";
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name", student.GenderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", student.UserID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = db.Students.Include(s => s.Gendertbls)
     .Include(s => s.Users).SingleOrDefault(u => u.StudentID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
