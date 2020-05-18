using Contest.BL.Enums;
using Contest.DA.Entities;

namespace Contest.BL.Dto
{
    public class GetContestsDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
        public ContestsSortType Sort { get; set; } = ContestsSortType.Popular;
        public ContestStatus Status { get; set; } = ContestStatus.Published;
    }
}
