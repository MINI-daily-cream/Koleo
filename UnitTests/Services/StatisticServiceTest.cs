using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Koleo.Services;
using Koleo.Models;
using Domain;
using System.Collections.Generic;
using API.Services.Interfaces;
using System.Security.Cryptography;

namespace Koleo.Tests
{
    [TestFixture]
    public class StatisticsServiceTests
    {
        [Test]
        public async Task GetByUser_ValidUserID_ReturnsStatisticsInfo()
        {
            // Arrange
            string userId = "someUserId";
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            var statisticsService = new StatisticsService(databaseServiceMock.Object);
            var expectedStatisticsInfo = new StatisticsInfo("userData0", "userData1", "userData2", "userData3", "userData4", "userData5", "userData6");

            
            databaseServiceMock.Setup(x => x.ExecuteSQLLastRow(It.IsAny<string>()))
                .ReturnsAsync((new List<object[]> { new object[] { "userData0", "userData1", "userData2", "userData3", "userData4", "userData5", "userData6" } }, true));

            // Act
            var result = await statisticsService.GetByUser(userId);

            // Assert
           NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.User_Id, Is.EqualTo(expectedStatisticsInfo.User_Id));

            
        }

        [Test]
        public async Task Update_ExistingUser_SuccessfullyUpdatesStatistics()
        {
            // Arrange
            string userId = "someUserId";
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            var statisticsService = new StatisticsService(databaseServiceMock.Object);
            var connectionInfoObject = new ConnectionInfoObject { KmNumber = 10, TrainNumber = "10", Duration = TimeSpan.FromMinutes(30) };

            // Mocking ExecuteSQLLastRow to return existing user data
            databaseServiceMock.Setup(x => x.ExecuteSQLLastRow(It.IsAny<string>()))
                .ReturnsAsync((new List<object[]> { new object[] { "1", "someUserId", "1", "2", "0", "00:00:00", "0" } }, true));

            // Act
             statisticsService.Update(userId, connectionInfoObject);

            // Assert
   
            databaseServiceMock.Verify(x => x.ExecuteSQL(It.Is<string>(sql => sql.Contains($"UPDATE STATISTICS SET kmnumber =11, trainnumber=12, connectionsnumber = 1"))), Times.Once);


        }

       
        [Test]
        public async Task Update_NewUser_SuccessfullyInsertsStatistics()
        {
            // Arrange
            string userId = "someUserId";
            var databaseServiceMock = new Mock<IDatabaseServiceAPI>();
            var statisticsService = new StatisticsService(databaseServiceMock.Object);
            var connectionInfoObject = new ConnectionInfoObject { KmNumber = 10, TrainNumber = "10", Duration = TimeSpan.FromMinutes(30) };

            databaseServiceMock.Setup(x => x.ExecuteSQLLastRow(It.IsAny<string>()))
                .ReturnsAsync((new List<object[]>(), true));

            // Act
            statisticsService.Update(userId, connectionInfoObject);

            // Assert
            databaseServiceMock.Verify(x => x.ExecuteSQL(It.IsAny<string>()), Times.Once);
            databaseServiceMock.Verify(x => x.ExecuteSQL(
                It.Is<string>(sql => sql.Contains("INSERT INTO STATISTICS(id,user_id,kmnumber, trainnumber, connectionsnumber, longestconnectiontime,points) VALUES(someUserId, someUserId, 10, 10, 1, 00:30:00, 0)"))),
                Times.Once);




        }

    }
}

    