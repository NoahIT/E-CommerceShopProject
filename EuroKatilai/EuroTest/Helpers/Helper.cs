using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EuroTest.Helpers
{
    static class Helper
    {
        public static TransactionScope CreateTransactionScope(int seconds = 99999)
        {
            return new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0,0,seconds), TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
