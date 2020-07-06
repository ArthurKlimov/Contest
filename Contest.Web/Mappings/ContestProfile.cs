using AutoMapper;
using Contest.BL.Dto;
using Contest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contest.Web.Mappings
{
    public class ContestProfile : Profile
    {
        public ContestProfile()
        {
            CreateMap<AddContest, ContestDto>();
        }
    }
}
