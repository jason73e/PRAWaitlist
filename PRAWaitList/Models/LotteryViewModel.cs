using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class LotteryViewModel
    {
        public int LotteryBatchID { get; set; }

        public int NotifyExpireHours { get; set; }

        public PagedList.IPagedList<LotteryModel> lsLM { get; set; }

        public List<StudentModel> lsSM { get; set; }
    }
}