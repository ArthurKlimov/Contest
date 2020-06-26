namespace Contest.BL.Dto
{
    public class GetContestsDto
    {
        public GetContestsDto()
        {

        }

        public GetContestsDto(string sort, string search)
        {
            Sort = sort;
            Search = search;
            PageNumber = 1;
            PageSize = 20;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }

        public bool IsValid()
        {
            if (PageNumber > 0 && PageSize > 0)
                return true;
            else
                return false;
        }
    }
}
