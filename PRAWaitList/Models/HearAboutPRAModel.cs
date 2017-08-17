using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class HearAboutPRAModel
    {
        [Key]
        [Required]
        public int Value { get; set; }
        [Required]
        public String Text { get; set; }
        [Required]
        public bool IsChecked { get; set; }
    }

    public class HearAboutPRAModelList
    {
        public List<HearAboutPRAModel> HearAboutPRAs { get; set; }
    }
}