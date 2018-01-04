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
    public class PortalMenusModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        public ActionResult _PortalMenu()
        {
            return PartialView(db.PortalMenusModels.ToList());
        }
        // GET: PortalMenusModels
        public ActionResult Index()
        {
            return View(db.PortalMenusModels.ToList());
        }

        // GET: PortalMenusModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortalMenusModel portalMenusModel = db.PortalMenusModels.Find(id);
            if (portalMenusModel == null)
            {
                return HttpNotFound();
            }
            return View(portalMenusModel);
        }

        // GET: PortalMenusModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortalMenusModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MenuName,ControllerName,ActionName,MenuType,ParentID,Sortorder,RoleName")] PortalMenusModel portalMenusModel)
        {
            if (ModelState.IsValid)
            {
                db.PortalMenusModels.Add(portalMenusModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portalMenusModel);
        }

        // GET: PortalMenusModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortalMenusModel portalMenusModel = db.PortalMenusModels.Find(id);
            if (portalMenusModel == null)
            {
                return HttpNotFound();
            }
            return View(portalMenusModel);
        }

        // POST: PortalMenusModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MenuName,ControllerName,ActionName,MenuType,ParentID,Sortorder,RoleName")] PortalMenusModel portalMenusModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portalMenusModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portalMenusModel);
        }

        // GET: PortalMenusModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortalMenusModel portalMenusModel = db.PortalMenusModels.Find(id);
            if (portalMenusModel == null)
            {
                return HttpNotFound();
            }
            return View(portalMenusModel);
        }

        // POST: PortalMenusModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortalMenusModel portalMenusModel = db.PortalMenusModels.Find(id);
            db.PortalMenusModels.Remove(portalMenusModel);
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
