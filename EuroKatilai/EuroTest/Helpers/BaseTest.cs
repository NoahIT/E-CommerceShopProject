using System;
using BL.Auth;
using BL.General;
using DAL;
using DAL.Auth;
using DAL.Interfaces;
using DAL.Profile;
using Microsoft.AspNetCore.Http;
using Resutest.Helpers;

namespace EuroTest.Helpers
{
	public class BaseTest
	{
        protected IAuthDAL authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
		protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected CurrentUser currentUser;
        protected IProfileDAL profileDAL = new ProfileDAL();

        public BaseTest()
		{
            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            authBL = new Auth(authDal, encrypt, webCookie, dbSession,userTokenDAL);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, profileDAL);
        }
	}
}

