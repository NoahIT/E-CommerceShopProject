using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);

        Task<UserModel> GetUser(int id);

        Task<int> CreateUser(UserModel model);

        Task<IEnumerable<AppRoleModel>> GetRoles(int appUserId);
        Task CreateAppUserAppRole(AppUserAppRole model);
    }
}
