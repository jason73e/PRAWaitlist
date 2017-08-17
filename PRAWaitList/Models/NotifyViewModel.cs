using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class NotifyViewModel
    {
        public LotteryModel lm { get; set; }
        public IntentToEnrollViewModel ievm { get; set; }
    }
}