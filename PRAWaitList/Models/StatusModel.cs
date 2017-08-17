using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class StatusModel
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }
    }
}