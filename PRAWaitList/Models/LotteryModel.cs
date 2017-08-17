using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class LotteryModel
    {
        [Key]
        public int Id { get; set; }
        public int LotteryBatchId { get; set; }
        public int StudentId { get; set; }
        public int RandomID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime? NotifyDate { get; set; }
        public DateTime? AcceptDate { get; set; }
        public DateTime? DeclineDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        public string Status { get; set; }

        public string ApplyYear { get; set; }

        public Grade? ApplyGrade { get; set; }
    }
}