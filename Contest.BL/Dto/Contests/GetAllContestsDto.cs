namespace Contest.BL.Dto
{
    public class GetAllContestsDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public bool IsPopular { get; set; }
        public bool IsNew { get; set; }
        public bool IsAlmostClosed { get; set; }
    }
}
