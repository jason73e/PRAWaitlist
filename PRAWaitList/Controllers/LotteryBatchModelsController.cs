﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRAWaitList.DAL;
using PRAWaitList.Models;
using PagedList;

namespace PRAWaitList.Controllers
{
    [Authorize]
    public class LotteryBatchModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: LotteryBatchModels
        public ActionResult Index()
        {
            LotteryBatchViewModel lbvm = new LotteryBatchViewModel();
            lbvm.lbms = db.LotteryBatches.OrderByDescending(x => x.CreateDate).ToList();
            lbvm.lslvm = new List<LotteryViewModel>();
            lbvm.lsCount = new List<int>();
            lbvm.lsNotify = new List<int>();
            lbvm.lsExpired = new List<int>();
            lbvm.lsAccepted = new List<int>();
            lbvm.lsDeclined = new List<int>();
            foreach(LotteryBatchModel lbm in lbvm.lbms)
            {
                LotteryViewModel a = Utility.GetLotteryViewModel(lbm.Id);
                lbvm.lslvm.Add(a);
                lbvm.lsCount.Add(a.lsLotteryModel.Count);
                int iNotify = a.lsLotteryModel.Where(x => x.Status == "Notified").Count();
                lbvm.lsNotify.Add(iNotify);
                int iExpired = a.lsLotteryModel.Where(x => x.Status == "Expired").Count();
                lbvm.lsExpired.Add(iExpired);
                int iAccepted = a.lsLotteryModel.Where(x => x.Status == "Accepted").Count();
                lbvm.lsAccepted.Add(iAccepted);
                int iDeclined = a.lsLotteryModel.Where(x => x.Status == "Declined").Count();
                lbvm.lsDeclined.Add(iDeclined);
            }
            lbvm.SchoolYears = Utility.GetSchoolYearSelectList();
            return View(lbvm);
        }

