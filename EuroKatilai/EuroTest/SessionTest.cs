using System;
using Resutest.Helpers;
using System.Transactions;
using EuroTest.Helpers;

namespace EuroTest
{
	public class SessionTest : BaseTest
    {
        [Test]
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var session = await this.dbSession.GetSession();

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSessoion);

                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }
    }
}

