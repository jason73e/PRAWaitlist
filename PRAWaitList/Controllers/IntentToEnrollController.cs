using PRAWaitList.DAL;
using PRAWaitList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.UI;
using BotDetect.Web.Mvc;
using System.Text.RegularExpressions;
using System.Net;

namespace PRAWaitList.Controllers
{
    public class IntentToEnrollController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();
        // GET: IntentToEnroll
        public ActionResult Index(string id,string DisplayMessage)
        {
            if (!string.IsNullOrEmpty(DisplayMessage))
            {
                ViewBag.DisplayMessage = DisplayMessage;
            }
            IntentToEnrollViewModel ievm = new IntentToEnrollViewModel();
            if (id == null)
            {
                ievm.fm = new FamilyModel();
                ievm.sm = new StudentModel();
                ievm.lsParents = new List<ParentModel>();
                ievm.lsSiblings = new List<SiblingModel>();
                ievm.lsParents.Add(new ParentModel());
                ievm.lsLearnPRAList = Utility.GetHearAboutPRAs();
                ievm.States = Utility.GetStateList();
                ievm.DistrictList = Utility.GetDistrictListByState("");
                ievm.SchoolList = Utility.GetSchoolListByDistrict("");
            }
            else
            {
                StudentModel studentModel = db.Students.Where(x => x.UStudentID == id).Single();
                if (studentModel == null)
                {
                    return HttpNotFound("Student Record Not Found");
                }
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

                ievm.sm = studentModel;
                ievm.fm = familyModel;
                ievm.lsParents = lsp;
                ievm.lsSiblings = lss;
                ievm.lsLearnPRAList = Utility.GetHearAboutPRAs();
                ievm.States = Utility.GetStateList();
                ievm.DistrictList = Utility.GetDistrictListByState(ievm.fm.StateID);
                ievm.SchoolList = Utility.GetSchoolListByDistrict(ievm.sm.LocalDistrict);
                ievm.StatusList = Utility.GetStatusList();

            }
            if (!(TempData["MyIEVMModel"]==null))
            {
                ievm = (IntentToEnrollViewModel)TempData["MyIEVMModel"];
            }
            TempData["MyIEVMModel"] = ievm;
            return View(ievm);
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "enrollCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Index(IntentToEnrollViewModel ivm, string AddToList, string AddParent, string RemoveParent,string AddSibling, string RemoveSibling)
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

            if (!string.IsNullOrEmpty(AddToList))
            {
                if (ModelState.IsValid)
                {
                   return RedirectToAction("Confirm");
                }
            }
            return View(ivm);
        }

