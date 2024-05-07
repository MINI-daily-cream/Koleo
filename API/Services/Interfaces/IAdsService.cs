using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IAdsService
    {
        public Task<(List<Advertisment>, bool)> GetAds(List<AdvertismentCategory> categories);
        public Task<bool> AddNewAdvertisement(string AdContent, string AdLinkUrl, string AdImageUrl, AdvertismentCategory AdCategory, string AdOwner);
        public Task<bool> EditAdvertisement(Advertisment ad, string AdContent = null, string AdLinkUrl = null, string AdImageUrl = null, string AdOwner = null);
        public Task<bool> DeleteAdvertisement(Advertisment ad);
        public Task<(List<Advertisment>, bool)> GetAdsForUser(Guid UserId);
        public Task<bool> SetAdsForUser(Guid UserId, List<Advertisment> ads);
        public Task<bool> DeleteAdsForUser(Guid UserId, List<Guid> AdsId);
    }
}