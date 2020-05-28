using Contest.BL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contest.Web.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
        }

        public HomeViewModel(PagedListDto<ContestDto> contests, string sort, string search)
        {
            Contests = contests;
            Sort = sort;
            Search = search;
        }

        public PagedListDto<ContestDto> Contests { get; set; }
        public string Sort { get; set; }
        public string Search { get; set; }
    }
}
