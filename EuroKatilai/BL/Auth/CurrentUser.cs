using BL.General;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IProfileDAL profileDAL;
        private readonly IAddressDAL addressDal;


        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL,
            IProfileDAL profileDAL, IAddressDAL addressDal)
        {
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
            this.profileDAL = profileDAL;
            this.addressDal = addressDal;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;
            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
            if (tokenGuid == null)
                return null;

            int? userid = await userTokenDAL.Get((Guid)tokenGuid);
            return userid;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool isLoggedIn = await dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userid = await GetUserIdByToken();
                if (userid != null)
                {
                    await dbSession.SetUserId((int)userid);
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        }

        public async Task<int?> GetCurrentUserId()
        {
            return await dbSession.GetUserId();
        }

        public bool IsAdmin()
        {
            if (dbSession.GetValueDef(AuthConstants.ADMIN_ROLE_KEY, "").ToString() == AuthConstants.ADMIN_ROLE_ABBR)
                return true;
            return false;
        }

        public async Task<ProfileModel> GetProfiles()
        {
            int? userid = await GetCurrentUserId();
            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profile = await profileDAL.GetByUserId((int)userid);

            if (profile == null)
            {
                var addId = await addressDal.Create(new AddressModel() { UserId = (int)userid });
                var i = await profileDAL.Add(new ProfileModel() { UserId = (int)userid, AddressId = addId});
            }
            profile.AddressModel = await addressDal.GetAddressByUserId(userid);

            return profile ;
        }


        public void Logout()
        {
            dbSession.DeleteSessionId();
            webCookie.Delete(AuthConstants.SessionCookieName);

            Guid? tokenGuid = GetCurrentUserToken();
            if (tokenGuid != null)
            {
                userTokenDAL.DeleteToken((Guid)tokenGuid);
                webCookie.Delete(AuthConstants.RememberMeCookieName);
            }

            dbSession.ResetSessionCache();
        }

        private Guid? GetCurrentUserToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;
            return Helpers.StringToGuidDef(tokenCookie ?? "");
        }

        public async Task DeleteNotUsebaleSessions()
        {
            await dbSession.DeleteSessionsLastAccessed();
        }

        public async Task DeleteAccount(int userId)
        {
            await dbSession.DeleteAccount(userId);
        }

        public async Task<UserPasswordSalt> GetCurrentUserPassword()
        {
            var id = await GetCurrentUserId();

           return await dbSession.GetPasswordByUserId(id ?? 0);
        }

    }
}
