using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class SchoolYearModel
    {
        public int ID { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public String Name { get; set; }

    }
}