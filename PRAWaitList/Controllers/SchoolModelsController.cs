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
using Microsoft.AspNet.SignalR;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace PRAWaitList.Controllers
{
    [System.Web.Mvc.Authorize]
    public class SchoolModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: SchoolModels
        public ActionResult Index(string SearchDistrict, string SearchState, int? page, int? PageSize)
        {
            TempData["MySchoolModel"] = null;
            SchoolViewModel svm = new SchoolViewModel();
            SendProgress("Loading...", 1, 8);
            svm.StateList = Utility.GetStateList();
            SendProgress("Loading...", 2, 8);
            svm.DistrictList = Utility.GetDistrictListByState(SearchState);
            SendProgress("Loading...", 3, 8);
            TempData["MySchoolModel"] = svm;
            SendProgress("Loading...", 4, 8);
            if(SearchDistrict=="0")
            {
                SearchDistrict = string.Empty;
            }
            ViewBag.SearchDistrict = SearchDistrict;
            ViewBag.SearchState = SearchState;
            int DefaultPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (PageSize != null)
            {
                DefaultPageSize = (int)PageSize;
            }
            ViewBag.PageSize = DefaultPageSize;
            int pageNumber = (page ?? 1);
            ViewBag.Page = page;
            List<SchoolModel> schoollist = new List<SchoolModel>();
            if (String.IsNullOrEmpty(SearchDistrict) && String.IsNullOrEmpty(SearchState))
            {
                svm.lsSchools = schoollist.ToPagedList(pageNumber, DefaultPageSize);
                TempData["MySchoolModel"] = svm;
                ViewBag.ErrorMessage = "Select a State and District to see the list of schools.";
                SendProgress("Complete...", 8, 8);
                return View(svm);
            }
            SendProgress("Loading...", 5, 8);
            if (!String.IsNullOrEmpty(SearchDistrict))
            {
                schoollist = db.Schools.Where(s => s.AgencyID==SearchDistrict).ToList();
            }
            SendProgress("Loading...", 6, 8);
            if (!String.IsNullOrEmpty(SearchState) && String.IsNullOrEmpty(SearchDistrict))
            {
                schoollist = db.Schools.Where(s => s.StateAbbr.Trim() == SearchState).ToList();
            }
            SendProgress("Loading...", 7, 8);
            svm.lsSchools = schoollist.ToPagedList(pageNumber, DefaultPageSize);
            SendProgress("Complete...", 8, 8);
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
        public ActionResult Seed(string sPath)
        {
            List<SchoolModel> schools = new List<SchoolModel>();
            List<SchoolModel> Addschools = new List<SchoolModel>();
            using (StreamReader reader = new StreamReader(sPath, Encoding.UTF8))
            {
                SendProgress("Reading File " + Path.GetFileName(sPath) + " ...", 0, 3);                
                CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                IEnumerable<SchoolRecord> record = csvReader.GetRecords<SchoolRecord>();
                csvReader.Configuration.BadDataFound = null;
                foreach (var rec in record) // Each record will be fetched and printed on the screen
                {
                    SchoolModel school = new SchoolModel();
                    school.SchoolName = rec.SchoolName;
                    school.StateName = rec.StateName;
                    school.StateAbbr = rec.StateAbbr;
                    school.SchoolID = rec.SchoolID;
                    school.SchoolID = school.SchoolID.Replace(@"""", "");
                    school.SchoolID = school.SchoolID.Replace(@"=", "");
                    school.AgencyName = rec.AgencyName;
                    school.AgencyID = rec.AgencyID;
                    school.AgencyID = school.AgencyID.Replace(@"""", "");
                    school.AgencyID = school.AgencyID.Replace(@"=", "");
                    schools.Add(school);
                }
            }
            SendProgress("Getting Database School Records...", 1, 3);
            List<SchoolModel> lsDBSchools = db.Schools.ToList();
            SendProgress("Comparing File Records to DataBase Records...", 2, 3);
            Addschools = schools.Except(lsDBSchools).ToList();
            if (Addschools.Count > 0)
            {
                using (SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString))
                {
                    SendProgress("Adding New Schools to Database...", Addschools.Count / 2, Addschools.Count);
                    sc.Open();
                    InsertSchools(sc, schools);
                    sc.Close();
                }
            }
            SendProgress("Complete...", 1, 1);
            return RedirectToAction("Index");
        }
        public void InsertSchools(SqlConnection sqlConnection, IEnumerable<SchoolModel> schools)
        {
            var tableName = "SchoolModel";
            var bufferSize = 5000;
            var inserter = new BulkInserter<SchoolModel>(sqlConnection, tableName, bufferSize);
            inserter.Insert(schools);
        }
        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            //CALCULATING PERCENTAGE BASED ON THE PARAMETERS SENT
            var percentage = (progressCount * 100) / totalItems;

            //PUSHING DATA TO ALL CLIENTS
            hubContext.Clients.All.AddProgress(progressMessage, percentage + "%");
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

        // GET: SchoolModels/Upload
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    file.SaveAs(path);
                    Seed(path);
                }
            }

            return RedirectToAction("Index");
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
