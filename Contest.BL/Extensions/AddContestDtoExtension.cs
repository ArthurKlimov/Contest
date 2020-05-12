using Contest.BL.Dto.Contests;
using System;

namespace Contest.BL.Extensions
{
    public static class AddContestDtoExtension
    {
        public static bool IsValid(this AddContestDto dto)
        {
            if (dto.StartDate.Year >= 2020 &&
                dto.EndDate.Year >= 2020 &&
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
