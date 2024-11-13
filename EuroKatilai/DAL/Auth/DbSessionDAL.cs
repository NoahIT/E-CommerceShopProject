using DAL.Interfaces;
using DAL.Models;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        private readonly IDbHelper dbHelper;

        public DbSessionDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task Create(SessionModel model)
        {
            string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionID, @SessionData, @Created, @LastAccessed, @UserId)";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
            var sessions = await dbHelper.QueryAsync<SessionModel?>(sql, new { sessionId });
            return sessions.FirstOrDefault();
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";
            await dbHelper.QueryAsync<SessionModel>(sql, new { sessionId });
        }

        public async Task Update(Guid dbSessionID, string sessionData)
        {
            string sql = @"update DbSession
                    set SessionData = @sessionData
                    where DbSessionID = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID, sessionData });
        }

        public async Task Extend(Guid dbSessionID)
        {
            string sql = @"update DbSession
                    set LastAccessed = @lastAccessed
                    where DbSessionID = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID, lastAccessed = DateTime.UtcNow });
        }

        public async Task<string?> GetSessionData(int? userid)
        {
            if (userid == null)
                return null;

            string sql = @"select SessionData from DbSession where UserId = @sessionId AND SessionData != '{}' AND SessionData != '{}'";
            var sessions = await dbHelper.QueryScalarAsync<string>(sql, new { sessionId = userid });
            return sessions;
        }

        public async Task Delete(Guid sessionId)
        {
            string sql = @"delete from DbSession where DbSessionID = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID = sessionId });
        }

        public async Task DeleteSessionsWith5DayNotAccessed()
        {
            string sql = @"DELETE FROM DbSession
                            WHERE LastAccessed < UTC_TIMESTAMP() - INTERVAL 1 DAY
                            AND UserId IS NULL";

            await dbHelper.ExecuteAsync(sql, new { });
        }

        public async Task DeleteAccount(int userId)
        {
            string sql = @"delete from AppUser where UserId = @userId";

            await dbHelper.ExecuteAsync(sql, new { userId = userId });
        }

        public async Task<UserPasswordSalt> GetPasswordByUserId(int userId)
        {
            string sql = @"select Password, Salt from AppUser where UserId = @userId";

            return await dbHelper.QueryScalarAsync<UserPasswordSalt>(sql, new { userId = userId });
        }
        public async Task UpdatePassword(int userId, string password)
        {
            string sql = @"update AppUser
                            set Password = @password
                            where UserId = @userId";

            await dbHelper.ExecuteAsync(sql,new { password=password, userId = userId});
        }
    }
}
