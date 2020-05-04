using Contest.BL.Dto;
using System.Threading.Tasks;

namespace Contest.BL.Interfaces
{
    public interface IContestService
    {
        Task AddContest (ContestDto dto);
        //Task GetContest();
        //Task GetAllContests();
        //Task EditContest();
        //Task DeleteContest();
        //Task HideContest();
    }
}
