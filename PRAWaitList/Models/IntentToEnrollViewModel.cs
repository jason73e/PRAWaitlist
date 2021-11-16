using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class IntentToEnrollViewModel
    {
        public FamilyModel fm { get; set; }
        public StudentModel sm { get; set; }
        public List<ParentModel> lsParents { get; set; }
        public List<SiblingModel> lsSiblings { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public List<HearAboutPRAModel> lsLearnPRAList { get; set; }

        public SelectList DistrictList { get; set; }

        public SelectList SchoolList { get; set; }
        public SelectList StatusList { get; set; }
    }
}