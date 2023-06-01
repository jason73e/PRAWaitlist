using PagedList;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class BatchStatusUpdateViewModel
    {
        public IPagedList<SelectStudentEditorViewModel> lsStudents { get; set; }
        public string SearchApplyYear { get; set; }
        public SelectList ApplyYearList { get; set; }

        public string SearchStatus { get; set; }

        public string NewStatus { get; set; }
        public SelectList StatusList { get; set; }
    }
}