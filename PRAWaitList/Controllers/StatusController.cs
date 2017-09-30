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
    public class StatusController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: Status
        public ActionResult Index()
        {
            return View(db.StatusModels.ToList());
        }

        // GET: Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusModel statusModel = db.StatusModels.Find(id);
            if (statusModel == null)
            {
                return HttpNotFound();
            }
            return View(statusModel);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] StatusModel statusModel)
        {
            if (ModelState.IsValid)
            {
                db.StatusModels.Add(statusModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusModel);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusModel statusModel = db.StatusModels.Find(id);
            if (statusModel == null)
            {
                return HttpNotFound();
            }
            return View(statusModel);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] StatusModel statusModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusModel);
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusModel statusModel = db.StatusModels.Find(id);
            if (statusModel == null)
            {
                return HttpNotFound();
            }
            return View(statusModel);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusModel statusModel = db.StatusModels.Find(id);
            db.StatusModels.Remove(statusModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Seed()
        {
            var AddStatus = new List<StatusModel>();
            var status = new List<StatusModel>
            {
                new StatusModel() { Status="Submitted" },
                new StatusModel() { Status="Verified"},
                new StatusModel() { Status="Notified"},
                new StatusModel() { Status="Accepted"},
                new StatusModel() { Status="Declined"},
                new StatusModel() { Status="Renewal"},
                new StatusModel() { Status="Expired"}
            };
            foreach (StatusModel s in status)
            {
                if (!db.StatusModels.Any(x => x.Status == s.Status))
                {
                    AddStatus.Add(s);
                }
            }
            if (AddStatus.Count > 0)
            {
                AddStatus.ForEach(s => db.StatusModels.Add(s));
                db.SaveChanges();
            }
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
