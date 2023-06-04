using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
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
        [Display(Name = "Grade")]
        [RegularExpression("^[1-8]|1[1-2]$", ErrorMessage = "Please select a grade.")]
        public Grade? BatchGrade { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUserID { get; set; }

    }
}