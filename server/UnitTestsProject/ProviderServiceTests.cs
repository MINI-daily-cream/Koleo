using API.Services.Interfaces;
using Koleo.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsProject
{
    public class ProviderServiceTests
    {
        [Fact]
        public async Task AddProvider_Returns_True_When_Provider_Added_Successfully()
        {
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var providerService = new ProviderService(mockDatabaseService.Object);
            var result = await providerService.AddProvider("providerName");
            Assert.True(result);
        }

        [Fact]
        public async Task EditProvider_Returns_True_When_Provider_Edited_Successfully()
        {
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var providerService = new ProviderService(mockDatabaseService.Object);
            var result = await providerService.EditProvider("providerId", "newName");
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProvider_Returns_True_When_Provider_Removed_Successfully()
        {
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var providerService = new ProviderService(mockDatabaseService.Object);
            var result = await providerService.RemoveProvider("providerId");
            Assert.True(result);
        }
    }
}
