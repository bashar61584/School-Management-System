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
    public class StaffsController : Controller
    {
        private ApplicationDbContext db;
        public StaffsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Designations).Include(s => s.Gendertbls).Include(s => s.Users);
            return View(staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var staff = db.Staffs.Include(s => s.Designations).Include(s => s.Gendertbls).Include(s => s.Users).SingleOrDefault(u=>u.StaffID==id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.DesignationID = new SelectList(db.Designations, "DesignationID", "Name");
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            staff.UserID = 1;

            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                if (staff.PhotoFile != null)
                {
                    var folder = "/Content/StaffPhoto";
                    var file = string.Format("{0}.png", staff.StaffID);
                    var response = UploadFile.UploadPhoto(staff.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        staff.Photo = pic;
                        db.Entry(staff).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.DesignationID = new SelectList(db.Designations, "DesignationID", "Name", staff.DesignationID);
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name", staff.GenderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", staff.UserID);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var staff = db.Staffs.Include(s => s.Designations).Include(s => s.Gendertbls).Include(s => s.Users).SingleOrDefault(u => u.StaffID == id);

            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationID = new SelectList(db.Designations, "DesignationID", "Name", staff.DesignationID);
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name", staff.GenderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", staff.UserID);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            staff.UserID = 1;
            if (ModelState.IsValid)
            {
                if (staff.PhotoFile != null)
                {
                    var folder = "/Content/StaffPhoto";
                    var file = string.Format("{0}.png", staff.StaffID);
                    var response = UploadFile.UploadPhoto(staff.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        staff.Photo = pic;
                    }
                }
                else
                {
                    staff.Photo = "/Content/StaffPhoto/" + staff.StaffID + ".png";
                }
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationID = new SelectList(db.Designations, "DesignationID", "Name", staff.DesignationID);
            ViewBag.GenderID = new SelectList(db.Gendertbls, "GenderID", "Name", staff.GenderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", staff.UserID);
            return View(staff);
            
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var staff = db.Staffs.Include(s => s.Designations).Include(s => s.Gendertbls).Include(s => s.Users).SingleOrDefault(u => u.StaffID == id);

            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
