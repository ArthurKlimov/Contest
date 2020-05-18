using Contest.BL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contest.Web.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(List<ContestDto> contests)
        {
            Contests = new List<ContestDto>();
            Contests = contests;
        }

        public List<ContestDto> Contests { get; set; }
    }
}
