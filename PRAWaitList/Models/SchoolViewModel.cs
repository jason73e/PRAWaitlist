using PagedList;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class SchoolViewModel
    {
        public IPagedList<SchoolModel> lsSchools { get; set; }
        public SelectList DistrictList { get; set; }
        public SelectList StateList { get; set; }
        public string SearchState { get; set; }
        public string SearchDistrict { get; set; }

    }
}