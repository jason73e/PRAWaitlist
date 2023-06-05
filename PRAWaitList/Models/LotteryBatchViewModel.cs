using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class LotteryBatchViewModel
    {
        public List<LotteryBatchModel> lbms { get; set; }
        public List<LotteryViewModel>lslvm { get; set; }
        public List<int> lsCount { get; set; }
        public List<int> lsNotify { get; set; }
        public List<int> lsExpired { get; set; }
        public List<int> lsDeclined { get; set;}
        public List<int> lsAccepted { get; set; }
        public SelectList SchoolYears { get; set; }

    }
}