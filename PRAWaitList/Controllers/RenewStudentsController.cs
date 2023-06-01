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
    public class RenewStudentsController : Controller
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
            RenewStudentsViewModel m = new RenewStudentsViewModel();
            m.lsStudents = students.ToPagedList(pageNumber, DefaultPageSize);
            m.StatusList = Utility.GetStatusList();
            m.ApplyYearList = Utility.GetFullSchoolYearList();
            m.SearchApplyYear = SearchApplyYear;
            m.SearchStatus = SearchStatus;
            TempData["MyRSModel"] = m;
            return View(m);
        }


        public JsonResult RenewStudents()
        {
            RenewStudentsViewModel rsvm = (RenewStudentsViewModel)TempData["MyRSModel"];
            var students = db.Students.Where(x => x.isActive == true);

            if (!String.IsNullOrEmpty(rsvm.SearchStatus))
            {
                students = students.Where(s => s.Status == rsvm.SearchStatus);
            }

            if (!String.IsNullOrEmpty(rsvm.SearchApplyYear))
            {
                students = students.Where(s => s.ApplyYear == rsvm.SearchApplyYear);
            }
            int i = 0;
            int itemsCount = students.Count();
            foreach (StudentModel s in students)
            {
                SendProgress("Sending Email for "+ s.FirstName + " " + s.LastName +" ...", i, itemsCount);
                s.Status = "Renewal";
                IList<ParentModel> parents = db.Parents.Where(p => p.FamilyID == s.FamilyID && p.isActive == true).ToList();
                string sFromEmail = ConfigurationManager.AppSettings["WaitListEmailAddress"].ToString();
                string sToEmail = string.Empty;
                foreach (ParentModel p in parents)
                {
                    if (p.isPreferredContact)
                    {
                        sToEmail = p.EmailAddress;
                    }
                }
                if (sToEmail == string.Empty)
                {
                    if (parents.Count > 0)
                    {
                        ParentModel p = parents.First();
                        sToEmail = p.EmailAddress;
                    }
                }
                if(SendConfirmationEmail(sFromEmail, sToEmail, s))
                {
                    Utility.UpdateStudent(s);
                }

                i++;
            }
            SendProgress("Done Sending Emails...", itemsCount, itemsCount);
            TempData["MyRSModel"] = rsvm;
            return Json("", JsonRequestBehavior.AllowGet);
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

        private bool SendConfirmationEmail(string sFromEmail, string sToEmail, StudentModel s)
        {
            int iMaxRetries = 20;
            string shost = ConfigurationManager.AppSettings["WaitListURL"].ToString();
            string sContactName = ConfigurationManager.AppSettings["WaitListContactName"].ToString();
            string sContactEmail= ConfigurationManager.AppSettings["WaitListContactEmail"].ToString();
            var body = "<p>Dear Prospective PRA Parent,</p><p><BR> In order to keep your student on the Platte River Academy waitlist you will need to update your student and family information on an annual basis.  Please be sure to update the student grade and application year information.  Use the following link to update your record.</p><p>{0}/intenttoenroll/index?id={1}</p><p>So that you are aware, failure to update your information will result in your student being removed from the wait list.  If you have any questions please contact {2} at {3}.</p>";
            var message = new MailMessage();
            try
            {
                var addr = new System.Net.Mail.MailAddress(sToEmail);
            }
            catch
            {
                return false;
            }
            message.To.Add(new MailAddress(sToEmail)); //replace with valid value
            message.Subject = "Information update required to stay on Platte River Academy Waitlist.";
            message.Body = string.Format(body, shost,s.UStudentID,sContactName,sContactEmail);
            message.IsBodyHtml = true;
            bool bMessageSent = false;
            int iRetries = 0;
            while (!bMessageSent && iRetries<iMaxRetries)
            {
                bMessageSent=SendEmail(message);
                iRetries++;
            }
            return bMessageSent;
        }

        private bool SendEmail(MailMessage m)
        {
            try
            {
                Utility.SendMail(m);
                return true;
            }
            catch(Exception e)
            {
                
                return false;
            }
        }
    }
}
