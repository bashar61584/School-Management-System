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
    public class AnnualsController : Controller
    {
        private ApplicationDbContext db;
        public AnnualsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Annuals
        public ActionResult Index()
        {
            var annuals = db.Annuals.Include(a => a.ClassSections)
                .Include(a => a.Sessions).Include(a => a.Users);
            return View(annuals.ToList());
        }

        // GET: Annuals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annual = db.Annuals.Include(a => a.ClassSections)
                .Include(a => a.Sessions).Include(a => a.Users).SingleOrDefault(u => u.AnnualID == id);
            if (annual == null)
            {
                return HttpNotFound();
            }
            return View(annual);
        }

        // GET: Annuals/Create
        public ActionResult Create()
        {
            ViewBag.ClassSectionID = new SelectList(db.ClassSections.Where(u=>u.IsActive==true), "ClassSectionID", "Name");
            ViewBag.SessionID = new SelectList(db.Sessions.Where(u => u.IsActive == true), "SessionID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Annuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Annual annual)
        {
            annual.UserID = 1;
            if (ModelState.IsValid)
            {
                var ClassSection = db.ClassSections.SingleOrDefault(u => u.ClassSectionID == annual.ClassSectionID);
                var SessionName = db.Sessions.SingleOrDefault(u => u.SessionID == annual.SessionID);
                annual.Title = (SessionName.Name + " -  " + ClassSection.Name );
                db.Annuals.Add(annual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name", annual.ClassSectionID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name", annual.SessionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", annual.UserID);
            return View(annual);
        }

        // GET: Annuals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annual annual = db.Annuals.Find(id);
            if (annual == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name", annual.ClassSectionID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name", annual.SessionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", annual.UserID);
            return View(annual);
        }

        // POST: Annuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Annual annual)
        {
            annual.UserID = 1;

            if (ModelState.IsValid)
            {
                var ClassSection = db.ClassSections.SingleOrDefault(u => u.ClassSectionID== annual.ClassSectionID);
                var SessionName = db.Sessions.SingleOrDefault(u => u.SessionID== annual.SessionID);
                annual.Title = (SessionName.Name + " -  " + ClassSection.Name);
                db.Entry(annual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassSectionID = new SelectList(db.ClassSections, "ClassSectionID", "Name", annual.ClassSectionID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name", annual.SessionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", annual.UserID);
            return View(annual);
        }

        // GET: Annuals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annual = db.Annuals.Include(a => a.ClassSections)
                .Include(a => a.Sessions).Include(a => a.Users).SingleOrDefault(u => u.AnnualID == id);
            if (annual == null)
            {
                return HttpNotFound();
            }
            return View(annual);
        }

        // POST: Annuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annual annual = db.Annuals.Find(id);
            db.Annuals.Remove(annual);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetClassFee(string sid)
        {
            int classsectionid = Convert.ToInt32(sid);
            var ClassSection = db.ClassSections.Find(classsectionid);
            var ClassM = db.ClassModels.SingleOrDefault(u => u.ClassID == ClassSection.ClassID);
            
            return Json(new { Fees = ClassM.Amount}, JsonRequestBehavior.AllowGet);
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
