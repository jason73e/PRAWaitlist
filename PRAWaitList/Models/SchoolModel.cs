﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class SchoolModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String SchoolName { get; set; }
        [Required]
        public String StateName { get; set; }
        [Required]
        public String StateAbbr { get; set; }
        [Required]
        public String SchoolID { get; set; }
        [Required]
        [Display(Name ="District")]
        public String AgencyName { get; set; }
        [Required]
        [Display(Name = "DistrictID")]
        public String AgencyID { get; set; }
    }
}