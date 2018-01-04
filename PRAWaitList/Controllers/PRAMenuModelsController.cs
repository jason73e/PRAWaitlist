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
    public class PRAMenuModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        public ActionResult _PRAMenu()
        {
            return PartialView(db.PRAMenuModels.OrderBy(x=>x.sortorder).ToList());
        }
        // GET: PRAMenuModels
        [Authorize]
        public ActionResult Index()
        {
            return View(db.PRAMenuModels.ToList());
        }

        // GET: PRAMenuModels/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRAMenuModel pRAMenuModel = db.PRAMenuModels.Find(id);
            if (pRAMenuModel == null)
            {
                return HttpNotFound();
            }
            return View(pRAMenuModel);
        }

        // GET: PRAMenuModels/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PRAMenuModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Name,link,target,sortorder")] PRAMenuModel pRAMenuModel)
        {
            if (ModelState.IsValid)
            {
                db.PRAMenuModels.Add(pRAMenuModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pRAMenuModel);
        }

        // GET: PRAMenuModels/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRAMenuModel pRAMenuModel = db.PRAMenuModels.Find(id);
            if (pRAMenuModel == null)
            {
                return HttpNotFound();
            }
            return View(pRAMenuModel);
        }

        // POST: PRAMenuModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Name,link,target,sortorder")] PRAMenuModel pRAMenuModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRAMenuModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pRAMenuModel);
        }

        // GET: PRAMenuModels/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRAMenuModel pRAMenuModel = db.PRAMenuModels.Find(id);
            if (pRAMenuModel == null)
            {
                return HttpNotFound();
            }
            return View(pRAMenuModel);
        }

        // POST: PRAMenuModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            PRAMenuModel pRAMenuModel = db.PRAMenuModels.Find(id);
            db.PRAMenuModels.Remove(pRAMenuModel);
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
