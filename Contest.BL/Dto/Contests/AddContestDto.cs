using Contest.DA.Entities;
using System;

namespace Contest.BL.Dto.Contests
{
    public class AddContestDto
    {
        public DateTime EndDate { get; set; }
        public string SmallDescription { get; set; }
        public string FullDescription { get; set; }
        public string Link { get; set; }
    }
}
