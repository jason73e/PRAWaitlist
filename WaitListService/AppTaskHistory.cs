using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitListService
{
    public class AppTaskHistory
    {

        [Key]
        public int uid { get; set; }
        public DateTime ts { get; set; }
        public int taskid { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string status { get; set; }

    }
}
