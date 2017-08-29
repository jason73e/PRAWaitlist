using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PRAWaitList.Models
{
    public class EmailQueueModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime QueueDate { get; set; }

        public string StatusModel { get; set; }

        public string ErrorMessage { get; set;}

        public DateTime StatusDate { get; set; }
        public int RecipientCount { get; set; }
        public string MessageTo { get; set; }
        public string MessageSubject { get; set; }
        public string MessageCC { get; set; }

        public string MessageBCC { get; set; }
        public string MessageBody { get; set; }
        public bool MessageIsHtml { get; set; }

    }
}