        // GET: LotteryBatchModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotteryBatchModel lotteryBatchModel = db.LotteryBatches.Find(id);
            if (lotteryBatchModel == null)
            {
                return HttpNotFound();
            }
            return View(lotteryBatchModel);
        }

        // GET: LotteryBatchModels/Create
        public ActionResult Create()
        {
            List<LotteryBatchModel> ls = new List<LotteryBatchModel>();
            ls.Add(new LotteryBatchModel());
            LotteryBatchViewModel lbvm = new LotteryBatchViewModel();
            lbvm.lbms = ls;
            lbvm.SchoolYears = Utility.GetNextTenSchoolYearList();
            return View(lbvm);
        }

        // POST: LotteryBatchModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LotteryBatchViewModel lbvm)
        {
            foreach (string sKey in Request.Form.AllKeys)
            {
                if (sKey.Contains("."))
                {
                    string sModel = sKey.Split('.')[0];
                    string sField = sKey.Split('.')[1];
                    switch (sField)
                    {
                        case "BatchGrade":
                            lbvm.lbms[0].BatchGrade = (Grade)Convert.ToInt32(Request.Form.Get(sKey));
                            break;
                        case "SchoolYearID":
                            lbvm.lbms[0].SchoolYearID = Convert.ToInt32(Request.Form.Get(sKey));
                            break;

                    }
                }
            }
            string sYearName = Utility.GetSchoolYear(lbvm.lbms[0].SchoolYearID);
            string sBatchName = DateTime.Now.ToString("yyyyMMddHHmmss") +"_"+ sYearName +"_"+ lbvm.lbms[0].BatchGrade.ToString();
            lbvm.lbms[0].BatchName = sBatchName;
            lbvm.lbms[0] = Utility.AddLotteryBatch(lbvm.lbms[0]);

            return RedirectToAction("Index");
        }

        // GET: LotteryBatchModels/BatchDetails/5
        public ActionResult BatchDetails(int? id, int? page, int? PageSize)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotteryViewModel lvm = new LotteryViewModel();
            lvm = Utility.GetLotteryViewModel((int)id);
            int DefaultPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (PageSize != null)
            {
                DefaultPageSize = (int)PageSize;
            }
            ViewBag.PageSize = DefaultPageSize; 
            int pageNumber = (page ?? 1);
            lvm.lsLM = lvm.lsLotteryModel.ToPagedList(pageNumber, DefaultPageSize);
            lvm.LotteryBatchID = (int)id;
            lvm.NotifyExpireHours = Convert.ToInt32(db.ConfigurationSettings.Find("NotifyExpireHours").value) * (-1);
            return View(lvm);
        }

        // GET: LotteryBatchModels/BatchDetails/5
        public ActionResult Notify(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotteryModel lm = db.Lotteries.Find((int)id);
            StudentModel student = db.Students.Find(lm.StudentId);
            FamilyModel family = db.Families.Find(student.FamilyID);
            List<ParentModel> parents = db.Parents.Where(x => x.FamilyID == student.FamilyID).ToList();
            List<SiblingModel> siblings = db.Siblings.Where(x => x.FamilyID == student.FamilyID).ToList();
            SelectList states = Utility.GetStateList();
            IntentToEnrollViewModel ievm = new IntentToEnrollViewModel();
            ievm.fm = family;
            ievm.lsParents = parents;
            ievm.lsSiblings = siblings;
            ievm.sm = student;
            ievm.States = states;
            NotifyViewModel nvm = new NotifyViewModel();
            nvm.lm = lm;
            nvm.ievm = ievm;
            return View(nvm);
        }

        // GET: LotteryBatchModels/BatchDetails/5
        public ActionResult Draw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Random r = new Random();
            LotteryBatchModel lbm = db.LotteryBatches.Find((int)id);
            string sApplyYear = db.SchoolYears.Find(lbm.SchoolYearID).Name;
            List<StudentModel> students = new List<StudentModel>();
            students = db.Students.Where(x => x.ApplyYear == sApplyYear && x.ApplyGrade == lbm.BatchGrade && x.isActive == true && x.Status=="Verified").ToList();
            List<LotteryModel> lsLottery = new List<LotteryModel>();
            string sHomeState = db.ConfigurationSettings.Find("HomeState").value.ToString();
            string sHomeDistrict = db.ConfigurationSettings.Find("HomeDistrict").value.ToString();
            string sLocalDistrict = db.Schools.Where(x => x.StateAbbr == sHomeState && x.AgencyName ==sHomeDistrict).First().AgencyID;
            foreach (StudentModel s in students)
            {
                LotteryModel l = new LotteryModel();
                l.LotteryBatchId = (int)id;
                l.StudentId = s.Id;
                l.ApplyGrade = s.ApplyGrade;
                l.ApplyYear = s.ApplyYear;
                int iLowSeed = 0;
                int iHighSeed = 100;
                if (s.isPRASibling)
                {
                    iLowSeed = 10000;
                    iHighSeed = 19999;
                }
                else if(s.isParentStaff())
                {
                    iLowSeed = 20000;
                    iHighSeed = 29999;
                }
                else if (s.isParentSAC())
                {
                    iLowSeed = 30000;
                    iHighSeed = 39999;
                }
                else if (s.LocalDistrict== sLocalDistrict)
                {
                    iLowSeed = 40000;
                    iHighSeed = 49999;
                }
                else
                {
                    iLowSeed = 50000;
                    iHighSeed = 59999;

                }
                int iRandomID = r.Next(iLowSeed, iHighSeed);
                while (lsLottery.Any(x => x.RandomID == iRandomID))
                {
                    iRandomID = r.Next(iLowSeed, iHighSeed);
                }
                l.RandomID = iRandomID;
                l.CreateDate = DateTime.Now;
                l.UpdateDate = DateTime.Now;
                l.UpdateUserID = User.Identity.Name;
                lsLottery.Add(l);
            }
            db.Lotteries.AddRange(lsLottery);
            db.SaveChanges();

            return RedirectToAction("BatchDetails", new { id = id });
        }

        // GET: LotteryBatchModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotteryBatchModel lotteryBatchModel = db.LotteryBatches.Find(id);
            if (lotteryBatchModel == null)
            {
                return HttpNotFound();
            }
            return View(lotteryBatchModel);
        }

        // POST: LotteryBatchModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BatchName,SchoolYearID,Grade,CreateDate,UpdateDate,UpdateUserID")] LotteryBatchModel lotteryBatchModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lotteryBatchModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lotteryBatchModel);
        }

        // GET: LotteryBatchModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotteryBatchModel lotteryBatchModel = db.LotteryBatches.Find(id);
            if (lotteryBatchModel == null)
            {
                return HttpNotFound();
            }
            return View(lotteryBatchModel);
        }

        // POST: LotteryBatchModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LotteryBatchModel lotteryBatchModel = db.LotteryBatches.Find(id);
            db.LotteryBatches.Remove(lotteryBatchModel);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNotes(NotifyViewModel nvm)
        {
            LotteryModel lm = db.Lotteries.Find(nvm.lm.Id);
            lm.Notes = nvm.lm.Notes + " - " + DateTime.Now.ToString() + " - " + User.Identity.Name + Environment.NewLine;
            lm.UpdateDate = DateTime.Now;
            lm.UpdateUserID = User.Identity.Name;
            db.SaveChanges();
            return RedirectToAction("Notify", new { id = nvm.lm.Id});
        }
        public ActionResult Notified(int lmId, string sParentName)
        {
            LotteryModel lm = db.Lotteries.Find(lmId);
            lm.NotifyDate = DateTime.Now;
            lm.Notes = lm.Notes + "Parent " + sParentName + " Notified - " + lm.NotifyDate.ToString() + " - " + User.Identity.Name + Environment.NewLine;
            lm.UpdateDate = DateTime.Now;
            lm.Status = "Notified";
            lm.UpdateUserID = User.Identity.Name;
            db.SaveChanges();
            return RedirectToAction("Notify", new { id = lmId });
        }

        public ActionResult Accepted(int lmId, string sParentName)
        {
            LotteryModel lm = db.Lotteries.Find(lmId);
            if(lm.NotifyDate == null)
            {
                lm.NotifyDate = DateTime.Now;
            }
            lm.DeclineDate = null;
            lm.AcceptDate = DateTime.Now;
            lm.Notes = lm.Notes + "Parent " + sParentName + " Accepted - " + lm.AcceptDate.ToString() + " - " + User.Identity.Name + Environment.NewLine;
            lm.UpdateDate = DateTime.Now;
            lm.Status = "Accepted";
            lm.UpdateUserID = User.Identity.Name;
            db.SaveChanges();
            return RedirectToAction("Notify", new { id = lmId });
        }

        public ActionResult Declined(int lmId, string sParentName)
        {
            LotteryModel lm = db.Lotteries.Find(lmId);
            if (lm.NotifyDate == null)
            {
                lm.NotifyDate = DateTime.Now;
            }
            lm.DeclineDate = DateTime.Now;
            lm.AcceptDate = null;
            lm.Notes = lm.Notes + "Parent " + sParentName + " Declined - " + lm.DeclineDate.ToString() + " - " + User.Identity.Name + Environment.NewLine;
            lm.UpdateDate = DateTime.Now;
            lm.Status = "Declined";
            lm.UpdateUserID = User.Identity.Name;
            db.SaveChanges();
            return RedirectToAction("Notify", new { id = lmId });
        }
    }
}
