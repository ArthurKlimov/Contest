using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contest.DA.Entities
{
    [Table("Contests")]
    public class ContestEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }
        public string City { get; set; }
        public bool AcrossCountry { get; set; }
    }
}
