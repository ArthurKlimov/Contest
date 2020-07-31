using System;

namespace Contest.BL.Dto.Contests
{
    public class AddContestDto : BaseContestDto
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string City { get; set; }
        public string Organizator { get; set; }
        public bool IsAcrossCountry { get; set; }
        public DateTime EndDate { get; set; }

        public override bool IsDtoValid()
        {
            if (string.IsNullOrWhiteSpace(Title) || Title?.Length < 10 || Title?.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(Link) || Link?.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(Organizator) || Organizator?.Length > 20)
                return false;

            if ((!string.IsNullOrWhiteSpace(City) && IsAcrossCountry) || (string.IsNullOrWhiteSpace(City) && !IsAcrossCountry))
                return false;

            if (City?.Length > 20)
                return false;

            if (EndDate == default || EndDate.Year < DateTime.Now.Year)
                return false;

            return true;
        }
    }
}
