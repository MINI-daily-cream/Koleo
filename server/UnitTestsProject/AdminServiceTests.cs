////using API.Services.Interfaces;
////using Koleo.Services;
////using Moq;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

////namespace UnitTestsProject
////{
////    public class AdminServiceTests
////    {
////        [Fact]
////        public async Task RemoveAccount_Returns_True_When_Account_Removed_Successfully()
////        {
////            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
////            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
////                               .ReturnsAsync((null, true));

////            var adminService = new AdminService(mockDatabaseService.Object);
////            var result = await adminService.RemoveAccount(Guid.NewGuid());
////            Assert.True(result);
////        }

////        [Fact]
////        public async Task AuthoriseAccount_Returns_True_When_Authorized()
////        {
////            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
////            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
////                               .ReturnsAsync((new List<string[]> { new string[] { "password" } }, true));

////            var adminService = new AdminService(mockDatabaseService.Object);
////            var result = await adminService.AuthoriseAccount("john@example.com", "password");
////            Assert.True(result);
////        }

      
////        [Fact]
////        public async Task RejectAdminRequest_Returns_True_When_Request_Rejected()
////        {
////            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
////            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
////                               .ReturnsAsync((null, true));

////            var adminService = new AdminService(mockDatabaseService.Object);
////            var result = await adminService.RejectAdminRequest("userId");

////            Assert.True(result);
////        }

////        [Fact]
////        public async Task DeleteUser_Returns_True_When_User_Deleted_Successfully()
////        {
////            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
////            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
////                               .ReturnsAsync((null, true));

////            var adminService = new AdminService(mockDatabaseService.Object);
////            var result = await adminService.DeleteUser("userId");

////            Assert.True(result);
////        }
////    }
////}
