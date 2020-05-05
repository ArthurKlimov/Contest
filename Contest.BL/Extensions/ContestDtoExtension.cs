using Contest.BL.Dto;

namespace Contest.BL.Extensions
{
    public static class ContestDtoExtension
    {
        public static bool Validate(this ContestDto dto)
        {
            if (dto.SmallDescription?.Length > 140 ||
                string.IsNullOrWhiteSpace(dto.SmallDescription) ||
                dto.FullDescription?.Length > 1500 ||
                string.IsNullOrWhiteSpace(dto.FullDescription) ||
                dto.StartDate > dto.EndDate ||
                dto.StartDate == default ||
                dto.EndDate == default ||
                string.IsNullOrWhiteSpace(dto.Link) ||
                dto.Link?.Length > 200 ||
                string.IsNullOrWhiteSpace(dto.Prize) ||
                dto.Prize?.Length > 140)
                return false;
            else
                return true;
        }
    }
}
