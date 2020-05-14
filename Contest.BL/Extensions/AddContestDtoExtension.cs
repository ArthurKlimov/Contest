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
            if ((dto.StartDate.Year == currentYear || dto.StartDate.Year == nextYear) &&
                (dto.EndDate.Year == currentYear || dto.EndDate.Year == nextYear) &&
                dto.EndDate >= dto.StartDate &&
                !string.IsNullOrWhiteSpace(dto.SmallDescription) &&
                !string.IsNullOrWhiteSpace(dto.FullDescription) &&
                dto.SmallDescription?.Length <= 140 && dto.FullDescription?.Length <= 2500)
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
