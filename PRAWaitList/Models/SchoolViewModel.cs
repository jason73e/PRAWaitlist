using PagedList;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class SchoolViewModel
    {
        public IPagedList<SchoolModel> lsSchools { get; set; }
    }
}