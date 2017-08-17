using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class ConfigurationSettingsModel
    {
        [Key]
        public string key { get; set; }
        public string value { get; set; }
    }
}