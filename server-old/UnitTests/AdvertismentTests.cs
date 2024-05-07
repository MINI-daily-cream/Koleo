using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Koleo.Models;
using Koleo.Services;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace KoleoPL.Tests
{
    public class AdvertisementTests
    {
        [Test]
              public void Advertisment_Id_IsSetCorrectly()
        {
            // Arrange
            var advertisment = new Advertisment();
            var id = Guid.NewGuid();

            // Act
            advertisment.Id = id;

            // Assert
            Assert.That(advertisment.Id, Is.EqualTo(id));
        }

        [Test]
        public void Advertisment_AdContent_IsSetCorrectly()
        {
            // Arrange
            var advertisment = new Advertisment();
            var adContent = "Test Ad Content";

            // Act
            advertisment.AdContent = adContent;

            // Assert
            Assert.That(advertisment.AdContent, Is.EqualTo(adContent));
        }



        [Test]
        public void Advertisment_AdCategory_ContainsAllCategories()
        {
            // Arrange
            var categories = Enum.GetValues(typeof(AdvertismentCategory));

            // Act & Assert
            foreach (var category in categories)
            {
                Assert.That(Enum.IsDefined(typeof(AdvertismentCategory), category), Is.True);
            }
        }
        //public async Task AddNewAdvertisement_Should_Return_True_When_Successful()
        //{
        //    // Arrange
        //    var admin = new Admin();
           

        //    // Act
        //    var result = await admin.AddNewAdvertisement("Content", "LinkUrl", "ImageUrl", AdvertismentCategory.Health, "Owner");

        //    // Assert
        //    Assert.That(result,Is.EqualTo(true));
        //}

    }
}
