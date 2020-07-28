using Contest.BL.Dto;
using System.Threading.Tasks;

namespace Contest.BL.Interfaces
{
    public interface IContestService
    {
        Task AddContest(ContestDto dto);
        Task<PagedListDto<ContestDto>> GetContests(GetContestsDto dto);
        Task PublishContest(int id);
        Task HideContest(int id);
        Task DeleteContest(int id);
    }
}
