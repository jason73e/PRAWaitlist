using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PRAWaitList.Models
{
    public class EmailControlModel
    {
        [Key]
        public int ID { get; set; }
        public string FromAddress { get; set; }

        public string SMTPDeliveryMethod { get; set; }

        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPassword { get; set; }
        public bool SMTPEnableSSL { get; set; }

        public int SMTPSendLimit { get; set; }
        public bool SMTPisActive { get; set; }
    }
}