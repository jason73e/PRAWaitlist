using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PRAWaitList.DAL;
using PRAWaitList.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR;

namespace PRAWaitList.Controllers
{
    [System.Web.Mvc.Authorize]
    public class WaitListAdminController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string currentStatus, string SearchStatus, Grade? currentGrade, Grade? SearchGrade, string currentApplyYear, string SearchYear, int? page, int? PageSize)
        {
            Utility.AutoSeed();
            WaitlistAdminViewModel m = IndexProcess(sortOrder, currentFilter, searchString, currentStatus, SearchStatus, currentGrade, SearchGrade, currentApplyYear, SearchYear, page, PageSize);
            return View(m);
        }

        private WaitlistAdminViewModel IndexProcess(string sortOrder, string currentFilter, string searchString, string currentStatus, string SearchStatus, Grade? currentGrade, Grade? SearchGrade, string currentApplyYear, string SearchYear, int? page, int? PageSize)
        {
            TempData["MyWLAModel"] = null;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "LastName_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.BirthDateSortParm = sortOrder == "BirthDate" ? "BirthDate_desc" : "BirthDate";
            ViewBag.GenderSortParm = sortOrder == "Gender" ? "Gender_desc" : "Gender";
            ViewBag.CurrentGradeSortParm = sortOrder == "CurrentGrade" ? "CurrentGrade_desc" : "CurrentGrade";
            ViewBag.ApplyingForGradeSortParm = sortOrder == "ApplyingForGrade" ? "ApplyingForGrade_desc" : "ApplyingForGrade";
            ViewBag.ApplyingForYearSortParm = sortOrder == "ApplyingForYear" ? "ApplyingForYear_desc" : "ApplyingForYear";
            ViewBag.LocalSchoolSortParm = sortOrder == "LocalSchool" ? "LocalSchool_desc" : "LocalSchool";
            ViewBag.LocalDistrictSortParm = sortOrder == "LocalDistrict" ? "LocalDistrict_desc" : "LocalDistrict";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_desc" : "Status";
            ViewBag.LearnAboutPRASortParm = sortOrder == "LearnAboutPRA" ? "LearnAboutPRA_desc" : "LearnAboutPRA";
            ViewBag.IsPRASibling = sortOrder == "IsPRASibling" ? "IsPRASibling_desc" : "IsPRASibling";
            //ViewBag.IsParentSAC = sortOrder == "IsParentSAC" ? "IsParentSAC_desc" : "IsParentSAC";
            //ViewBag.IsParentStaff = sortOrder == "IsParentStaff" ? "IsParentStaff_desc" : "IsParentStaff";
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!string.IsNullOrEmpty(SearchStatus))
            {
                page = 1;
            }
            else
            {
                SearchStatus = currentStatus;
            }

            if (SearchGrade != null)
            {
                page = 1;
            }
            else
            {
                SearchGrade = currentGrade;
            }
            if (!string.IsNullOrEmpty(SearchYear))
            {
                page = 1;
            }
            else
            {
                SearchYear = currentApplyYear;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentStatus = SearchStatus;
            ViewBag.CurrentGrade = SearchGrade;
            ViewBag.CurrentApplyYear = SearchYear;


            var students = db.Students.Where(x => x.isActive == true);
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToLower().Contains(searchString.ToLower()) || s.FirstName.ToLower().Contains(searchString.ToLower()));
            }

            if (!String.IsNullOrEmpty(SearchStatus))
            {
                students = students.Where(s => s.Status == SearchStatus);
            }

            if (SearchGrade != null)
            {
                students = students.Where(s => s.ApplyGrade == SearchGrade);
            }

            if (!string.IsNullOrEmpty(SearchYear))
            {
                students = students.Where(s => s.ApplyYear == SearchYear);
            }

            switch (sortOrder)
            {
                case "LastName_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "FirstName":
                    students = students.OrderBy(s => s.FirstName);
                    break;
                case "FirstName_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "BirthDate":
                    students = students.OrderBy(s => s.BirthDate);
                    break;
                case "BirthDate_desc":
                    students = students.OrderByDescending(s => s.BirthDate);
                    break;
                case "Gender":
                    students = students.OrderBy(s => s.Gender);
                    break;
                case "Gender_desc":
                    students = students.OrderByDescending(s => s.Gender);
                    break;
                case "CurrentGrade":
                    students = students.OrderBy(s => s.CurrentGrade);
                    break;
                case "CurrentGrade_desc":
                    students = students.OrderByDescending(s => s.CurrentGrade);
                    break;
                case "ApplyingForGrade":
                    students = students.OrderBy(s => s.ApplyGrade);
                    break;
                case "ApplyingForGrade_desc":
                    students = students.OrderByDescending(s => s.ApplyGrade);
                    break;
                case "ApplyingForYear":
                    students = students.OrderBy(s => s.ApplyYear);
                    break;
                case "ApplyingForYear_desc":
                    students = students.OrderByDescending(s => s.ApplyYear);
                    break;
                case "LocalSchool":
                    students = students.OrderBy(s => s.LocalSchool);
                    break;
                case "LocalSchool_desc":
                    students = students.OrderByDescending(s => s.LocalSchool);
                    break;
                case "LocalDistrict":
                    students = students.OrderBy(s => s.LocalDistrict);
                    break;
                case "LocalDistrict_desc":
                    students = students.OrderByDescending(s => s.LocalDistrict);
                    break;
                case "Status":
                    students = students.OrderBy(s => s.Status);
                    break;
                case "Status_desc":
                    students = students.OrderByDescending(s => s.Status);
                    break;
                case "LearnAboutPRA":
                    students = students.OrderBy(s => s.LearnAboutPRA);
                    break;
                case "LearnAboutPRA_desc":
                    students = students.OrderByDescending(s => s.LearnAboutPRA);
                    break;
                case "IsPRASibling":
                    students = students.OrderBy(s => s.isPRASibling);
                    break;
                case "IsPRASibling_desc":
                    students = students.OrderByDescending(s => s.isPRASibling);
                    break;
                //case "IsParentStaff":
                //    students = students.OrderBy(s => s.isParentStaff());
                //    break;
                //case "IsParentStaff_desc":
                //    students = students.OrderByDescending(s => s.isParentStaff());
                //    break;
                //case "IsParentSAC":
                //    students = students.OrderBy(s => s.isParentSAC());
                //    break;
                //case "IsParentSAC_desc":
                //    students = students.OrderByDescending(s => s.isParentSAC());
                //    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            int DefaultPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (PageSize != null)
            {
                DefaultPageSize = (int)PageSize;
            }
            ViewBag.PageSize = DefaultPageSize;
            int pageNumber = (page ?? 1);
            ViewBag.Page = page;
            WaitlistAdminViewModel m = new WaitlistAdminViewModel();
            m.lsStudents = students.ToPagedList(pageNumber, DefaultPageSize);
            m.StatusList = Utility.GetStatusList();
            m.SchoolYearList = Utility.GetSchoolYearList();
            m.SearchGrade = SearchGrade;
            m.SearchStatus = SearchStatus;
            m.SearchYear = SearchYear;
            return m;
        }
        // GET: WaitListAdmin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string currentStatus, string SearchStatus,Grade? currentGrade, Grade? SearchGrade, string currentApplyYear, string SearchYear, int? page, int? PageSize,string submitbutton)
        {
            if(submitbutton=="Reset")
            {
                return RedirectToAction("Index");
            }
            WaitlistAdminViewModel m = IndexProcess(sortOrder, currentFilter, searchString, currentStatus, SearchStatus, currentGrade, SearchGrade, currentApplyYear, SearchYear, page, PageSize);
            return View(m);
        }

        // GET: WaitListAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = db.Students.Find(id);
            int iFamilyID = studentModel.FamilyID;
            if (studentModel == null)
            {
                return HttpNotFound("Student Record Not Found");
            }
            studentModel.lsLearnAboutPRA = studentModel.GetStudentHearAboutPRALink();

            FamilyModel familyModel = db.Families.Find(iFamilyID);

            if (familyModel == null)
            {
                return HttpNotFound("Family Record Not Found");
            }

            List<ParentModel> lsp = db.Parents.Where(x => x.FamilyID == iFamilyID && x.isActive==true).ToList();
            List<SiblingModel> lss = new List<SiblingModel>();
            if(db.Siblings.Where(x => x.FamilyID == iFamilyID && x.isActive == true).Any())
            {
                lss = db.Siblings.Where(x => x.FamilyID == iFamilyID && x.isActive == true).ToList();
            }
            IntentToEnrollViewModel vm = new IntentToEnrollViewModel();
            vm.sm = studentModel;
            vm.fm = familyModel;
            vm.lsParents = lsp;
            vm.lsSiblings = lss;
            vm.lsLearnPRAList = Utility.GetHearAboutPRAs();
            vm.States = Utility.GetStateList();
            vm.DistrictList = Utility.GetDistrictListByState(vm.fm.StateID);
            vm.SchoolList = Utility.GetSchoolListByDistrict(vm.sm.LocalDistrict);
            vm.StatusList = Utility.GetStatusList();
            TempData["MyWLAModel"] = vm;

            return View(vm);
        }

        // GET: WaitListAdmin/Create
        public ActionResult Create()
        {
            return RedirectToAction("index","IntentToEnroll");
        }


        // GET: WaitListAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            IntentToEnrollViewModel vm = new IntentToEnrollViewModel();
            if (TempData["MyWLAModel"] == null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentModel studentModel = db.Students.Find(id);
                if (studentModel == null)
                {
                    return HttpNotFound("Student Record Not Found");
                }
                studentModel.lsLearnAboutPRA = studentModel.GetStudentHearAboutPRALink();
                int iFamilyID = studentModel.FamilyID;

                FamilyModel familyModel = db.Families.Find(iFamilyID);

                if (familyModel == null)
                {
                    return HttpNotFound("Family Record Not Found");
                }

                List<ParentModel> lsp = db.Parents.Where(x => x.FamilyID == iFamilyID && x.isActive == true).ToList();
                List<SiblingModel> lss = new List<SiblingModel>();
                if (db.Siblings.Where(x => x.FamilyID == iFamilyID && x.isActive == true).Any())
                {
                    lss = db.Siblings.Where(x => x.FamilyID == iFamilyID && x.isActive == true).ToList();
                }

                vm.sm = studentModel;
                vm.fm = familyModel;
                vm.lsParents = lsp;
                vm.lsSiblings = lss;
                vm.lsLearnPRAList = Utility.GetHearAboutPRAs();
                vm.States = Utility.GetStateList();
                vm.DistrictList = Utility.GetDistrictListByState(vm.fm.StateID);
                vm.SchoolList = Utility.GetSchoolListByDistrict(vm.sm.LocalDistrict);
                vm.StatusList = Utility.GetStatusList();
                TempData["MyWLAModel"] = vm;
            }
            else
            {
                vm = (IntentToEnrollViewModel)TempData["MyWLAModel"];
            }
            return View(vm);
        }


        
        // POST: WaitListAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IntentToEnrollViewModel ivm, string Save, string AddParent, string RemoveParent, string AddSibling, string RemoveSibling)
        {
            ivm = GetFormData(ivm);
            if (!string.IsNullOrEmpty(AddParent))
            {
                return this.AddParent(ivm);
            }

            if (!string.IsNullOrEmpty(AddSibling))
            {
                return this.AddSibling(ivm);
            }

            if (!string.IsNullOrEmpty(RemoveParent))
            {
                return this.RemoveParent(ivm);
            }

            if (!string.IsNullOrEmpty(RemoveSibling))
            {
                string sMySiblingPrefix = RemoveSibling.Replace("Remove Sibling ", string.Empty);
                return this.RemoveSibling(ivm, sMySiblingPrefix);
            }

            if (!string.IsNullOrEmpty(Save))
            {
                ivm.fm = Utility.UpdateFamily(ivm.fm);
                List<ParentModel> lsDBParents = db.Parents.Where(x => x.FamilyID == ivm.fm.Id).ToList();
                foreach(ParentModel p in lsDBParents)
                {
                    if(!ivm.lsParents.Any(x=>x.Id==p.Id))
                    {
                        db.Parents.Remove(p);
                        db.SaveChanges();
                    }
                }
                foreach (ParentModel p in ivm.lsParents)
                {
                    p.FamilyID = ivm.fm.Id;
                    Utility.UpdateParent(p);
                }
                Boolean isPRASibling = false;
                if (db.Siblings.Any(x => x.FamilyID == ivm.fm.Id))
                {
                    List<SiblingModel> lsDBSiblings = db.Siblings.Where(x => x.FamilyID == ivm.fm.Id).ToList();
                    foreach (SiblingModel s in lsDBSiblings)
                    {
                        if (!ivm.lsSiblings.Any(x => x.Id == s.Id))
                        {
                            db.Siblings.Remove(s);
                            db.SaveChanges();
                        }
                    }
                }
                foreach (SiblingModel s in ivm.lsSiblings)
                {
                    s.FamilyID = ivm.fm.Id;
                    if (s.isPRAStudent == true)
                    {
                        isPRASibling = true;
                    }
                    Utility.UpdateSibling(s);
                }
                ivm.sm.isPRASibling = isPRASibling;
                ivm.sm = Utility.UpdateStudent(ivm.sm);
                Utility.UpdateIsPraSibling(ivm.sm.Id);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: WaitListAdmin/AddParnet/5/5
        private IntentToEnrollViewModel GetFormData(IntentToEnrollViewModel ivm)
        {
            if (TempData["MyWLAModel"] == null)
            {
                ivm.fm = new FamilyModel();
                ivm.sm = new StudentModel();
                ivm.lsParents = new List<ParentModel>();
                ivm.lsSiblings = new List<SiblingModel>();
                ivm.lsLearnPRAList = Utility.GetHearAboutPRAs();
                ivm.States = Utility.GetStateList();
                ivm.DistrictList = Utility.GetDistrictListByState("");
                ivm.SchoolList = Utility.GetSchoolListByDistrict("");
                ivm.StatusList = Utility.GetStatusList();
            }
            else
            {
                ivm = (IntentToEnrollViewModel)TempData["MyWLAModel"];
            }
            foreach (string sKey in Request.Form.AllKeys)
            {
                if (sKey.Contains("."))
                {
                    string sModel = sKey.Split('.')[0];
                    string sField = sKey.Split('.')[sKey.Split('.').Length - 1];
                    if (sModel == "Family")
                    {
                        switch (sField)
                        {
                            case "Address1":
                                ivm.fm.Address1 = Request.Form.Get(sKey);
                                break;
                            case "Address2":
                                ivm.fm.Address2 = Request.Form.Get(sKey);
                                break;
                            case "City":
                                ivm.fm.City = Request.Form.Get(sKey);
                                break;
                            case "FamilyName":
                                ivm.fm.FamilyName = Request.Form.Get(sKey);
                                break;
                            case "StateID":
                                ivm.fm.StateID = Request.Form.Get(sKey);
                                break;
                            case "ZipCode":
                                ivm.fm.ZipCode = Request.Form.Get(sKey);
                                break;
                        }

                    }
                    else if (sModel == "Student")
                    {
                        switch (sField)
                        {
                            case "ApplyGrade":
                                ivm.sm.ApplyGrade = (Grade)Convert.ToInt32(Request.Form.Get(sKey));
                                break;
                            case "ApplyYear":
                                ivm.sm.ApplyYear = Request.Form.Get(sKey);
                                break;
                            case "BirthDate":
                                ivm.sm.BirthDate = Convert.ToDateTime(Request.Form.Get(sKey));
                                break;
                            case "CurrentGrade":
                                ivm.sm.CurrentGrade = (Grade)Convert.ToInt32(Request.Form.Get(sKey));
                                break;
                            case "FirstName":
                                ivm.sm.FirstName = Request.Form.Get(sKey);
                                break;
                            case "Gender":
                                ivm.sm.Gender = Request.Form.Get(sKey);
                                break;
                            case "LastName":
                                ivm.sm.LastName = Request.Form.Get(sKey);
                                break;
                            case "LearnAboutPRA":
                                ivm.sm.LearnAboutPRA = Request.Form.Get(sKey);
                                break;
                            case "LocalDistrict":
                                ivm.sm.LocalDistrict = Request.Form.Get(sKey);
                                break;
                            case "LocalSchool":
                                ivm.sm.LocalSchool = Request.Form.Get(sKey);
                                break;
                            case "Status":
                                ivm.sm.Status = Request.Form.Get(sKey);
                                break;
                        }
                    }
                    else if (sModel.StartsWith("Parents"))
                    {
                        int iParentCount = 0;
                        string sParentCount = sKey.Split('.')[1];
                        sParentCount = sParentCount.Replace("lsParents[", string.Empty);
                        sParentCount = sParentCount.Replace("]", string.Empty);
                        iParentCount = Convert.ToInt32(sParentCount);
                        switch (sField)
                        {
                            case "Address1":
                                ivm.lsParents[iParentCount].Address1 = Request.Form.Get(sKey);
                                break;
                            case "Address2":
                                ivm.lsParents[iParentCount].Address2 = Request.Form.Get(sKey);
                                break;
                            case "City":
                                ivm.lsParents[iParentCount].City = Request.Form.Get(sKey);
                                break;
                            case "EmailAddress":
                                ivm.lsParents[iParentCount].EmailAddress = Request.Form.Get(sKey);
                                break;
                            case "FirstName":
                                ivm.lsParents[iParentCount].FirstName = Request.Form.Get(sKey);
                                break;
                            case "isPreferredContact":
                                string svalue = Request.Form.GetValues(sKey)[0];
                                bool bValue = false;
                                if (svalue == "true")
                                {
                                    bValue = true;
                                }
                                ivm.lsParents[iParentCount].isPreferredContact = bValue;
                                break;
                            case "LastName":
                                ivm.lsParents[iParentCount].LastName = Request.Form.Get(sKey);
                                break;
                            case "Phone1":
                                ivm.lsParents[iParentCount].Phone1 = Request.Form.Get(sKey);
                                break;
                            case "Phone1Type":
                                ivm.lsParents[iParentCount].Phone1Type = (PhoneType)Convert.ToInt32(Request.Form.Get(sKey));
                                break;
                            case "Phone2":
                                ivm.lsParents[iParentCount].Phone2 = Request.Form.Get(sKey);
                                break;
                            case "Phone2Type":
                                ivm.lsParents[iParentCount].Phone2Type = (PhoneType)Convert.ToInt32(Request.Form.Get(sKey));
                                break;
                            case "pType":
                                ivm.lsParents[iParentCount].pType = (ParentType)Convert.ToInt32(Request.Form.Get(sKey));
                                break;
                            case "StateID":
                                ivm.lsParents[iParentCount].StateID = Request.Form.Get(sKey);
                                break;
                            case "ZipCode":
                                ivm.lsParents[iParentCount].ZipCode = Request.Form.Get(sKey);
                                break;
                        }


                    }
                    else if (sModel.StartsWith("Siblings"))
                    {
                        int iSiblingCount = 0;
                        string sSiblingCount = sKey.Split('.')[1];
                        sSiblingCount = sSiblingCount.Replace("lsSiblings[", string.Empty);
                        sSiblingCount = sSiblingCount.Replace("]", string.Empty);
                        iSiblingCount = Convert.ToInt32(sSiblingCount);
                        switch (sField)
                        {
                            case "BirthDate":
                                ivm.lsSiblings[iSiblingCount].BirthDate = Convert.ToDateTime(Request.Form.Get(sKey));
                                break;
                            case "FirstName":
                                ivm.lsSiblings[iSiblingCount].FirstName = Request.Form.Get(sKey);
                                break;
                            case "isPRAStudent":
                                string svalue = Request.Form.GetValues(sKey)[0];
                                bool bValue = false;
                                if (svalue == "true")
                                {
                                    bValue = true;
                                }
                                ivm.lsSiblings[iSiblingCount].isPRAStudent = bValue;
                                break;
                            case "LastName":
                                ivm.lsSiblings[iSiblingCount].LastName = Request.Form.Get(sKey);
                                break;
                        }
                    }
                }
            }
            TempData["MyWLAModel"] = ivm;
            return ivm;

        }
        public ViewResult AddSibling(IntentToEnrollViewModel ivm)
        {
            ivm.lsSiblings.Add(new SiblingModel());
            TempData["MyWLAModel"] = ivm;
            return View("Edit", ivm);

        }

        public ViewResult RemoveSibling(IntentToEnrollViewModel ivm, string sMySiblingPrefix)
        {
            int iSiblingIndex = Convert.ToInt32(sMySiblingPrefix.Replace("Sibling", string.Empty)) - 1;
            ivm.lsSiblings.RemoveAt(iSiblingIndex);
            TempData["MyWLAModel"] = ivm;
            return View("Edit", ivm);
        }

        public ViewResult AddParent(IntentToEnrollViewModel ivm)
        {
            ivm.lsParents.Add(new ParentModel());
            TempData["MyWLAModel"] = ivm;
            return View("Edit", ivm);
        }

        public ViewResult RemoveParent(IntentToEnrollViewModel ivm)
        {
            ivm.lsParents.RemoveAt(1);
            TempData["MyWLAModel"] = ivm;
            return View("Edit", ivm);
        }

        // GET: WaitListAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = db.Students.Find(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            studentModel.lsLearnAboutPRA = studentModel.GetStudentHearAboutPRALink();

            return View(studentModel);
        }

        // POST: WaitListAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentModel studentModel = db.Students.Find(id);

            if (studentModel != null)
            {
                studentModel.ApplyGrade = studentModel.ApplyGrade;
                studentModel.ApplyYear = studentModel.ApplyYear;
                studentModel.BirthDate = studentModel.BirthDate;
                studentModel.CreateDate = studentModel.CreateDate;
                studentModel.CurrentGrade = studentModel.CurrentGrade;
                studentModel.FamilyID = studentModel.FamilyID;
                studentModel.FirstName = studentModel.FirstName;
                studentModel.Gender = studentModel.Gender;
                studentModel.Id = studentModel.Id;
                studentModel.isActive = false;
                studentModel.LastName = studentModel.LastName;
                studentModel.LearnAboutPRA = studentModel.LearnAboutPRA;
                studentModel.LocalDistrict = studentModel.LocalDistrict;
                studentModel.LocalSchool = studentModel.LocalSchool;
                studentModel.Status = studentModel.Status;
                studentModel.UpdateDate = DateTime.Now;
                studentModel.UpdateUserID = User.Identity.Name;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ImportStudents()
        {
            List<Results> lsr = db.Results.Where(x => x.imported==false).ToList();
            IntentToEnrollViewModel ivm = new IntentToEnrollViewModel();
            int icounter = 0;
            if (lsr.Count > 0)
            {
                SendProgress("Importing Students...", icounter, lsr.Count);
            }
            ivm.lsParents = new List<ParentModel>();
            ivm.lsSiblings = new List<SiblingModel>();
            foreach(Results r in lsr)
            {
                try
                {
                    ivm = new IntentToEnrollViewModel();
                    ivm.lsParents = new List<ParentModel>();
                    ivm.lsSiblings = new List<SiblingModel>();
                    FamilyModel f = new FamilyModel();
                    f.Address1 = r.Address;
                    f.Address2 = r.Apt_Unit_No;
                    f.City = r.City;
                    f.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                    f.FamilyName = r.Lastname;
                    f.IsActive = true;
                    f.StateID = r.State;
                    f.ZipCode = r.Zip;
                    ivm.fm = f;
                    ivm.fm = Utility.AddFamily(ivm.fm);
                    StudentModel s = new StudentModel();
                    s.ApplyGrade = (Grade)Convert.ToInt32(r.Student_Current_Grade);
                    s.ApplyYear = r.Current_School_Year;
                    s.BirthDate = Convert.ToDateTime(r.Student_DOB);
                    s.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                    s.CurrentGrade = (Grade)Convert.ToInt32(r.Student_Current_Grade);
                    s.FirstName = r.Firstname;
                    s.Gender = r.Sex;
                    s.isActive = true;
                    s.LastName = r.Lastname;
                    s.LearnAboutPRA = r.How_did_you_learn_about_PRA + "," + r.How_did_you_learn_about_PRA_choices;
                    s.LocalDistrict = r.District_Data.ToUpper() == "IN" ? "0803450" : "Other";
                    s.LocalSchool = "Other";
                    ivm.sm = s;
                    ivm.sm.FamilyID = ivm.fm.Id;
                    ivm.sm.UStudentID = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                    ivm.sm.Status = "Submitted";
                    if (r.Mother_Firstname.Trim().Length > 0)
                    {
                        ParentModel p = new ParentModel();
                        p.Address1 = r.Mother_Address;
                        p.Address2 = r.Mother_Apt_Unit_No;
                        p.City = r.Mother_City;
                        p.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        p.EmailAddress = r.Mother_Email;
                        p.FamilyID = ivm.fm.Id;
                        p.FirstName = r.Mother_Firstname;
                        p.isActive = true;
                        p.isPreferredContact = false;
                        p.LastName = r.Mother_Lastname;
                        p.Phone1 = r.Mother_Home_Phone;
                        p.Phone1Type = PhoneType.Home;
                        p.Phone2 = r.Mother_Cell_Phone;
                        p.Phone2Type = PhoneType.Cell;
                        p.pType = ParentType.Mother;
                        p.StateID = r.Mother_State;
                        p.ZipCode = r.Mother_Zip;
                        ivm.lsParents.Add(p);
                    }
                    if (r.Father_Firstname.Trim().Length > 0)
                    {
                        ParentModel p = new ParentModel();
                        p.Address1 = r.Father_Address;
                        p.Address2 = r.Father_Apt_Unit_No;
                        p.City = r.Father_City;
                        p.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        p.EmailAddress = r.Father_Email;
                        p.FamilyID = ivm.fm.Id;
                        p.FirstName = r.Father_Firstname;
                        p.isActive = true;
                        p.isPreferredContact = false;
                        p.LastName = r.Father_Lastname;
                        p.Phone1 = r.Father_Home_Phone;
                        p.Phone1Type = PhoneType.Home;
                        p.Phone2 = r.Father_Cell_Phone;
                        p.Phone2Type = PhoneType.Cell;
                        p.pType = ParentType.Father;
                        p.StateID = r.Father_State;
                        p.ZipCode = r.Father_Zip;
                        ivm.lsParents.Add(p);
                    }
                    if (r.Sib1_Name.Trim().Length > 0 && r.Sib1_DOB.HasValue)
                    {
                        SiblingModel sibmod = new SiblingModel();
                        sibmod.BirthDate = r.Sib1_DOB;
                        sibmod.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        sibmod.FamilyID = ivm.fm.Id;
                        sibmod.FirstName = r.Sib1_Name;
                        sibmod.isActive = true;
                        sibmod.isPRAStudent = r.Sib1_atPRA == "Y" ? true : false;
                        sibmod.LastName = r.Sib1_Name;
                        ivm.lsSiblings.Add(sibmod);
                    }
                    if (r.Sib2_Name.Trim().Length > 0 && r.Sib2_DOB.HasValue)
                    {
                        SiblingModel sibmod = new SiblingModel();
                        sibmod.BirthDate = r.Sib2_DOB;
                        sibmod.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        sibmod.FamilyID = ivm.fm.Id;
                        sibmod.FirstName = r.Sib2_Name;
                        sibmod.isActive = true;
                        sibmod.isPRAStudent = r.Sib2_atPRA == "Y" ? true : false;
                        sibmod.LastName = r.Sib2_Name;
                        ivm.lsSiblings.Add(sibmod);
                    }
                    if (r.Sib3_Name.Trim().Length > 0 && r.Sib3_DOB!= null)
                    {
                        SiblingModel sibmod = new SiblingModel();
                        sibmod.BirthDate = Convert.ToDateTime(r.Sib3_DOB);
                        sibmod.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        sibmod.FamilyID = ivm.fm.Id;
                        sibmod.FirstName = r.Sib3_Name;
                        sibmod.isActive = true;
                        sibmod.isPRAStudent = r.Sib3_atPRA == "Y" ? true : false;
                        sibmod.LastName = r.Sib3_Name;
                        ivm.lsSiblings.Add(sibmod);
                    }
                    if (r.Sib4_Name.Trim().Length > 0 && r.Sib4_DOB!=null)
                    {
                        SiblingModel sibmod = new SiblingModel();
                        sibmod.BirthDate = Convert.ToDateTime(r.Sib4_DOB);
                        sibmod.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        sibmod.FamilyID = ivm.fm.Id;
                        sibmod.FirstName = r.Sib4_Name;
                        sibmod.isActive = true;
                        sibmod.isPRAStudent = r.Sib4_atPRA == "Y" ? true : false;
                        sibmod.LastName = r.Sib4_Name;
                        ivm.lsSiblings.Add(sibmod);
                    }
                    if (r.Sib5_Name5.Trim().Length > 0 && r.Sib5_DOB!=null)
                    {
                        SiblingModel sibmod = new SiblingModel();
                        sibmod.BirthDate = Convert.ToDateTime(r.Sib5_DOB);
                        sibmod.CreateDate = Convert.ToDateTime(r.Timestamp == null ? DateTime.Now : r.Timestamp);
                        sibmod.FamilyID = ivm.fm.Id;
                        sibmod.FirstName = r.Sib5_Name5;
                        sibmod.isActive = true;
                        sibmod.isPRAStudent = r.Sib5_atPRA == "Y" ? true : false;
                        sibmod.LastName = r.Sib5_Name5;
                        ivm.lsSiblings.Add(sibmod);
                    }
                    Boolean isPRASibling = false;
                    foreach (SiblingModel sib in ivm.lsSiblings)
                    {
                        if (sib.isPRAStudent == true)
                        {
                            isPRASibling = true;
                        }
                        sib.FamilyID = ivm.fm.Id;
                    }
                    ivm.sm.isPRASibling = isPRASibling;
                    ivm.sm = Utility.AddStudent(ivm.sm);
                    List<ParentModel> lsDBParents = db.Parents.Where(x => x.FamilyID == ivm.fm.Id).ToList();
                    foreach (ParentModel p in lsDBParents)
                    {
                        if (!ivm.lsParents.Any(x => x.Id == p.Id))
                        {
                            db.Parents.Remove(p);
                            db.SaveChanges();
                        }
                    }
                    if (db.Siblings.Any(x => x.FamilyID == ivm.fm.Id))
                    {
                        List<SiblingModel> lsDBSiblings = db.Siblings.Where(x => x.FamilyID == ivm.fm.Id).ToList();
                        foreach (SiblingModel sm in lsDBSiblings)
                        {
                            if (!ivm.lsSiblings.Any(x => x.Id == sm.Id))
                            {
                                db.Siblings.Remove(sm);
                                db.SaveChanges();
                            }
                        }
                    }
                    if (ivm.lsParents.Count > 0)
                    {
                        ivm.lsParents = Utility.AddParents(ivm.lsParents);
                    }
                    if (ivm.lsSiblings.Count > 0)
                    {
                        ivm.lsSiblings = Utility.AddSiblings(ivm.lsSiblings);
                    }
                    Utility.UpdateIsPraSibling(ivm.sm.Id);
                    r.imported = true;
                    db.SaveChanges();
                    icounter++;
                    SendProgress("Importing Students...", icounter, lsr.Count);
                }
                catch (Exception e)
                {
                    r.ImportErrorMsg = e.Message;
                    db.SaveChanges();
                    icounter++;
                    SendProgress("Importing Students...", icounter, lsr.Count);
                }
            }
            icounter++;
            SendProgress("Importing Students...", lsr.Count, lsr.Count);
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
        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            //CALCULATING PERCENTAGE BASED ON THE PARAMETERS SENT
            var percentage = (progressCount * 100) / totalItems;

            //PUSHING DATA TO ALL CLIENTS
            hubContext.Clients.All.AddProgress(progressMessage, percentage + "%");
        }
    }
}
