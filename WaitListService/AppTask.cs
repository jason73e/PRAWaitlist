using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitListService
{
    public class AppTask
    {
        [Key]
        public int uid { get; set; }
        public string Name { get; set; }
        public string ExePath { get; set; }
        public DateTime NextRun { get; set; }
        public int RepeatInterval { get; set; }
        public string RepeatUnit { get; set; }
        public bool active { get; set; }
        public bool running { get; set; }

    }
}
