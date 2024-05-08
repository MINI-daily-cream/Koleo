using API.Services.Interfaces;
using Domain;
using Koleo.Services;
using Moq;

namespace UnitTestsProject
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task GetAccountInfo_Returns_AccountInfo_When_User_Exists()
        {
            string userId = "userId";
            string name = "name";
            string surname = "surname";
            string email = "mini@pw.edu.pl";
            var expectedResult = new AccountInfo(name, surname, email);

            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((new List<string[]> { new string[] { name, surname, email } }, true));

            var accountService = new AccountService(mockDatabaseService.Object);
            var result = await accountService.GetAccountInfo(userId);
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.Surname, result.Surname);
            Assert.Equal(expectedResult.Email, result.Email);
        }

        [Fact]
        public async Task GetAccountInfo_Returns_Null_When_User_Does_Not_Exist()
        {
            string userId = "nonExistingUserId";

            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((new List<string[]> { }, true));

            var accountService = new AccountService(mockDatabaseService.Object);
            var result = await accountService.GetAccountInfo(userId);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAccountInfo_Returns_True_When_Update_Successful()
        {
            string userId = "userId";
            string newName = "NewName";
            string newSurname = "NewSurname";
            string newEmail = "email@edu.pl";

            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));
            var accountService = new AccountService(mockDatabaseService.Object);
            var result = await accountService.UpdateAccountInfo(userId, newName, newSurname, newEmail);
            Assert.True(result);
        }
    }
}