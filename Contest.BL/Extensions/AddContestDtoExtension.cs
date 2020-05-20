using Contest.BL.Dto.Contests;
using System;

namespace Contest.BL.Extensions
{
    public static class AddContestDtoExtension
    {
        private static readonly int currentYear = DateTime.UtcNow.Year;
        private static readonly int nextYear = DateTime.UtcNow.Year + 1;

        public static bool IsValid(this AddContestDto dto)
        {
            if ((dto.EndDate.Year == currentYear || dto.EndDate.Year == nextYear) &&
                !string.IsNullOrWhiteSpace(dto.SmallDescription) &&
                dto.SmallDescription?.Length <= 140 && 
                !string.IsNullOrWhiteSpace(dto.Link) &&
                dto.Link?.Length <= 140 &&
                dto.FullDescription?.Length <= 2500)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
