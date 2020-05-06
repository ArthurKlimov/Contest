using Contest.DA.Entities;
using System;

namespace Contest.BL.Dto.Contests
{
    public class AddContestDto
    {
        public string SmallDescription { get; set; }
        public string FullDescription { get; set; }
        public string Link { get; set; }
        public string Prize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PublishDate { get; set; }
        public ContestStatus Status { get; set; }
        public int Views { get; set; }
        public string Cover { get; set; }
    }
}
