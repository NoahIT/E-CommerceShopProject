using EuroTest.Helpers;
using Resutest.Helpers;
using System.Transactions;
using BL.Exceptions;
using DAL.Models;

namespace EuroTest
{
    public class RegisterTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                // validate: should not be in the DB
                authBL.ValidateEmail(email).GetAwaiter().GetResult();

                //createuser
                int userId = await authBL.CreateUser(
                    new UserModel() 
                    {
                        Email = email,
                        Password = "qwer1234"
                    });
                Assert.Greater(userId, 0);

                // validate: should not be in the DB
                Assert.Throws<DuplicateEmailException>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });
            }

        }
    }
}