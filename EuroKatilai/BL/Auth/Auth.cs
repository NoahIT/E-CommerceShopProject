using BL.General;
using System.ComponentModel.DataAnnotations;
using BL.Exceptions;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IWebCookie webCookie;


        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IWebCookie webCookie,
            IDbSession dbSession,
            IUserTokenDAL userTokenDAL)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.userTokenDAL = userTokenDAL;
            this.webCookie = webCookie;
        }

        public async Task Login(int id)
        {
            SessionModel sessionData = await dbSession.GetSession();
            Guid oldSessionId = sessionData.DbSessionId;

            await dbSession.SetUserId(id);

            var roles = await authDal.GetRoles(id);
            if (roles.Any(m => m.Abbreviation == AuthConstants.ADMIN_ROLE_ABBR))
            {
                dbSession.AddValue(AuthConstants.ADMIN_ROLE_KEY, AuthConstants.ADMIN_ROLE_ABBR);
                await dbSession.UpdateSessionData();
            }

            if (roles.Any(m => m.Abbreviation == AuthConstants.USER_ROLE_ABBR))
            {
                dbSession.AddValue(AuthConstants.USER_ROLE_KEY, AuthConstants.USER_ROLE_ABBR);
                await dbSession.UpdateSessionData();
            }
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);

                if (rememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
                }

                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);

            int id = await authDal.CreateUser(user);

            //await authDal.CreateAppUserAppRole(new AppUserAppRole()
            //{
            //    AppRoleId = 2,
            //    AppUserId = id
            //});

            await Login(id);
            return id;
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
                throw new DuplicateEmailException();
        }

        public async Task<int> Register(UserModel user)
        {
            int id = 0;

            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                id =  await CreateUser(user);
                scope.Complete();
            }

            return id;
        }
    }
}
