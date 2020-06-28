namespace Contest.BL.Dto
{
    public class GetContestsDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public string City { get; set; }

        public bool IsValid()
        {
            if (PageNumber > 0 && PageSize > 0)
                return true;
            else
                return false;
        }
    }
}
