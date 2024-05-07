using API.Services.Interfaces;
using Koleo.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class AdminServiceTests
    {
       

        [Fact]
        public async Task RemoveAccount_Returns_True_When_Account_Removed_Successfully()
        {
            // Arrange
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adminService = new AdminService(mockDatabaseService.Object);

            // Act
            var result = await adminService.RemoveAccount(Guid.NewGuid());

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task RejectAdminRequest_Returns_True_When_Request_Rejected()
        {
            // Arrange
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adminService = new AdminService(mockDatabaseService.Object);

            // Act
            var result = await adminService.RejectAdminRequest("userId");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUser_Returns_True_When_User_Deleted_Successfully()
        {
            // Arrange
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adminService = new AdminService(mockDatabaseService.Object);

            // Act
            var result = await adminService.DeleteUser("userId");

            // Assert
            Assert.True(result);
        }

    }
}
