using Contest.BL.Enums;
using Contest.DA.Entities;
using System;

namespace Contest.BL.Dto
{
    public class GetContestsDto
    {
        public GetContestsDto()
        {

        }

        public GetContestsDto(ContestsSortType sortType, string search, int pageNumber, int pageSize)
        {
            Sort = sortType;
            Search = search;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public ContestsSortType Sort { get; set; }
    }
}
