using System;
using System.Threading.Tasks;
using Xunit;

namespace CoreTestTransactionScope
{
    public class PersonTests
    {
        [Fact]
        public async Task CreatePerson()
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
