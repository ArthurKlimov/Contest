using Contest.BL.Dto.Contests;
using System;

namespace Contest.BL.Dto
{
    public class ContestDto : AddContestDto
    {
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string CoverPath { get; set; }
        public bool IsPublished { get; set; }
        public string Views { get; set; }
    }
}
