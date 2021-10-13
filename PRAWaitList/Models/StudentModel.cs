using PRAWaitList.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public enum Grade
    {
        First=1, Second=2, Third=3, Fourth=4, Fifth=5, Sixth=6, Seventh=7, Eighth=8, None = 9, PreSchool = 10, Kindergarten = 11
    }


    public class StudentModel
    {
        public StudentModel()
        {
            BirthDate = DateTime.Now.AddYears(-5);
        }

        [Key]
        public int Id { get; set; }
        public int FamilyID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Current Grade")]
        [Range(1,(int)Grade.Kindergarten,ErrorMessage ="Please select a grade.")]
        public Grade CurrentGrade { get; set; }
        [Required]
        [Display(Name = "Grade Applying For")]
        [Range(1, (int)Grade.Kindergarten, ErrorMessage = "Please select a grade.")]
        public Grade ApplyGrade { get; set; }
        [Required]
        [Display(Name = "School Year Applying For")]
        public string ApplyYear { get; set; }
        [Required]
        [Display(Name = "Local School Name")]
        public string LocalSchool { get; set; }
        [Required]
        [Display(Name = "Local School District")]
        public string LocalDistrict { get; set; }
        public string Status { get; set; }
        [Required]
        [Display(Name = "How did you learn about PRA?")]
        public string LearnAboutPRA { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UStudentID { get; set; }
        public string UpdateUserID { get; set; }
        public Boolean isActive { get; set; }
        [Required]
        [Display(Name = "Has Sibling at PRA?")]
        public Boolean isPRASibling { get; set; }

        private PRAWaitListContext db = new PRAWaitListContext();

        public Boolean isParentStaff()
        {
            Boolean retValue = false;
            List<ParentModel> lsParents = db.Parents.Where(x => x.FamilyID == this.FamilyID).ToList();
            foreach(ParentModel p in lsParents)
            {
                if(p.isStaff)
                {
                    retValue = true;
                }
            }
            return retValue;
        }

        public Boolean isParentSAC()
        {
            Boolean retValue = false;
            List<ParentModel> lsParents = db.Parents.Where(x => x.FamilyID == this.FamilyID).ToList();
            foreach (ParentModel p in lsParents)
            {
                if (p.isSAC)
                {
                    retValue = true;
                }
            }
            return retValue;
        }

    }
}