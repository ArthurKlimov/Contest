using Contest.BL.Dto.Contests;
using System;

namespace Contest.BL.Dto
{
    public class ContestDto : AddContestDto
    {
        public int Id { get; set; }
        public DateTime? PublishDate { get; set; }
        public string PublishDateString { get; set; }
        public string EndDateString { get; set; }
        public bool IsPublished { get; set; }
        public int Views { get; set; }
        public bool AcrossCountry { get; set; }
    }
}
