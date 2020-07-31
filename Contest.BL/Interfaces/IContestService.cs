using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using System.Threading.Tasks;

namespace Contest.BL.Interfaces
{
    public interface IContestService
    {
        Task AddContest(AddContestDto dto);
        Task<PagedListDto<ContestDto>> GetContests(GetContestsDto dto);
        Task DeleteContest(int id);
        Task UpdateContest(ContestDto dto);
    }
}
