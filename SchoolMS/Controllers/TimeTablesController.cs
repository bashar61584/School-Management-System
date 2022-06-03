using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolMS.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Printing;
using SchoolMS.Report.TimeTableRep;

namespace SchoolMS.Controllers
{
    public class TimeTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeTables
        public ActionResult Index()
        {
            var timeTables = db.TimeTables.Include(t => t.ClassSubjects).Include(t => t.Staffs).Include(t => t.Users);
            return View(timeTables.ToList());
        }

        // GET: TimeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var timeTable = db.TimeTables.Include(u=>u.Staffs).Include(u => u.Users).Include(u => u.ClassSubjects).SingleOrDefault(u=>u.TimeTableID==id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTable);
        }

        // GET: TimeTables/Create
        public ActionResult Create()
        {
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjects, "ClassSubjectID", "Name");
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: TimeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeTable timeTable)
        {
            timeTable.UserID = 1;
            if (ModelState.IsValid)
            {
                db.TimeTables.Add(timeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjects, "ClassSubjectID", "Name", timeTable.ClassSubjectID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name", timeTable.StaffID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", timeTable.UserID);
            return View(timeTable);
        }

        // GET: TimeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjects, "ClassSubjectID", "Name", timeTable.ClassSubjectID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name", timeTable.StaffID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", timeTable.UserID);
            return View(timeTable);
        }

        // POST: TimeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TimeTable timeTable)
        {
            timeTable.UserID = 1;

            if (ModelState.IsValid)
            {
                db.Entry(timeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjects, "ClassSubjectID", "Name", timeTable.ClassSubjectID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name", timeTable.StaffID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", timeTable.UserID);
            return View(timeTable);
        }

        // GET: TimeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var timeTable = db.TimeTables.Include(u => u.Staffs).Include(u => u.Users).Include(u => u.ClassSubjects).SingleOrDefault(u => u.TimeTableID == id);

            if (timeTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTable);
        }

        // POST: TimeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeTable timeTable = db.TimeTables.Find(id);
            db.TimeTables.Remove(timeTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Code for the Report Begin
        public ActionResult CreateReport()
        {
            var model = db.Staffs.Include(u => u.Designations).ToList(); 
            return View(model);
        }
        public ActionResult RetriveByStaff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.TimeTables.Include(u => u.ClassSubjects.Subjects).Include(u => u.Staffs)
                 .Include(u => u.ClassSubjects.ClassSections.ClassModels)
                 .ToList().Where(u => u.StaffID ==id);
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }
        public ActionResult CreateReportByClass()
        {
            var model = db.ClassModels.Include(u=>u.Programs).ToList(); 
            return View(model);
        }
        public ActionResult RetriveByClass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.TimeTables.Include(u => u.ClassSubjects.Subjects).Include(u => u.Staffs)
                 .Include(u => u.ClassSubjects.ClassSections.ClassModels)
                 .ToList().Where(u => u.ClassSubjects.ClassSections.ClassModels.ClassID ==id);
            if (model== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }
        // Code for the Report End
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public HttpResponseBase Download_PDF(int? id)
        {
            DataSet1 ds = new DataSet1(); 
            TimeTableReport rp = new TimeTableReport();
            var model=db.TimeTables.Include(u => u.ClassSubjects.Subjects).Include(u => u.Staffs)
                     .Include(u => u.ClassSubjects.ClassSections.ClassModels)
                     .ToList().Where(u => u.StaffID == id);
            foreach (var item in model)
            {
                DataRow dr = ds.Tables["tbl_TimeTable"].NewRow();
                dr["StaffName"] = item.Staffs.Name; 
                dr["ClassSubject"] = item.ClassSubjects.Name;
                dr["StartTime"] = item.StartTime;
                dr["EndTime"] = item.EndTitme;
                dr["Day"] = item.Day;
                dr["Header"] = ProgramTitle.Header;
                dr["Phone"] = ProgramTitle.Phone;
                dr["Address"] = ProgramTitle.Address;
                dr["Email"] = ProgramTitle.Email;
                ds.Tables["tbl_TimeTable"].Rows.Add(dr);


            }
            rp.SetDataSource(ds.Tables["tbl_TimeTable"]);
            var stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            var pdfbytoarray = new byte[stream.Length - 1];
            stream.Position = 0;
            stream.Read(pdfbytoarray, 0, Convert.ToInt32(stream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "filename=TimeTable");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", pdfbytoarray.Length.ToString());
            Response.BinaryWrite(pdfbytoarray);
            return Response;


            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Report/TimeTableRep"), "TimeTableReport.rpt"));
            //rd.SetDataSource(db.TimeTables.Include(u => u.ClassSubjects.Subjects).Include(u => u.Staffs)
            //     .Include(u => u.ClassSubjects.ClassSections.ClassModels)
            //     .ToList().Where(u => u.StaffID == id));

            //var stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //var pdfbytoarray = new byte[stream.Length - 1];
            //stream.Position = 0;
            //stream.Read(pdfbytoarray, 0, Convert.ToInt32(stream.Length - 1));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.AddHeader("content-disposition", "filename=TimeTable");
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-length", pdfbytoarray.Length.ToString());
            //Response.BinaryWrite(pdfbytoarray);
            //return Response;





            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Report/TimeTableRep"), "TimeTableReport.rpt"));
            //rd.SetDataSource(db.TimeTables.Include(u => u.ClassSubjects.Subjects).Include(u => u.Staffs)
            //     .Include(u => u.ClassSubjects.ClassSections.ClassModels)
            //     .ToList().Where(u => u.StaffID == id));

            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();


            //rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            //rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);

            //return File(stream, "application/pdf", "TimeTableByStaff.pdf");
            //}
            //else
            //{
            //}
            //return View();
        }
        public HttpResponseBase Download_PDF2(int? id)
        {
            DataSet1 ds = new DataSet1();
            TimeTableReportClass rp = new TimeTableReportClass();
            var model = db.TimeTables.Include(u => u.ClassSubjects.Subjects).Include(u => u.Staffs)
                 .Include(u => u.ClassSubjects.ClassSections.ClassModels)
                 .ToList().Where(u => u.ClassSubjects.ClassSections.ClassModels.ClassID == id);
            foreach (var item in model)
            {
                DataRow dr = ds.Tables["tbl_TimeTable"].NewRow();
                dr["StaffName"] = item.Staffs.Name;
                dr["ClassSubject"] = item.ClassSubjects.Name;
                dr["StartTime"] = item.StartTime;
                dr["EndTime"] = item.EndTitme;
                dr["Day"] = item.Day;
                dr["Class"] = item.ClassSubjects.ClassSections.Name;
                dr["Subject"] = item.ClassSubjects.Subjects.Name;
                dr["Header"] = ProgramTitle.Header;
                dr["Phone"] = ProgramTitle.Phone;
                dr["Address"] = ProgramTitle.Address;
                dr["Email"] = ProgramTitle.Email;
                ds.Tables["tbl_TimeTable"].Rows.Add(dr);


            }
            rp.SetDataSource(ds.Tables["tbl_TimeTable"]);
            var stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            var pdfbytoarray = new byte[stream.Length - 1];
            stream.Position = 0;
            stream.Read(pdfbytoarray, 0, Convert.ToInt32(stream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "filename=TimeTable");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", pdfbytoarray.Length.ToString());
            Response.BinaryWrite(pdfbytoarray);
            return Response;
        }
        }
    }
