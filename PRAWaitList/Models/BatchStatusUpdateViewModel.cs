using PagedList;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class BatchStatusUpdateViewModel
    {
        public IPagedList<SelectStudentEditorViewModel> pagedLsStudents { get; set; }
        public IList<SelectStudentEditorViewModel> lsStudents { get; set; }
        public IList<SelectStudentEditorViewModel> displayForPaginglsStudents { get; set; }
        public string SearchApplyYear { get; set; }
        public SelectList ApplyYearList { get; set; }

        public string SearchStatus { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="You must select a new status")]
        public string NewStatus { get; set; }
        public SelectList StatusList { get; set; }
    }
}