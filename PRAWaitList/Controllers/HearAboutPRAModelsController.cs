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
    public class HearAboutPRAModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: HearAboutPRAModels
        public ActionResult Index()
        {
            return View(db.HearAboutPRAs.ToList());
        }

        // GET: HearAboutPRAModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HearAboutPRAModel hearAboutPRAModel = db.HearAboutPRAs.Find(id);
            if (hearAboutPRAModel == null)
            {
                return HttpNotFound();
            }
            return View(hearAboutPRAModel);
        }

        // GET: HearAboutPRAModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HearAboutPRAModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Value,Text,IsChecked")] HearAboutPRAModel hearAboutPRAModel)
        {
            if (ModelState.IsValid)
            {
                db.HearAboutPRAs.Add(hearAboutPRAModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hearAboutPRAModel);
        }

        // GET: HearAboutPRAModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HearAboutPRAModel hearAboutPRAModel = db.HearAboutPRAs.Find(id);
            if (hearAboutPRAModel == null)
            {
                return HttpNotFound();
            }
            return View(hearAboutPRAModel);
        }

        // POST: HearAboutPRAModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Value,Text,IsChecked")] HearAboutPRAModel hearAboutPRAModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hearAboutPRAModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hearAboutPRAModel);
        }

        // GET: HearAboutPRAModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HearAboutPRAModel hearAboutPRAModel = db.HearAboutPRAs.Find(id);
            if (hearAboutPRAModel == null)
            {
                return HttpNotFound();
            }
            return View(hearAboutPRAModel);
        }

        // POST: HearAboutPRAModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HearAboutPRAModel hearAboutPRAModel = db.HearAboutPRAs.Find(id);
            db.HearAboutPRAs.Remove(hearAboutPRAModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Seed()
        {
            var Addhearaboutpras = new List<HearAboutPRAModel>();
            var hearaboutpras = new List<HearAboutPRAModel>
            {
                new HearAboutPRAModel() { Text="School Website", IsChecked = false, Value=1},
                new HearAboutPRAModel() { Text="District Website", IsChecked = false, Value=2},
                new HearAboutPRAModel() { Text="Friend recommendation", IsChecked = false, Value=3},
                new HearAboutPRAModel() { Text="Live in the neighborhood", IsChecked = false, Value=4},
                new HearAboutPRAModel() { Text="PRA parent recommendation", IsChecked = false, Value=5},
                new HearAboutPRAModel() { Text="General Internet research", IsChecked = false, Value=6},
                new HearAboutPRAModel() { Text="Other", IsChecked = false, Value=7}
            };

            foreach (HearAboutPRAModel s in hearaboutpras)
            {
                if (!db.HearAboutPRAs.Any(x => x.Value == s.Value))
                {
                    Addhearaboutpras.Add(s);
                }
            }
            if (Addhearaboutpras.Count > 0)
            {
                Addhearaboutpras.ForEach(s => db.HearAboutPRAs.Add(s));
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