        public ActionResult Confirm()
        {
            IntentToEnrollViewModel ievm = new IntentToEnrollViewModel();
            ievm.fm = new FamilyModel();
            ievm.sm = new StudentModel();
            ievm.lsParents = new List<ParentModel>();
            ievm.lsSiblings = new List<SiblingModel>();
            ievm.lsParents.Add(new ParentModel());
            ievm.lsLearnPRAList = Utility.GetHearAboutPRAs();
            ievm.States = Utility.GetStateList();
            ievm.DistrictList = Utility.GetDistrictListByState("");
            ievm.SchoolList = Utility.GetSchoolListByDistrict("");
            TempData["SiblingCounter"] = 0;
            if (!(TempData["MyIEVMModel"] == null))
            {
                ievm = (IntentToEnrollViewModel)TempData["MyIEVMModel"];
            }
            TempData["MyIEVMModel"] = ievm;
            return View(ievm);
        }
        [HttpPost]
        public ActionResult Confirm(IntentToEnrollViewModel ivm)
        {
            ivm = (IntentToEnrollViewModel)TempData["MyIEVMModel"];
            ivm.fm = Utility.AddFamily(ivm.fm);
            ivm.sm.FamilyID = ivm.fm.Id;
            ivm.sm.UStudentID = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            ivm.sm.Status = "Submitted";
            foreach (ParentModel p in ivm.lsParents)
            {
                p.FamilyID = ivm.fm.Id;
            }
            Boolean isPRASibling = false;
            foreach (SiblingModel s in ivm.lsSiblings)
            {
                if (s.isPRAStudent == true)
                {
                    isPRASibling = true;
                }
                s.FamilyID = ivm.fm.Id;
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
                foreach (SiblingModel s in lsDBSiblings)
                {
                    if (!ivm.lsSiblings.Any(x => x.Id == s.Id))
                    {
                        db.Siblings.Remove(s);
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
            string sFromEmail = System.Configuration.ConfigurationManager.AppSettings["WaitListEmailAddress"].ToString();
            string sToEmail = string.Empty;
            sToEmail = ivm.lsParents[0].EmailAddress;
            foreach (ParentModel p in ivm.lsParents)
            {
                if (p.isPreferredContact)
                {
                    sToEmail = p.EmailAddress;
                }
            }
            //display confirmation on screen.
            ViewBag.DisplayMessage = "Your Student has been added to the Wait List.  You will receive confirmation messages and updates from " + sFromEmail;
            //send email confirmation
            try
            {
                SendConfirmationEmail(sFromEmail, sToEmail);
            }
            catch (Exception e)
            {
                ViewBag.DisplayMessage = ViewBag.DisplayMessage + ".  Send Email Function Failed: " + e.Message;
            }
            MvcCaptcha.ResetCaptcha("enrollCaptcha");
            ivm = new IntentToEnrollViewModel();
            ivm.fm = new FamilyModel();
            ivm.sm = new StudentModel();
            ivm.lsParents = new List<ParentModel>();
            ivm.lsSiblings = new List<SiblingModel>();
            ivm.lsParents.Add(new ParentModel());
            ivm.lsLearnPRAList = Utility.GetHearAboutPRAs();
            ivm.States = Utility.GetStateList();
            ivm.DistrictList = Utility.GetDistrictListByState("");
            ivm.SchoolList = Utility.GetSchoolListByDistrict("");
            TempData["SiblingCounter"] = 0;
            TempData["MyIEVMModel"] = ivm;
            return RedirectToAction("Index",new { DisplayMessage = ViewBag.DisplayMessage});
        }

        private IntentToEnrollViewModel GetFormData(IntentToEnrollViewModel ivm)
        {
            if (TempData["MyIEVMModel"] == null)
            {
                ivm.fm = new FamilyModel();
                ivm.sm = new StudentModel();
                ivm.lsParents = new List<ParentModel>();
                ivm.lsSiblings = new List<SiblingModel>();
                ivm.lsLearnPRAList = Utility.GetHearAboutPRAs();
                ivm.States = Utility.GetStateList();
                ivm.DistrictList = Utility.GetDistrictListByState("");
                ivm.SchoolList = Utility.GetSchoolListByDistrict("");
            }
            else
            {
                ivm = (IntentToEnrollViewModel)TempData["MyIEVMModel"];
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
                                string sdate = Request.Form.Get(sKey);
                                DateTime dBDate;
                                if (DateTime.TryParse(sdate, out dBDate))
                                {
                                    ivm.sm.BirthDate = dBDate;
                                }
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
                        }
                    }
                    else if (sModel.StartsWith("Parent"))
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
                    else if (sModel.StartsWith("Sibling"))
                    {
                        int iSiblingCount = 0;
                        string sSiblingCount = sKey.Split('.')[1];
                        sSiblingCount = sSiblingCount.Replace("lsSiblings[", string.Empty);
                        sSiblingCount = sSiblingCount.Replace("]", string.Empty);
                        iSiblingCount = Convert.ToInt32(sSiblingCount);
                        switch (sField)
                        {
                            case "BirthDate":
                                string sdate = Request.Form.Get(sKey);
                                DateTime dBDate;
                                if (DateTime.TryParse(sdate, out dBDate))
                                {
                                    ivm.lsSiblings[iSiblingCount].BirthDate = dBDate;
                                }
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
            TempData["MyIEVMModel"] = ivm;
            return ivm;

        }

        private ActionResult SendConfirmationEmail(string sFromEmail, string sToEmail)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(sToEmail)); //replace with valid value
            message.Subject = "Student Added to Waitlist.";
            message.Body = string.Format(body, "PRA WaitList", sFromEmail, "Your student has been added to the Waitlist.  You will be notified if selected in the lottery.  You will also receive annual emails asking you update your information to stay on the waitlist.");
            message.IsBodyHtml = true;
            SendEmail(message);
            return View();
        }

        private ActionResult SendEmail(MailMessage m)
        {
            Utility.SendMail(m);
            return View();
        }

        public ViewResult AddSibling(IntentToEnrollViewModel ivm)
        {
            ivm.lsSiblings.Add(new SiblingModel());
            TempData["MyIEVMModel"] = ivm;
            return View("Index", ivm);

        }

        public ViewResult RemoveSibling(IntentToEnrollViewModel ivm, string sMySiblingPrefix)
        {
            int iSiblingIndex = Convert.ToInt32(sMySiblingPrefix.Replace("Sibling", string.Empty))-1;
            ivm.lsSiblings.RemoveAt(iSiblingIndex);
            TempData["MyIEVMModel"] = ivm;
            return View("Index", ivm);
        }

        public ViewResult AddParent(IntentToEnrollViewModel ivm)
        {
            ivm.lsParents.Add(new ParentModel());
            TempData["MyIEVMModel"] = ivm;
            return View("Index",ivm);
        }

        public ViewResult RemoveParent(IntentToEnrollViewModel ivm)
        {
            ivm.lsParents.RemoveAt(1);
            TempData["MyIEVMModel"] = ivm;
            return View("Index", ivm);
        }

        public JsonResult GetDistricts(string sStateCode)
        {
            IntentToEnrollViewModel ivm = (IntentToEnrollViewModel)TempData["MyIEVMModel"];
            SelectList sl = Utility.GetDistrictListByState(sStateCode);
            ivm.DistrictList = sl;
            TempData["MyIEVMModel"] = ivm;
            return Json(sl, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchools(string sDistrictCode)
        {
            IntentToEnrollViewModel ivm = (IntentToEnrollViewModel)TempData["MyIEVMModel"];
            SelectList sl = Utility.GetSchoolListByDistrict(sDistrictCode);
            ivm.SchoolList = sl;
            TempData["MyIEVMModel"] = ivm;
            return Json(sl, JsonRequestBehavior.AllowGet);
        }

    }
}