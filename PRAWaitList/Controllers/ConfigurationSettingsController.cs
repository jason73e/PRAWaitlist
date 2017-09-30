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
    public class ConfigurationSettingsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: ConfigurationSettings
        public ActionResult Index()
        {
            return View(db.ConfigurationSettings.ToList());
        }

        // GET: ConfigurationSettings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigurationSettingsModel configurationSettingsModel = db.ConfigurationSettings.Find(id);
            if (configurationSettingsModel == null)
            {
                return HttpNotFound();
            }
            return View(configurationSettingsModel);
        }

        // GET: ConfigurationSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConfigurationSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "key,value")] ConfigurationSettingsModel configurationSettingsModel)
        {
            if (ModelState.IsValid)
            {
                db.ConfigurationSettings.Add(configurationSettingsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configurationSettingsModel);
        }

        // GET: ConfigurationSettings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigurationSettingsModel configurationSettingsModel = db.ConfigurationSettings.Find(id);
            if (configurationSettingsModel == null)
            {
                return HttpNotFound();
            }
            return View(configurationSettingsModel);
        }

        // POST: ConfigurationSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "key,value")] ConfigurationSettingsModel configurationSettingsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configurationSettingsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configurationSettingsModel);
        }

        // GET: ConfigurationSettings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigurationSettingsModel configurationSettingsModel = db.ConfigurationSettings.Find(id);
            if (configurationSettingsModel == null)
            {
                return HttpNotFound();
            }
            return View(configurationSettingsModel);
        }

        // POST: ConfigurationSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ConfigurationSettingsModel configurationSettingsModel = db.ConfigurationSettings.Find(id);
            db.ConfigurationSettings.Remove(configurationSettingsModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Seed()
        {
            var AddSettings = new List<ConfigurationSettingsModel>();
            var settings = new List<ConfigurationSettingsModel>
            {
                new ConfigurationSettingsModel() { key="NotifyExpireHours" , value="48"},
                new ConfigurationSettingsModel() { key="HomeState",value="CO" },
                new ConfigurationSettingsModel() { key="HomeDistrict",value="Douglas County School District No. Re 1"}
            };
            foreach (ConfigurationSettingsModel s in settings)
            {
                if (!db.ConfigurationSettings.Any(x => x.key == s.key))
                {
                    AddSettings.Add(s);
                }
            }
            if (AddSettings.Count > 0)
            {
                AddSettings.ForEach(s => db.ConfigurationSettings.Add(s));
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
