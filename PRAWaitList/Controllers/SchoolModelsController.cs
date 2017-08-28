using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRAWaitList.DAL;
using PRAWaitList.Models;
using System.Reflection;
using System.IO;
using CsvHelper;
using System.Text;

namespace PRAWaitList.Controllers
{
    [Authorize]
    public class SchoolModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: SchoolModels
        public ActionResult Index()
        {
            return View(db.Schools.ToList());
        }

        public ActionResult Seed()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PRAWaitList.App_Data.USSchoolList.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    List<SchoolModel> schools = new List<SchoolModel>();
                    while (csvReader.Read())
                    {
                        SchoolModel school = new SchoolModel();
                        school.SchoolName = csvReader.GetField<string>("SchoolName");
                        school.StateName = csvReader.GetField<string>("StateName");
                        school.StateAbbr = csvReader.GetField<string>("StateAbbr");
                        school.SchoolID = csvReader.GetField<string>("SchoolID");
                        school.SchoolID = school.SchoolID.Replace(@"""", "");
                        school.SchoolID = school.SchoolID.Replace(@"=", "");
                        school.AgencyName = csvReader.GetField<string>("AgencyName");
                        school.AgencyID = csvReader.GetField<string>("AgencyID");
                        school.AgencyID = school.AgencyID.Replace(@"""", "");
                        school.AgencyID = school.AgencyID.Replace(@"=", "");
                        schools.Add(school);
                    }
                    db.Schools.AddRange(schools);
                }
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: SchoolModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolModel schoolModel = db.Schools.Find(id);
            if (schoolModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolModel);
        }

        // GET: SchoolModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SchoolName,StateName,StateAbbr,SchoolID,AgencyName,AgencyID")] SchoolModel schoolModel)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(schoolModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolModel);
        }

        // GET: SchoolModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolModel schoolModel = db.Schools.Find(id);
            if (schoolModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolModel);
        }

        // POST: SchoolModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SchoolName,StateName,StateAbbr,SchoolID,AgencyName,AgencyID")] SchoolModel schoolModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolModel);
        }

        // GET: SchoolModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolModel schoolModel = db.Schools.Find(id);
            if (schoolModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolModel);
        }

        // POST: SchoolModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolModel schoolModel = db.Schools.Find(id);
            db.Schools.Remove(schoolModel);
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
