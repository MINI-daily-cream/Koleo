using FakeItEasy;
using FluentAssertions;
using Koleo.Services;
using KoleoPL.Services;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Domain;
using API.Services.Interfaces;
using Koleo.Models;

namespace UnitTests.AccountServiceUnitTests
{
    public class AccountServiceTests
    {
        private readonly AccountService _accountService;
        private readonly Mock<IDatabaseServiceAPI> _databaseservice;

        public AccountServiceTests()
        {
            _databaseservice = new Mock<IDatabaseServiceAPI>();

            //SUT
            _accountService = new AccountService(_databaseservice.Object);
        }
        [Fact]
        public async void AccountServices_GetAccountInfo_ReturnsSuccess()
        {
            Guid userId = Guid.NewGuid();
            string expectedName = "Adam";
            string expectedSurname = "Nowak";
            string expectedEmail = "adam.nowak@example.com";

            AccountInfo accountInfo = new AccountInfo(expectedName, expectedSurname, expectedEmail);
            List<string[]> accountInfoStr = new List<string[]> { new[] { expectedName, expectedSurname, expectedEmail } };

            _databaseservice.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ReturnsAsync(accountInfoStr);

            var result = await _accountService.GetAccountInfo(userId);

            Assert.NotNull(accountInfo);
            Assert.Equal(expectedName, accountInfo.Name);
            Assert.Equal(expectedSurname, accountInfo.Surname);
            Assert.Equal(expectedEmail, accountInfo.Email);
        }

        [Fact]
        public async void GetAccountInfo_Returns_Null_For_Invalid_UserId()
        {
            Guid userId = Guid.NewGuid();

            _databaseservice.Setup(service => service.ExecuteSQL(It.IsAny<string>()))
                .ReturnsAsync(new List<string[]>());

            var accountInfo = await _accountService.GetAccountInfo(userId);

            Assert.Null(accountInfo);
        }

        [Fact]
        public async void AccountServices_UpdateAccountInfo_ReturnsSuccess()
        {
            Guid userId = Guid.NewGuid();
            string newName = "Adam";
            string newSurname = "Nowak";
            string newEmail = "adam.nowak@example.com";

            List<string[]> accountInfoStr = new List<string[]> { new[] { newName, newSurname, newEmail } };
           
            _databaseservice.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ReturnsAsync(accountInfoStr);

            var result = await _accountService.UpdateAccountInfo(userId, newName, newSurname, newEmail);

            Assert.True(result);
        }
        [Fact]
        public async void AccountServices_UpdateAccountInfo_ReturnsFalse()
        {
            Guid userId = Guid.NewGuid();
            string newName = "Adam";
            string newSurname = "Nowak";
            string newEmail = "adam.nowak@example.com";

            _databaseservice.Setup(x => x.ExecuteSQL(It.IsAny<string>())).ThrowsAsync(new Exception("Database error"));

            var result = await _accountService.UpdateAccountInfo(userId, newName, newSurname, newEmail);

            Assert.False(result);
        }
    }
}
