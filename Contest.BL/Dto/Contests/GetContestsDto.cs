using Contest.BL.Enums;
using Contest.DA.Entities;

namespace Contest.BL.Dto
{
    public class GetContestsDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public ContestsSortType Sort { get; set; }
    }
}
