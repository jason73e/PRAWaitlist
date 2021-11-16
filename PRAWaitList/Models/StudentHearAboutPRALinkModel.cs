using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class StudentHearAboutPRALinkModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public int HPRAId { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Display(Name = "Referral Name")]
        public string ReferralName { get; set; }
        [Display(Name = "Referral Email")]
        public string ReferralEmail { get; set; }
        [Display(Name = "Other")]
        public string OtherText { get; set; }
    }

}