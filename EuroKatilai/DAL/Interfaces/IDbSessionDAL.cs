using DAL.Models;

namespace DAL.Interfaces
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);

        Task Update(Guid dbSessionID, string sessionData);

        Task Extend(Guid dbSessionID);

        Task Create(SessionModel model);

        Task Lock(Guid sessionId);
        Task<string?> GetSessionData(int? userid);
        Task Delete(Guid sessionId);
        Task DeleteSessionsWith5DayNotAccessed();
        Task DeleteAccount(int userId);
        Task<UserPasswordSalt> GetPasswordByUserId(int userId);
        Task UpdatePassword(int userId, string password);
    }
}
