using System;

namespace Contest.BL.Dto
{
    public class ContestDto
    {
        public ContestDto()
        {

        }

        public ContestDto(DateTime endDate, string title, string link, string city, bool acrossCountry, byte[] cover)
        {
            Title = title;
            EndDate = endDate;
            Link = link;
            Cover = cover;
            City = city;
            AcrossCountry = acrossCountry;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Link { get; set; }
        public byte[] Cover { get; set; }
        public string PublishDateString { get; set; }
        public string EndDateString { get; set; }
        public string CoverPath { get; set; }
        public bool IsPublished { get; set; }
        public int Views { get; set; }
        public string City { get; set; }
        public bool AcrossCountry { get; set; }
    }
}
