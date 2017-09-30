using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PRAWaitList.DAL;
using PRAWaitList.Models;
using PagedList;

namespace PRAWaitList.Controllers
{
    [Authorize]
    public class StateModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: StateModels
        public ActionResult Index(int? page, int? PageSize)
        {
            TempData["MySLModel"] = null;
            int DefaultPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (PageSize != null)
            {
                DefaultPageSize = (int)PageSize;
            }
            ViewBag.PageSize = DefaultPageSize;
            int pageNumber = (page ?? 1);
            ViewBag.Page = page;
            var statelist = db.States.ToList();
            StateListViewModel m = new StateListViewModel();
            m.lsStates = statelist.ToPagedList(pageNumber, DefaultPageSize);
            TempData["MySLModel"] = m;
            return View(m);
        }

        // GET: StateModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateModel stateModel = db.States.Find(id);
            if (stateModel == null)
            {
                return HttpNotFound();
            }
            return View(stateModel);
        }

        // GET: StateModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StateModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateID,Name")] StateModel stateModel)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(stateModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stateModel);
        }

        // GET: StateModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateModel stateModel = db.States.Find(id);
            if (stateModel == null)
            {
                return HttpNotFound();
            }
            return View(stateModel);
        }

        // POST: StateModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateID,Name")] StateModel stateModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stateModel);
        }

        // GET: StateModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateModel stateModel = db.States.Find(id);
            if (stateModel == null)
            {
                return HttpNotFound();
            }
            return View(stateModel);
        }

        // POST: StateModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StateModel stateModel = db.States.Find(id);
            db.States.Remove(stateModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Seed()
        {
            var AddStates = new List<StateModel>();
            var states = new List<StateModel>
            {
                new StateModel() { Name="Alabama", StateID="AL"},
                new StateModel() { Name="Alaska", StateID="AK"},
                new StateModel() { Name="Arizona", StateID="AZ"},
                new StateModel() { Name="Arkansas", StateID="AR"},
                new StateModel() { Name="California", StateID="CA"},
                new StateModel() { Name="Colorado", StateID="CO"},
                new StateModel() { Name="Connecticut", StateID="CT"},
                new StateModel() { Name="District of Columbia", StateID="DC"},
                new StateModel() { Name="Delaware", StateID="DE"},
                new StateModel() { Name="Florida", StateID="FL"},
                new StateModel() { Name="Georgia", StateID="GA"},
                new StateModel() { Name="Hawaii", StateID="HI"},
                new StateModel() { Name="Idaho", StateID="ID"},
                new StateModel() { Name="Illinois", StateID="IL"},
                new StateModel() { Name="Indiana", StateID="IN"},
                new StateModel() { Name="Iowa", StateID="IA"},
                new StateModel() { Name="Kansas", StateID="KS"},
                new StateModel() { Name="Kentucky", StateID="KY"},
                new StateModel() { Name="Louisiana", StateID="LA"},
                new StateModel() { Name="Maine", StateID="ME"},
                new StateModel() { Name="Maryland", StateID="MD"},
                new StateModel() { Name="Massachusetts", StateID="MA"},
                new StateModel() { Name="Michigan", StateID="MI"},
                new StateModel() { Name="Minnesota", StateID="MN"},
                new StateModel() { Name="Mississippi", StateID="MS"},
                new StateModel() { Name="Missouri", StateID="MO"},
                new StateModel() { Name="Montana", StateID="MT"},
                new StateModel() { Name="Nebraska", StateID="NE"},
                new StateModel() { Name="Nevada", StateID="NV"},
                new StateModel() { Name="New Hampshire", StateID="NH"},
                new StateModel() { Name="New Jersey", StateID="NJ"},
                new StateModel() { Name="New Mexico", StateID="NM"},
                new StateModel() { Name="New York", StateID="NY"},
                new StateModel() { Name="North Carolina", StateID="NC"},
                new StateModel() { Name="North Dakota", StateID="ND"},
                new StateModel() { Name="Ohio", StateID="OH"},
                new StateModel() { Name="Oklahoma", StateID="OK"},
                new StateModel() { Name="Oregon", StateID="OR"},
                new StateModel() { Name="Pennsylvania", StateID="PA"},
                new StateModel() { Name="Rhode Island", StateID="RI"},
                new StateModel() { Name="South Carolina", StateID="SC"},
                new StateModel() { Name="South Dakota", StateID="SD"},
                new StateModel() { Name="Tennessee", StateID="TN"},
                new StateModel() { Name="Texas", StateID="TX"},
                new StateModel() { Name="Utah", StateID="UT"},
                new StateModel() { Name="Vermont", StateID="VT"},
                new StateModel() { Name="Virginia", StateID="VA"},
                new StateModel() { Name="Washington", StateID="WA"},
                new StateModel() { Name="West Virginia", StateID="WV"},
                new StateModel() { Name="Wisconsin", StateID="WI"},
                new StateModel() { Name="Wyoming", StateID="WY"}
            };
            foreach(StateModel s in states)
            {
                if(!db.States.Any(x=> x.StateID==s.StateID))
                {
                    AddStates.Add(s);
                }
            }
            if (AddStates.Count > 0)
            {
                AddStates.ForEach(s => db.States.Add(s));
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
