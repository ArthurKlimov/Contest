using System.Threading.Tasks;

namespace Contest.BL.Interfaces
{
    public interface IImageService
    {
        Task UploadContestCover(byte[] coverImageBytes, string coverImageType, int contestId);
    }
}
