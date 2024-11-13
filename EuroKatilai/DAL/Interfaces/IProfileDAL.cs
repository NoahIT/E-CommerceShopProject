using DAL.Models;

namespace DAL.Interfaces
{
    public interface IProfileDAL
    {
        Task<ProfileModel> Get(int userId);
        Task<int> Add(ProfileModel profile);
        Task Update(ProfileModel profile);
        Task<ProfileModel> GetByUserId(int userId);
    }
}
