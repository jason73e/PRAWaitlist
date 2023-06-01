using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class SelectStudentEditorViewModel
    {
        public bool Selected { get; set; }
        public int Id { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public Grade CurrentGrade { get; set; }
        public Grade ApplyGrade { get; set; }
        public string ApplyYear { get; set; }
        public string Status { get; set; }
        public bool isPRASibling { get; set; }

    }
}