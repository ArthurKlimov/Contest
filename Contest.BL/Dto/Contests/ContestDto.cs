using System;

namespace Contest.BL.Dto
{
    public class ContestDto
    {
        public ContestDto()
        {

        }

        public ContestDto(DateTime endDate, string title, string description, string link, string city, byte[] cover)
        {
            Title = title;
            EndDate = endDate;
            Description = description;
            Link = link;
            Cover = cover;
            City = city;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public byte[] Cover { get; set; }
        public string PublishDateString { get; set; }
        public string EndDateString { get; set; }
        public string CoverPath { get; set; }
        public bool IsPublished { get; set; }
        public int Views { get; set; }
        public string City { get; set; }


        public bool IsValid()
        {
            if (!string.IsNullOrWhiteSpace(Title) && Title?.Length <= 75 &&
                EndDate != null && (EndDate.Year == DateTime.UtcNow.Year || EndDate.Year == DateTime.UtcNow.Year + 1) &&
                Description?.Length <= 1500 &&
                !string.IsNullOrWhiteSpace(Link) && Link?.Length <= 75 &&
                !string.IsNullOrWhiteSpace(City) && City?.Length <= 50)
                return true;
            else
                return false;
        }
    }
}
