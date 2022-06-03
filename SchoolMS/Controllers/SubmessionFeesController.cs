using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolMS.Models;
using SchoolMS.ModelsViews;

namespace SchoolMS.Controllers
{
    public class SubmessionFeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubmessionFees
        public ActionResult Index()
        {
           
            var submessionFees = db.SubmessionFees.Include(u=>u.StudentPromotes.Students)
                .Include(u=>u.StudentPromotes.Annuals).Include(s => s.MonthsModels).Include(s => s.Users).ToList();
 

            return View(submessionFees);
        }

        // GET: SubmessionFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var model = db.SubmessionFees.Include(u=>u.StudentPromotes.Annuals).Include(u=>u.Users).Include(u=>u.MonthsModels)
                .Include(u=>u.StudentPromotes.Students)
                .Single(u => u.SubmissionFeeID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: SubmessionFees/Create
        public ActionResult Create()
        {
            
            var model = new ForSubmissionFeeCreateViewModel()
            {
                studentpromote = db.StudentPromotes.Include(u => u.Annuals).Include(u => u.Students).ToList().Where(u => u.IsActive != false),
            submessionFee = new SubmessionFee(),
                monthmodel=db.MonthsModels.ToList()
               
            };
         
            return View(model);
        }

        // POST: SubmessionFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmessionFee submessionFee)
        {
            if (ModelState.IsValid)
            {
                submessionFee.UserID=1;
                db.SubmessionFees.Add(submessionFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var model = new ForSubmissionFeeCreateViewModel()
            {
                studentpromote = db.StudentPromotes.Include(u => u.Annuals).Include(u => u.Students).ToList().Where(u => u.IsActive != false),
                submessionFee =submessionFee,
                monthmodel = db.MonthsModels.ToList()

            };

            
            return View(model);
        }

        // GET: SubmessionFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new ForSubmissionFeeCreateViewModel()
            {
                studentpromote = db.StudentPromotes.Include(u => u.Annuals).Include(u => u.Students).ToList().Where(u => u.IsActive != false),
                submessionFee =db.SubmessionFees.Include(u=>u.StudentPromotes).Single(u=>u.SubmissionFeeID==id)   ,
                monthmodel = db.MonthsModels.ToList()

            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
         
  
        }

        // POST: SubmessionFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubmessionFee submessionFee)
        {
            submessionFee.UserID = 1; 
            if (ModelState.IsValid)
            {
                db.Entry(submessionFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var model = new ForSubmissionFeeCreateViewModel()
            {
                studentpromote = db.StudentPromotes.Include(u => u.Annuals).Include(u => u.Students).ToList().Where(u => u.IsActive != false),
                submessionFee = submessionFee,
                monthmodel = db.MonthsModels.ToList()

            };
            return View(model);
        }

        // GET: SubmessionFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.SubmessionFees.Include(u => u.StudentPromotes.Annuals).Include(u => u.Users).Include(u => u.MonthsModels)
              .Include(u => u.StudentPromotes.Students)
              .Single(u => u.SubmissionFeeID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: SubmessionFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmessionFee submessionFee = db.SubmessionFees.Find(id);
            db.SubmessionFees.Remove(submessionFee);
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
