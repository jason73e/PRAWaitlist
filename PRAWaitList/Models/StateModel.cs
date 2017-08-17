using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class StateModel
    {
        [Key]
        public String StateID { get; set; }
        [Required]
        public String Name { get; set; }

    }
}