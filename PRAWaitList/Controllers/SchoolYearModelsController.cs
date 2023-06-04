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

namespace PRAWaitList.Controllers
{
    [Authorize]
    public class SchoolYearModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: SchoolYearModels
        public ActionResult Index()
        {
            return View(db.SchoolYears.ToList());
        }

        // GET: SchoolYearModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolYearModel schoolYearModel = db.SchoolYears.Find(id);
            if (schoolYearModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolYearModel);
        }

        // GET: SchoolYearModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolYearModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartYear,EndYear,Name")] SchoolYearModel schoolYearModel)
        {
            if (ModelState.IsValid)
            {
                db.SchoolYears.Add(schoolYearModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolYearModel);
        }

        // GET: SchoolYearModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolYearModel schoolYearModel = db.SchoolYears.Find(id);
            if (schoolYearModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolYearModel);
        }

        // POST: SchoolYearModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StartYear,EndYear,Name")] SchoolYearModel schoolYearModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolYearModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolYearModel);
        }

        // GET: SchoolYearModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolYearModel schoolYearModel = db.SchoolYears.Find(id);
            if (schoolYearModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolYearModel);
        }

        // POST: SchoolYearModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolYearModel schoolYearModel = db.SchoolYears.Find(id);
            db.SchoolYears.Remove(schoolYearModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Seed()
        {
            Utility.AutoSeed();
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
