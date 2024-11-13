using DAL.Models;

namespace BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetUserIdByToken();
        Task<int?> GetCurrentUserId();

        Task<ProfileModel> GetProfiles();
        bool IsAdmin();

        void Logout();
        Task DeleteNotUsebaleSessions();
        Task DeleteAccount(int userId);
        Task<UserPasswordSalt> GetCurrentUserPassword();
    }
}
