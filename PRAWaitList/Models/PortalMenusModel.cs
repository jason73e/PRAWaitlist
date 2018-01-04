using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRAWaitList.Models
{
    public class PortalMenusModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string MenuName { get; set; }
        [Required]
        public string ControllerName { get; set; }

        [Required]
        public string ActionName { get; set; }

        [Required]
        public string MenuType { get; set; }

        [Required]
        public int ParentID { get; set; }

        [Required]
        public int Sortorder { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}