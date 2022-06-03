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
    public class MonthsModelsController : Controller
    {
        private ApplicationDbContext db;
        public MonthsModelsController()
        {
            db = new ApplicationDbContext();
        }
        // GET: MonthsModels
        public ActionResult Index()
        {
            var monthsModels = db.MonthsModels.Include(m => m.Users);
            return View(monthsModels.ToList());
        }

        // GET: MonthsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var monthsModel = db.MonthsModels.Include(m => m.Users)
                .SingleOrDefault(u=>u.MonthID==id);
            if (monthsModel == null)
            {
                return HttpNotFound();
            }
            return View(monthsModel);
        }

        // GET: MonthsModels/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: MonthsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MonthsModel monthsModel)
        {
            monthsModel.UserID = 1;

            if (ModelState.IsValid)
            {
                db.MonthsModels.Add(monthsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", monthsModel.UserID);
            return View(monthsModel);
        }

        // GET: MonthsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthsModel monthsModel = db.MonthsModels.Find(id);
            if (monthsModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", monthsModel.UserID);
            return View(monthsModel);
        }

        // POST: MonthsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MonthsModel monthsModel)
        {
            monthsModel.UserID = 1;
            if (ModelState.IsValid)
            {
                db.Entry(monthsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", monthsModel.UserID);
            return View(monthsModel);
        }

        // GET: MonthsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var monthsModel = db.MonthsModels.Include(m => m.Users)
                 .SingleOrDefault(u => u.MonthID == id);
            if (monthsModel == null)
            {
                return HttpNotFound();
            }
            return View(monthsModel);
        }

        // POST: MonthsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthsModel monthsModel = db.MonthsModels.Find(id);
            db.MonthsModels.Remove(monthsModel);
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
