using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class PRAMenuModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string link { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string target { get; set; }

        public int sortorder { get; set; }
    }
}