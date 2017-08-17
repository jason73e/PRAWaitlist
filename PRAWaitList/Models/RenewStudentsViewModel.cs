using PagedList;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class RenewStudentsViewModel
    {
        public IPagedList<StudentModel> lsStudents { get; set; }
        public string SearchApplyYear { get; set; }
        public SelectList ApplyYearList { get; set; }

        public string SearchStatus { get; set; }
        public SelectList StatusList { get; set; }
    }
}