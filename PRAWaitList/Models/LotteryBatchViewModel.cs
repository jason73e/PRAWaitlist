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
        public SelectList SchoolYears { get; set; }
    }
}