using PagedList;
using System.Web.Mvc;
using PRAWaitList.DAL;
namespace PRAWaitList.Models
{
    public class WaitlistAdminViewModel
    {
        public IPagedList<StudentModel> lsStudents {get;set;}
        public Grade? SearchGrade { get; set; }

        public string SearchYear { get; set; }

        public string SearchStatus { get; set; }
        public SelectList StatusList { get; set; }
        public SelectList SchoolYearList = Utility.GetSchoolYearList();
    }
}