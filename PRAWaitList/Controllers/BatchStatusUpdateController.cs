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
using PagedList;
using System.Configuration;
using System.Net.Mail;
using Microsoft.AspNet.SignalR;

namespace PRAWaitList.Controllers
{
    [System.Web.Mvc.Authorize]
    public class BatchStatusUpdateController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: RenewStudents
        public ActionResult Index(string sortOrder, string currentStatus, string SearchStatus, string currentApplyYear, string SearchApplyYear, int? page, int? PageSize)
        {
            TempData["MyRSModel"] = null;
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

            if (SearchStatus != null)
            {
                page = 1;
            }
            else
            {
                SearchStatus = currentStatus;
            }

            if (SearchApplyYear != null)
            {
                page = 1;
            }
            else
            {
                SearchApplyYear = currentApplyYear;
            }

            ViewBag.CurrentApplyYear = SearchApplyYear;
            ViewBag.CurrentStatus = SearchStatus;

            var students = db.Students.Where(x => x.isActive == true);

            if (!String.IsNullOrEmpty(SearchStatus))
            {
                students = students.Where(s => s.Status == SearchStatus);
            }

            if (!String.IsNullOrEmpty(SearchApplyYear))
            {
                students = students.Where(s => s.ApplyYear == SearchApplyYear);
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

            var model = new StudentSelectionViewModel();
            foreach (var student in students)
            {
                var editorViewModel = new SelectStudentEditorViewModel()
                {
                    Id = student.Id,
                    LastName = student.LastName,
                    FirstName = student.FirstName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender,
                    CurrentGrade = student.CurrentGrade,
                    ApplyGrade = student.ApplyGrade,
                    ApplyYear = student.ApplyYear,
                    Status = student.Status,
                    isPRASibling = student.isPRASibling,
                    Selected = false
                };
                model.Students.Add(editorViewModel);
            }
            BatchStatusUpdateViewModel m = new BatchStatusUpdateViewModel();
            m.pagedLsStudents = model.Students.ToPagedList(pageNumber, DefaultPageSize);
            m.displayForPaginglsStudents = m.pagedLsStudents.ToList();
            m.lsStudents = model.Students;
            m.StatusList = Utility.GetStatusList();
            m.ApplyYearList = Utility.GetFullSchoolYearList();
            m.SearchApplyYear = SearchApplyYear;
            m.SearchStatus = SearchStatus;
            TempData["MyBSUModel"] = m;
            return View(m);
        }

        [HttpPost]
        public ActionResult UpdateStudentsStatus(BatchStatusUpdateViewModel m)
        {
            BatchStatusUpdateViewModel bsuvm = m;
            var students = db.Students.Where(x => x.isActive == true);

            if (!String.IsNullOrEmpty(bsuvm.SearchStatus))
            {
                students = students.Where(s => s.Status == bsuvm.SearchStatus);
            }

            if (!String.IsNullOrEmpty(bsuvm.SearchApplyYear))
            {
                students = students.Where(s => s.ApplyYear == bsuvm.SearchApplyYear);
            }
            StudentSelectionViewModel ssvm = new StudentSelectionViewModel();
            if (bsuvm.displayForPaginglsStudents != null && bsuvm.displayForPaginglsStudents.Count > 0)
            {
                ssvm.Students = bsuvm.displayForPaginglsStudents.ToList();
                List<int> selectedIds = ssvm.getSelectedIds().ToList();
                students = students.Where(x => selectedIds.Contains(x.Id));

                int i = 0;
                int itemsCount = students.Count();
                foreach (StudentModel s in students)
                {
                    s.Status = bsuvm.NewStatus;
                    Utility.UpdateStudent(s);
                }
            }
            return RedirectToAction("Index");
        }
        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            if (totalItems > 0)
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
}
