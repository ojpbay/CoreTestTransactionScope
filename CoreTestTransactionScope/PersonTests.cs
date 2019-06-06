using System;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CoreTestTransactionScope
{
    public class PersonTests
    {
        [Fact]
        public async Task CreatePerson()
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var updater = new DatabaseUpdater();
                var result = await updater.CreatePersonAsync(new Person
                {
                    FullName = Guid.NewGuid().ToString(),
                    DateUpdated = DateTime.Now
                });

                Assert.True(result.Id > 0);
            }
        }
    }
}
