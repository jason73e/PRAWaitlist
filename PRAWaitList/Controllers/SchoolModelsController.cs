using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PRAWaitList.DAL;
using PRAWaitList.Models;
using System.Reflection;
using System.IO;
using CsvHelper;
using System.Text;
using PagedList;

namespace PRAWaitList.Controllers
{
    [Authorize]
    public class SchoolModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: SchoolModels
        public ActionResult Index(string currentDistrict,string SearchDistrict, string currentState, string SearchState, int? page, int? PageSize)
        {
            TempData["MySchoolModel"] = null;
            if (SearchDistrict != null)
            {
                page = 1;
            }
            else
            {
                SearchDistrict = currentDistrict;
            }
            if (SearchState != null)
            {
                page = 1;
            }
            else
            {
                SearchState = currentState;
            }
            ViewBag.CurrentDistrict = SearchDistrict;
            ViewBag.CurrentState = SearchState;

            int DefaultPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (PageSize != null)
            {
                DefaultPageSize = (int)PageSize;
            }
            ViewBag.PageSize = DefaultPageSize;
            int pageNumber = (page ?? 1);
            ViewBag.Page = page;
            var schoollist = db.Schools.ToList();
            if (!String.IsNullOrEmpty(SearchDistrict))
            {
                schoollist = schoollist.Where(s => s.AgencyID==SearchDistrict).ToList();
            }
            if (!String.IsNullOrEmpty(SearchState))
            {
                schoollist = schoollist.Where(s => s.StateAbbr.Trim() == SearchState).ToList();
            }
            SchoolViewModel svm = new SchoolViewModel();
            svm.lsSchools = schoollist.ToPagedList(pageNumber, DefaultPageSize);
            svm.StateList = Utility.GetStateList();
            svm.DistrictList = Utility.GetDistrictListByState(SearchState);
            TempData["MySchoolModel"] = svm;
            return View(svm);
        }

        public JsonResult GetDistricts(string sStateCode)
        {
            SchoolViewModel ivm = (SchoolViewModel)TempData["MySchoolModel"];
            SelectList sl = Utility.GetDistrictListByState(sStateCode);
            ivm.DistrictList = sl;
            TempData["MyIEVMModel"] = ivm;
            return Json(sl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Seed()
        {
            List<SchoolModel> schools = new List<SchoolModel>();
            List<SchoolModel> Addschools = new List<SchoolModel>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PRAWaitList.App_Data.USSchoolList.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
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

                }
            }
            foreach (SchoolModel s in schools)
            {
                if (!db.Schools.Any(x => x.SchoolID == s.SchoolID && x.AgencyID == s.AgencyID))
                {
                    Addschools.Add(s);
                }
            }
            if (Addschools.Count > 0)
            {
                db.Schools.AddRange(Addschools);
                db.SaveChanges();
            }
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
