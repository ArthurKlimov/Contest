using Contest.BL.Dto;
using System.Collections.Generic;

namespace Contest.Web.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
        }

        public HomeViewModel(List<ContestDto> contests, string sort, string search)
        {
            Sort = sort;
            Search = search;
            Contests = contests;
        }

        public string Sort { get; set; }
        public string Search { get; set; }
        public List<ContestDto> Contests { get; set; }
    }
}
