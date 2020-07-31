using AutoMapper;
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.DA.Entities;

namespace Contest.BL.Mappings
{
    public class ContestProfile : Profile
    {
        public ContestProfile()
        {
            CreateMap<ContestEntity, ContestDto>();
            CreateMap<ContestDto, ContestEntity>();
            CreateMap<AddContestDto, ContestEntity>();
        }
    }
}
