using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class StudentSelectionViewModel
    {
        public List<SelectStudentEditorViewModel> Students { get; set; }

        public StudentSelectionViewModel()
        {
            this.Students = new List<SelectStudentEditorViewModel>();
        }

        public IEnumerable<int> getSelectedIds()
        {
            return (from s in this.Students where s.Selected select s.Id).ToList();
        }
    }
}