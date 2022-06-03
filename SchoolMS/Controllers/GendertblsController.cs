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
    public class GendertblsController : Controller
    {
        private ApplicationDbContext db;
        public GendertblsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Gendertbls
        public ActionResult Index()
        {
            return View(db.Gendertbls.Include(u=>u.Users).ToList());
        }

        // GET: Gendertbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gendertbl gendertbl = db.Gendertbls.Include(u => u.Users)
                .SingleOrDefault(u=>u.GenderID==id);
            if (gendertbl == null)
            {
                return HttpNotFound();
            }
            return View(gendertbl);
        }

        // GET: Gendertbls/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Gendertbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Gendertbl gendertbl)
        {
            gendertbl.UserID = 1;

            if (ModelState.IsValid)
            {
                db.Gendertbls.Add(gendertbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", gendertbl.UserID);
            return View(gendertbl);
        }

        // GET: Gendertbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gendertbl gendertbl = db.Gendertbls.Find(id);
            if (gendertbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", gendertbl.UserID);
            return View(gendertbl);
        }

        // POST: Gendertbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Gendertbl gendertbl)
        {
            gendertbl.UserID = 1; 
            if (ModelState.IsValid)
            {
                db.Entry(gendertbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", gendertbl.UserID);
            return View(gendertbl);
        }

        // GET: Gendertbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gendertbl gendertbl = db.Gendertbls.Include(u => u.Users)
                 .SingleOrDefault(u => u.GenderID == id);
            if (gendertbl == null)
            {
                return HttpNotFound();
            }
            return View(gendertbl);
        }

        // POST: Gendertbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gendertbl gendertbl = db.Gendertbls.Find(id);
            db.Gendertbls.Remove(gendertbl);
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
