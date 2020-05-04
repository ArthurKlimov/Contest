using System;

namespace Contest.DA.Entities
{
    public class ContestEntity : BaseEntity
    {
        public string SmallDescription { get; set; }
        public string FullDescription { get; set; }
        public string Link { get; set; }
        public string Prize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContestStatus Status { get; set; }
        public int Views { get; set; }
        public string Cover { get; set; }
    }

    public enum ContestStatus
    {
        Created,
        OnModeration,
        Published,
        Hidden,
        Closed
    }
}
