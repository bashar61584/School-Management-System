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
    public class ProgramSessionsController : Controller
    {
        private ApplicationDbContext db;
        public ProgramSessionsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: ProgramSessions
        public ActionResult Index()
        {
            var programSessions = db.ProgramSessions.Include(p => p.Programs).Include(p => p.Sessions).Include(p => p.Users);
            return View(programSessions.ToList());
        }

        // GET: ProgramSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var programSession = db.ProgramSessions.Include(p => p.Programs)
                .Include(p => p.Sessions).Include(p => p.Users)
                .SingleOrDefault(u => u.ProgramSessionID == id);
                ;
            if (programSession == null)
            {
                return HttpNotFound();
            }
            return View(programSession);
        }

        // GET: ProgramSessions/Create
        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name");
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: ProgramSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgramSession programSession)
        {
            programSession.UserID = 1;

            if (ModelState.IsValid)
            {
                var sessionname = db.Sessions.SingleOrDefault(u => u.SessionID == programSession.SessionID);
                var programname = db.Programs.SingleOrDefault(u => u.ProgramID == programSession.ProgramID);
                programSession.Details = ("( " + sessionname.Name + " - " + programname.Name + " ) ");

                db.ProgramSessions.Add(programSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", programSession.ProgramID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name", programSession.SessionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", programSession.UserID);
            return View(programSession);
        }

        // GET: ProgramSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramSession programSession = db.ProgramSessions.Find(id);
            if (programSession == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", programSession.ProgramID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name", programSession.SessionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", programSession.UserID);
            return View(programSession);
        }

        // POST: ProgramSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProgramSession programSession)
        {
            programSession.UserID = 1;
            if (ModelState.IsValid)
            {
                var sessionname = db.Sessions.SingleOrDefault(u => u.SessionID == programSession.SessionID);
                var programname = db.Programs.SingleOrDefault(u => u.ProgramID == programSession.ProgramID);
                programSession.Details = ("( " + sessionname.Name + " - " + programname.Name + " ) ");

                db.Entry(programSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", programSession.ProgramID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Name", programSession.SessionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", programSession.UserID);
            return View(programSession);
        }

        // GET: ProgramSessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var programSession = db.ProgramSessions.Include(p => p.Programs)
                           .Include(p => p.Sessions).Include(p => p.Users)
                           .SingleOrDefault(u => u.ProgramSessionID == id);
            if (programSession == null)
            {
                return HttpNotFound();
            }
            return View(programSession);
        }

        // POST: ProgramSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramSession programSession = db.ProgramSessions.Find(id);
            db.ProgramSessions.Remove(programSession);
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
