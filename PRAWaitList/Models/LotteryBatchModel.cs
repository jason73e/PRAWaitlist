using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public enum LotteryBatchType
    {
        [Display(Name ="Kindergarten Lottery")]
        KindergartenLottery = 1,
        [Display(Name = "Primary Lottery")]
        PrimaryLottery = 2 
    }
    public class LotteryBatchModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Name")]
        [Required]
        public string BatchName { get; set; }
        [Required]
        [Display(Name = "School Year")]
        public int SchoolYearID { get; set; }
        [Required]
        [Range(1, 2, ErrorMessage = "Please select a batch type.")]
        [Display(Name = "Batch Type")]
        public LotteryBatchType BatchType { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUserID { get; set; }

    }
}