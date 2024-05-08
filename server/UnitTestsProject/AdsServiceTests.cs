using API.Services.Interfaces;
using Koleo.Models;
using KoleoPL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsProject
{
    public class AdsServiceTests
    {
        [Fact]
        public async Task GetAds_Returns_Ads_When_Categories_Exist()
        {
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((new List<string[]> { new string[] { Guid.NewGuid().ToString(), "content", "link", "image", "Health", "owner" } }, true));

            var adsService = new AdsService(mockDatabaseService.Object);
            var categories = new List<AdvertismentCategory> { AdvertismentCategory.Health };

            var result = await adsService.GetAds(categories);
            Assert.True(result.Item2);
            Assert.NotEmpty(result.Item1);

        }

        [Fact]
        public async Task AddNewAdvertisement_Returns_True_When_Advertisement_Added_Successfully()
        {
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adsService = new AdsService(mockDatabaseService.Object);

            var result = await adsService.AddNewAdvertisement("content", "link", "image", AdvertismentCategory.Health, "owner");
            Assert.True(result);
        }

        [Fact]
        public async Task EditAdvertisement_Returns_True_When_Advertisement_Edited_Successfully()
        {
            var ad = new Advertisment { Id = Guid.NewGuid() };
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adsService = new AdsService(mockDatabaseService.Object);
            var result = await adsService.EditAdvertisement(ad, "newContent", "newLink", "newImage", "newOwner");
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAdvertisement_Returns_True_When_Advertisement_Deleted_Successfully()
        {
            var ad = new Advertisment { Id = Guid.NewGuid() };
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adsService = new AdsService(mockDatabaseService.Object);
            var result = await adsService.DeleteAdvertisement(ad);
            Assert.True(result);
        }

       

        [Fact]
        public async Task SetAdsForUser_Returns_True_When_Ads_Set_For_User()
        {
            var userId = Guid.NewGuid();
            var ads = new List<Advertisment> { new Advertisment { Id = Guid.NewGuid() } };
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adsService = new AdsService(mockDatabaseService.Object);
            var result = await adsService.SetAdsForUser(userId, ads);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAdsForUser_Returns_True_When_Ads_Deleted_For_User()
        {
            var userId = Guid.NewGuid();
            var adsId = new List<Guid> { Guid.NewGuid() };
            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true));

            var adsService = new AdsService(mockDatabaseService.Object);
            var result = await adsService.DeleteAdsForUser(userId, adsId);

            Assert.True(result);
        }
    }
}
