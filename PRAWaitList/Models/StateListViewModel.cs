using PagedList;
using System.Web.Mvc;

namespace PRAWaitList.Models
{
    public class StateListViewModel
    {
        public IPagedList<StateModel> lsStates { get; set; }
    }
}