using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PRAWaitList.Models
{
    public class SchoolYearModel
    {
        public int ID { get; set; }
        [DisplayName("Start Year")]
        public int StartYear { get; set; }
        [DisplayName("End Year")]
        public int EndYear { get; set; }
        public String Name { get; set; }

    }
}