using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class FamilyModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Family Last Name")]
        [StringLength(50)]
        public string FamilyName { get; set; }
        [StringLength(150)]
        [Required]
        [DisplayName("Address Line 1")]
        public string Address1 { get; set; }
        [StringLength(50)]
        [DisplayName("Address Line 2")]
        public string Address2 { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(3)]
        [DisplayName("State")]
        public string StateID { get; set; }
        [StringLength(50)]
        [Required]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public Boolean IsActive { get; set; }        
    }
}