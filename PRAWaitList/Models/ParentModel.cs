using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public enum ParentType
    {
        Mother = 1, Father = 2
    }

    public enum PhoneType
    {
        Home = 1, Work = 2, Cell = 3
    }

    public class ParentModel
    {
        [Key]
        public int Id { get; set; }
        public int FamilyID { get; set; }
        [Required]
        [DisplayName("Parent Type")]
        [Range(1, (int)ParentType.Father, ErrorMessage = "Please select a Parent Type.")]
        public ParentType pType { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [DisplayName("Phone 1")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }
        [Required]
        [DisplayName("Phone Type 1")]
        [Range(1, (int)PhoneType.Cell, ErrorMessage = "Please select a Phone Type.")]
        public PhoneType Phone1Type { get; set; }
        [Required]
        [DisplayName("Phone 2")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }
        [Required]
        [DisplayName("Phone Type 2")]
        [Range(1, (int)PhoneType.Cell, ErrorMessage = "Please select a Phone Type.")]
        public PhoneType Phone2Type { get; set; }
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
        [Display(Name="State")]
        public string StateID { get; set; }
        [StringLength(50)]
        [Required]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Primary Contact")]
        public Boolean isPreferredContact { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public Boolean isActive { get; set; }
    }
}