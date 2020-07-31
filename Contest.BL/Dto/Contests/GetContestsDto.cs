using Contest.BL.Dto.Contests;

namespace Contest.BL.Dto
{
    public class GetContestsDto : BaseContestDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public string City { get; set; }
        public bool IsPublished { get; set; }

        public override bool IsDtoValid()
        {
            if (PageNumber <= 0)
                return false;

            if (PageSize <= 0)
                return false;

            if (Search.Length > 100)
                return false;

            if (Sort != "Popular" && Sort != "Old" &&
                Sort != "AlmostClosed" && Sort != "New")
                return false;

            if (City.Length > 20)
                return false;

            return true;
        }
    }
}
