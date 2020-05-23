﻿using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using System.Threading.Tasks;

namespace Contest.BL.Interfaces
{
    public interface IContestService
    {
        Task<int> AddContest(AddContestDto dto);
        //Task<ContestDto> GetContest(BaseContestDto dto);
        Task <PagedListDto<ContestDto>> GetContests(GetContestsDto dto);
        //Task EditContest(ContestDto dto);
        //Task DeleteContest(BaseContestDto dto);
    }
}
