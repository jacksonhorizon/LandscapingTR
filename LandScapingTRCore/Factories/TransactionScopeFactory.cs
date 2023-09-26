using System.Transactions;

namespace LandscapingTR.Core.Factories
{
    public static class TransactionScopeFactory
    {
        public static TransactionScope createReadUncommitted()
        {
            return createReadUncommitted(TransactionScopeOption.Required);
        }

        public static TransactionScope createReadUncommitted(TransactionScopeOption scopeOption)
        {
            var options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };

            return new TransactionScope(scopeOption, options, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
