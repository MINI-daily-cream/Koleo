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
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateAccount_Returns_True_When_VerifyAccount_Passes()
        {
            var email = "mini@pw.edu.pl";
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            databaseServiceMock.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ReturnsAsync((new List<string[]> { new string[] { "0" } }, true));

            var userService = new UserService(databaseServiceMock.Object);
            var result = await userService.CreateAccount("Name", "Surname", email, "password", null);
            Assert.True(result);
        }

        [Fact]
        public async Task CreateAccount_Returns_False_When_VerifyAccount_Fails()
        {
            var email = "mini@pw.edu.pl";
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            databaseServiceMock.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ReturnsAsync((new List<string[]> { new string[] { "1" } }, true));

            var userService = new UserService(databaseServiceMock.Object);
            var result = await userService.CreateAccount("Name", "Surname", email, "password", null);
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveAccount_Returns_True_When_Successful()
        {
            var userId = Guid.NewGuid();
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            databaseServiceMock.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ReturnsAsync((null, true));

            var userService = new UserService(databaseServiceMock.Object);
            var result = await userService.RemoveAccount(userId);
            Assert.True(result);
        }

        [Fact]
        public async Task UserExists_Returns_False_When_User_Does_Not_Exist()
        {
            var email = "mini@pw.edu.pl";
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            databaseServiceMock.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ReturnsAsync((new List<string[]> { }, true));

            var userService = new UserService(databaseServiceMock.Object);
            var result = await userService.UserExists(email);
            Assert.False(result);
        }
    }
}
