using BL.General;
using System.Text.Json;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private readonly IWebCookie webCookie;

        private SessionModel? sessionModel = null;
        private Dictionary<string, object>? SessionData = null;

        public DbSession(IDbSessionDAL sessionDAL, IWebCookie webCookie)
        {
            this.sessionDAL = sessionDAL;
            this.webCookie = webCookie;
        }

        private void CreateSessionCookie(Guid sessionid)
        {
            webCookie.Delete(AuthConstants.SessionCookieName);
            webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
        }

        private async Task<SessionModel> CreateSession()
        {
            var data = new SessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow
            };
            await sessionDAL.Create(data);
            return data;
        }

        public async Task<SessionModel> GetSession()
        {
            if (sessionModel != null)
                return sessionModel;

            Guid sessionId;
            var sessionString = webCookie.Get(AuthConstants.SessionCookieName);
            if (sessionString != null)
                sessionId = Guid.Parse(sessionString);
            else
                sessionId = Guid.NewGuid();


            var data = await sessionDAL.Get(sessionId);
            if (data == null)
            {
                data = await CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }


            sessionModel = data;
            if (data.SessionData != null)
            {
                SessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(data.SessionData) ?? new Dictionary<string, object>();
            }
            else
            {
                SessionData = new Dictionary<string, object>();
            }

            await sessionDAL.Extend(data.DbSessionId);
            return data;
        }

        public async Task UpdateSessionData()
        {
            if (SessionData != null && sessionModel != null)
                await sessionDAL.Update(sessionModel.DbSessionId, JsonSerializer.Serialize(SessionData));
            else
                throw new Exception("Nera sesijos");
        }

        public void AddValue(string key, object value)
        {
            if (SessionData == null)
                throw new Exception("Nera sesijos");
            if (SessionData.ContainsKey(key))
                SessionData[key] = value;
            else
                SessionData.Add(key, value);
        }

        public void RemoveValue(string key)
        {
            if (SessionData == null)
                throw new Exception("Сессия не загружена");
            if (SessionData.ContainsKey(key))
                SessionData.Remove(key);
        }

        public object GetValueDef(string key, object defaultValue)
        {
            if (SessionData == null)
                throw new Exception("Сессия не загружена");
            if (SessionData.ContainsKey(key))
                return SessionData[key];
            return defaultValue;
        }

        public async Task SetUserId(int userId)
        {
            var data = await GetSession();
            data.UserId = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);

            data.SessionData = JsonSerializer.Serialize(SessionData);
            await sessionDAL.Create(data);
        }

        public async Task<int?> GetUserId()
        {
            var data = await GetSession();
            return data.UserId;
        }

        public async Task<bool> IsLoggedIn()
        {
            var data = await GetSession();

            return data.UserId != null;
        }

        public async Task Lock()
        {
            await GetSession();
            if (sessionModel == null)
                throw new Exception("Сессия не загружена");
            await sessionDAL.Lock((Guid)sessionModel.DbSessionId);
        }

        public void ResetSessionCache()
        {
            sessionModel = null;
        }

        public async Task DeleteSessionId()
        {
            await GetSession();
            if (sessionModel != null)
                await sessionDAL.Delete((Guid)sessionModel.DbSessionId);
        }

        public async Task DeleteSessionsLastAccessed()
        {
            await sessionDAL.DeleteSessionsWith5DayNotAccessed();
        }

        public async Task DeleteAccount(int userId)
        {
            await sessionDAL.DeleteAccount(userId);
        }

        public async Task<UserPasswordSalt> GetPasswordByUserId(int userId)
        {
           return await sessionDAL.GetPasswordByUserId(userId);
        }

        public async Task UpdatePassword(string password)
        {
            var model = await GetUserId();

           await sessionDAL.UpdatePassword(model ?? 0, password);
        }
    }

}
