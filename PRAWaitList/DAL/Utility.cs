using BotDetect.Web.Mvc;
using PRAWaitList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace PRAWaitList.DAL
{

    public static class Utility
    {
        public static SelectList GetStateList()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            List<StateModel> states = db.States.OrderBy(x => x.Name).ToList();
            //states.Insert(0, new StateModel() { Name = "- Select a State -", StateID = "" });
            SelectList sl = new SelectList(states, "StateID", "Name");
            db.Dispose();
            return (sl);
        }

        public static SelectList GetStatusList()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            List<StatusModel> status = db.StatusModels.OrderBy(x => x.Status).ToList();
            //states.Insert(0, new StateModel() { Name = "- Select a State -", StateID = "" });
            SelectList sl = new SelectList(status, "Status", "Status");
            db.Dispose();
            return (sl);
        }

        public static SelectList GetFullSchoolYearList()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            List<SchoolYearModel> schoolyears = db.SchoolYears.OrderBy(x => x.Name).ToList();
            //states.Insert(0, new StateModel() { Name = "- Select a State -", StateID = "" });
            db.Dispose();
            return (new SelectList(schoolyears, "Name", "Name"));
        }

        public static SelectList GetSchoolYearSelectList()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            List<SchoolYearModel> schoolyears = db.SchoolYears.OrderBy(x => x.Name).ToList();
            //states.Insert(0, new StateModel() { Name = "- Select a State -", StateID = "" });
            db.Dispose();
            return (new SelectList(schoolyears, "ID", "Name"));
        }

        public static SelectList GetSchoolYearList()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            List<SchoolYearModel> schoolyears = db.SchoolYears.Where(x => x.StartYear >= DateTime.Now.Year).OrderBy(x => x.Name).ToList();
            //states.Insert(0, new StateModel() { Name = "- Select a State -", StateID = "" });
            db.Dispose();
            return (new SelectList(schoolyears, "Name", "Name"));
        }

        public static SelectList GetSchoolDistrictList()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            var schools = db.Schools.Select(x => new { x.AgencyID, x.AgencyName }).Distinct().OrderBy(x => x.AgencyName).ToList();
            schools.Add(new { AgencyID = "Other", AgencyName = "Other School District" });
            SelectList sl = new SelectList(schools, "AgencyID", "AgencyName");
            db.Dispose();
            return (sl);
        }

        public static SelectList GetSchoolDistrictListForFamily(int iFamilyID)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            var Family = db.Families.Where(x => x.Id == iFamilyID).Single();
            string sStateCode = Family.StateID;
            SelectList sl = GetDistrictListByState(sStateCode);
            db.Dispose();
            return (sl);
        }

        public static SelectList GetDistrictListByState(string sStateCode)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            var schools = db.Schools.Where(x => x.StateAbbr == sStateCode).Select(x => new { x.AgencyID, x.AgencyName }).Distinct().OrderBy(x => x.AgencyName).ToList();
            schools.Add(new { AgencyID = "Other", AgencyName = "Other School District" });
            SelectList sl = new SelectList(schools, "AgencyID", "AgencyName");
            db.Dispose();
            return (sl);
        }

        public static string GetDistrictName(string DistrictCode)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            string DistrictName = db.Schools.Where(x => x.AgencyID == DistrictCode).Select(x => new { x.AgencyName }).Distinct().Single().AgencyName.ToString();
            db.Dispose();
            return (DistrictName);
        }
        public static string GetSchoolName(string sSchoolCode)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            string SchoolName = db.Schools.Where(x => x.SchoolID == sSchoolCode).Select(x => new { x.SchoolName }).Distinct().Single().SchoolName.ToString();
            db.Dispose();
            return (SchoolName);
        }

        public static SelectList GetSchoolListByDistrict(string sDistrictCode)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            var schools = db.Schools.Where(x => x.AgencyID == sDistrictCode).Select(x => new { x.SchoolID, x.SchoolName }).Distinct().OrderBy(x => x.SchoolName).ToList();
            schools.Add(new { SchoolID = "Other", SchoolName = "Other School " });
            SelectList sl = new SelectList(schools, "SchoolID", "SchoolName");
            db.Dispose();
            return (sl);
        }

        public static List<String> GetHearAboutPRAs()
        {
            PRAWaitListContext db = new PRAWaitListContext();
            var hearaboutpras = db.HearAboutPRAs.Select(x => x.Text).ToList();
            db.Dispose();
            return (hearaboutpras);
        }

        public static FamilyModel AddFamily(FamilyModel fm)
        {
            if (fm.Id > 0)
            {
                return UpdateFamily(fm);
            }
            else
            {
                PRAWaitListContext db = new PRAWaitListContext();
                if (!db.Families.Any(x => x.FamilyName == fm.FamilyName && x.Address1 == fm.Address1 && x.City == fm.City))
                {
                    fm.CreateDate = DateTime.Now;
                    fm.UpdateDate = DateTime.Now;
                    fm.IsActive = true;
                    db.Families.Add(fm);
                    db.SaveChanges();
                }
                FamilyModel rfm = db.Families.Where(x => x.FamilyName == fm.FamilyName && x.Address1 == fm.Address1 && x.City == fm.City).Single();
                db.Dispose();
                return (rfm);
            }
        }


        public static LotteryBatchModel AddLotteryBatch(LotteryBatchModel m)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            if (!db.LotteryBatches.Any(x => x.BatchName == m.BatchName && x.BatchType == m.BatchType && x.SchoolYearID == m.SchoolYearID))
            {
                m.CreateDate = DateTime.Now;
                m.UpdateDate = DateTime.Now;
                m.UpdateUserID = HttpContext.Current.User.Identity.Name;
                db.LotteryBatches.Add(m);
                db.SaveChanges();
            }
            LotteryBatchModel rm = db.LotteryBatches.Where(x => x.BatchName == m.BatchName && x.BatchType == m.BatchType && x.SchoolYearID == m.SchoolYearID).Single();
            db.Dispose();
            return (rm);
        }

        
        public static FamilyModel UpdateFamily(FamilyModel fm)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            FamilyModel familyModel = db.Families.Find(fm.Id);
            if (familyModel == null)
            {
                throw new Exception("Family Record Not Found");
            }
            familyModel.Address1 = fm.Address1;
            familyModel.Address2 = fm.Address2;
            familyModel.City = fm.City;
            familyModel.CreateDate = fm.CreateDate;
            familyModel.FamilyName = fm.FamilyName;
            familyModel.IsActive = fm.IsActive;
            familyModel.StateID = fm.StateID;
            familyModel.UpdateDate = DateTime.Now;
            familyModel.UpdateUserID = HttpContext.Current.User.Identity.Name;
            familyModel.ZipCode = fm.ZipCode;
            db.SaveChanges();

            FamilyModel rfm = db.Families.Find(fm.Id);
            db.Dispose();
            return (rfm);
        }

        public static void UpdateIsPraSibling(int? studentId)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            StudentModel studentModel = db.Students.Find(studentId);
            if (studentModel == null)
            {
                throw new Exception("Student Record Not Found");
            }
            int iFamilyID = studentModel.FamilyID;
            List<SiblingModel> ls = db.Siblings.Where(x => x.FamilyID == iFamilyID && x.isActive == true).ToList();
            Boolean isPRASibling = false;
            foreach(SiblingModel s in ls)
            {
                if(s.isPRAStudent)
                {
                    isPRASibling = true;
                }
            }
            studentModel.isPRASibling = isPRASibling;
            studentModel.UpdateDate = DateTime.Now;
            studentModel.UpdateUserID = HttpContext.Current.User.Identity.Name;
            db.SaveChanges();
            db.Dispose();
        }
        public static SiblingModel UpdateSibling(SiblingModel m)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            if(m.Id==0)
            {
               return AddSibling(m);
            }
            SiblingModel siblingModel = db.Siblings.Find(m.Id);
            if (siblingModel == null)
            {
                throw new Exception("Sibling Record Not Found");
            }
            siblingModel.BirthDate = m.BirthDate;
            siblingModel.CreateDate = m.CreateDate;
            siblingModel.FamilyID = m.FamilyID;
            siblingModel.FirstName = m.FirstName;
            siblingModel.isActive = m.isActive;
            siblingModel.isPRAStudent = m.isPRAStudent;
            siblingModel.LastName = m.LastName;
            siblingModel.UpdateDate = DateTime.Now;
            siblingModel.UpdateUserID = HttpContext.Current.User.Identity.Name;
            db.SaveChanges();

            SiblingModel rm = db.Siblings.Find(m.Id);
            db.Dispose();
            return (rm);
        }

        public static StudentModel UpdateStudent(StudentModel sm)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            StudentModel studentModel = db.Students.Find(sm.Id);
            if (studentModel == null)
            {
                throw new Exception("Student Record Not Found");
            }
            studentModel.ApplyGrade = sm.ApplyGrade;
            studentModel.ApplyYear = sm.ApplyYear;
            studentModel.BirthDate = sm.BirthDate;
            studentModel.CreateDate = sm.CreateDate;
            studentModel.CurrentGrade = sm.CurrentGrade;
            studentModel.FamilyID = sm.FamilyID;
            studentModel.FirstName = sm.FirstName;
            studentModel.Gender = sm.Gender;
            studentModel.isActive = sm.isActive;
            studentModel.isPRASibling = sm.isPRASibling;
            studentModel.LastName = sm.LastName;
            studentModel.LearnAboutPRA = sm.LearnAboutPRA;
            studentModel.LocalDistrict = sm.LocalDistrict;
            studentModel.LocalSchool = sm.LocalSchool;
            studentModel.Status = sm.Status;
            studentModel.UpdateDate = DateTime.Now;
            studentModel.UpdateUserID = HttpContext.Current.User.Identity.Name;
            
            db.SaveChanges();

            StudentModel rsm = db.Students.Find(sm.Id);
            db.Dispose();
            return (rsm);
        }

        public static ParentModel UpdateParent(ParentModel m)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            if (m.Id == 0)
            {
                return AddParent(m);
            }
            ParentModel parentModel = db.Parents.Find(m.Id);
            if (parentModel == null)
            {
                throw new Exception("Parent Record Not Found");
            }
            parentModel.Address1 = m.Address1;
            parentModel.Address2 = m.Address2;
            parentModel.City = m.City;
            parentModel.CreateDate = m.CreateDate;
            parentModel.EmailAddress = m.EmailAddress;
            parentModel.FamilyID = m.FamilyID;
            parentModel.FirstName = m.FirstName;
            parentModel.isActive = m.isActive;
            parentModel.isPreferredContact = m.isPreferredContact;
            parentModel.LastName = m.LastName;
            parentModel.Phone1 = m.Phone1;
            parentModel.Phone1Type = m.Phone1Type;
            parentModel.Phone2 = m.Phone2;
            parentModel.Phone2Type = m.Phone2Type;
            parentModel.pType = m.pType;
            parentModel.StateID = m.StateID;
            parentModel.ZipCode = m.ZipCode;
            parentModel.UpdateDate = DateTime.Now;
            parentModel.UpdateUserID = HttpContext.Current.User.Identity.Name;
            db.SaveChanges();

            ParentModel rm = db.Parents.Find(m.Id);
            db.Dispose();
            return (rm);
        }

        public static StudentModel AddStudent(StudentModel sm)
        {
            if (sm.Id > 0)
            {
                return UpdateStudent(sm);
            }
            else
            {
                PRAWaitListContext db = new PRAWaitListContext();
                if (!db.Students.Any(x => x.FamilyID == sm.FamilyID && x.FirstName == sm.FirstName && x.LastName == sm.LastName && x.BirthDate == sm.BirthDate))
                {
                    sm.CreateDate = DateTime.Now;
                    sm.UpdateDate = DateTime.Now;
                    sm.isActive = true;
                    db.Students.Add(sm);
                    db.SaveChanges();
                }
                StudentModel rsm = db.Students.Where(x => x.FamilyID == sm.FamilyID && x.FirstName == sm.FirstName && x.LastName == sm.LastName && x.BirthDate == sm.BirthDate).Single();
                db.Dispose();
                return (rsm);
            }
        }

        public static List<ParentModel> AddParents(List<ParentModel> lsParents)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            int iFamilyID = lsParents[0].FamilyID;
            foreach (ParentModel pm in lsParents)
            {
                if (!db.Parents.Any(x => x.FamilyID == pm.FamilyID && x.FirstName == pm.FirstName && x.LastName == pm.LastName))
                {
                    pm.CreateDate = DateTime.Now;
                    pm.UpdateDate = DateTime.Now;
                    pm.isActive = true;
                    AddParent(pm);
                }
            }
            List<ParentModel> rlsParents = db.Parents.Where(x => x.FamilyID == iFamilyID).ToList();
            db.Dispose();
            return (rlsParents);
        }

        public static ParentModel AddParent(ParentModel pm)
        {
            if (pm.Id > 0)
            {
                return UpdateParent(pm);
            }
            else
            {
                PRAWaitListContext db = new PRAWaitListContext();
                if (db.Parents.Any(x => x.FamilyID == pm.FamilyID && x.FirstName == pm.FirstName && x.LastName == pm.LastName))
                {
                    pm = db.Parents.Where(x => x.FamilyID == pm.FamilyID && x.FirstName == pm.FirstName && x.LastName == pm.LastName).Single();
                    pm.UpdateDate = DateTime.Now;
                    pm.isActive = true;
                    db.SaveChanges();

                }
                else
                {
                    pm.CreateDate = DateTime.Now;
                    pm.UpdateDate = DateTime.Now;
                    pm.isActive = true;
                    db.Parents.Add(pm);
                    db.SaveChanges();
                }
                pm = db.Parents.Where(x => x.FamilyID == pm.FamilyID && x.FirstName == pm.FirstName && x.LastName == pm.LastName).Single();
                db.Dispose();
                return pm;
            }
        }

        public static List<SiblingModel> AddSiblings(List<SiblingModel> lsSiblings)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            int iFamilyID = lsSiblings[0].FamilyID;
            foreach (SiblingModel sm in lsSiblings)
            {
                if (!db.Siblings.Any(x => x.FamilyID == sm.FamilyID && x.FirstName == sm.FirstName && x.LastName == sm.LastName))
                {
                    sm.CreateDate = DateTime.Now;
                    sm.UpdateDate = DateTime.Now;
                    sm.isActive = true;
                    AddSibling(sm);
                }
            }
            List<SiblingModel> rlsSiblings = db.Siblings.Where(x => x.FamilyID == iFamilyID).ToList();
            db.Dispose();
            return (rlsSiblings);
        }
        public static SiblingModel AddSibling(SiblingModel sm)
        {
            if (sm.Id > 0)
            {
                return UpdateSibling(sm);
            }
            else
            {
                PRAWaitListContext db = new PRAWaitListContext();
                if (db.Siblings.Any(x => x.FamilyID == sm.FamilyID && x.FirstName == sm.FirstName && x.LastName == sm.LastName))
                {
                    sm = db.Siblings.Where(x => x.FamilyID == sm.FamilyID && x.FirstName == sm.FirstName && x.LastName == sm.LastName).Single();
                    sm.UpdateDate = DateTime.Now;
                    sm.isActive = true;
                    db.SaveChanges();

                }
                else
                {
                    sm.CreateDate = DateTime.Now;
                    sm.UpdateDate = DateTime.Now;
                    sm.isActive = true;
                    db.Siblings.Add(sm);
                    db.SaveChanges();
                }
                sm = db.Siblings.Where(x => x.FamilyID == sm.FamilyID && x.FirstName == sm.FirstName && x.LastName == sm.LastName).Single();
                db.Dispose();
                return sm;
            }
        }
        public static MvcCaptcha GetEnrollCaptcha()
        {
            // create the control instance
            MvcCaptcha enrollCaptcha = new MvcCaptcha("enrollCaptcha");

            // set up client-side processing of the Captcha code input textbox
            enrollCaptcha.UserInputID = "CaptchaCode";

            // Captcha settings
            enrollCaptcha.ImageSize = new System.Drawing.Size(255, 50);

            return enrollCaptcha;
        }

        public static void SendMail(MailMessage m)
        {
            PRAWaitListContext db = new PRAWaitListContext();
            EmailQueueModel eq = new EmailQueueModel();
            eq.MessageBCC = GetDelimitedString(m.Bcc.ToList());
            eq.MessageBody = m.Body;
            eq.MessageCC = GetDelimitedString(m.CC.ToList());
            eq.MessageIsHtml = m.IsBodyHtml;
            eq.MessageSubject = m.Subject;
            eq.MessageTo = GetDelimitedString(m.To.ToList());
            eq.QueueDate = DateTime.Now;
            eq.StatusDate = DateTime.Now;
            eq.StatusModel = "Ready";
            eq.RecipientCount = GetRecipientCount(eq.MessageBCC, eq.MessageCC, eq.MessageTo);
            db.EmailQueues.Add(eq);
            db.SaveChanges();
        }

        private static int GetRecipientCount(string messageBCC, string messageCC, string messageTo)
        {
            int iTotalCount = 0;
            int iCCCount = 0;
            int iBCCCount = 0;
            int iToCount = 0;
            if (messageBCC == string.Empty)
            {
                iBCCCount = 0;
            }
            else
            {
                iBCCCount = messageBCC.Split(',').Length;
            }
            if (messageCC == string.Empty)
            {
                iCCCount = 0;
            }
            else
            {
                iCCCount = messageCC.Split(',').Length;
            }
            if (messageTo == string.Empty)
            {
                iToCount = 0;
            }
            else
            {
                iToCount = messageTo.Split(',').Length;
            }
            iTotalCount = iCCCount + iBCCCount + iToCount;
            return iTotalCount;
        }

        public static string GetDelimitedString(List<MailAddress> ls)
        {
            string rtv = string.Empty;
            foreach(MailAddress ma in ls)
            {
                rtv += ma.Address + ",";
            }
            if(rtv.EndsWith(","))
            {
                rtv = rtv.Substring(0, rtv.Length - 1);
            }
            return rtv;
        }
    }
}