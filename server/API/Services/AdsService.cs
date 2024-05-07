using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;

namespace KoleoPL.Services
{
    public class AdsService : IAdsService
    {
        public IDatabaseServiceAPI _databaseservice;
        public AdsService(IDatabaseServiceAPI databaseservice)
        {
            _databaseservice = databaseservice;
        }
        public async Task<(List<Advertisment>, bool)> GetAds(List<AdvertismentCategory> categories)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Advertisment WHERE ");
            foreach (var category in categories)
            {
                sql.Append($"AdvertismentCategory='{category.ToString()}' AND ");
            }
            int startIndex = sql.Length - 5;
            sql.Remove(sql.Length - 5, 5);

            var result = await _databaseservice.ExecuteSQL(sql.ToString());

            if (result.Item2 != null)
            {
                List<Advertisment> returnedList = result.Item1.Select(item => new Advertisment
                {
                    Id = Guid.Parse(item[0]),
                    AdContent = item[1],
                    AdLinkUrl = item[2],
                    AdImageUrl = item[3],
                    AdCategory = (AdvertismentCategory)Enum.Parse(typeof(AdvertismentCategory), item[4]),
                    AdOwner = item[5],
                }).ToList();
                return (returnedList, true);
            }
            else
            {
                return (null, false);
            }
        }
        public async Task<bool> AddNewAdvertisement(string AdContent, string AdLinkUrl, string AdImageUrl, AdvertismentCategory AdCategory, string AdOwner)
        {
            string sql = $"INSERT INTO Advertisment (AdContent, AdLinkUrl, AdImageUrl, AdCategory) " +
                $"VALUES ('{AdContent}, '{AdLinkUrl}', '{AdImageUrl}', '{AdCategory.ToString()}', '{AdOwner}')";

            try
            {
                await _databaseservice.ExecuteSQL(sql.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> EditAdvertisement(Advertisment ad, string AdContent = null, string AdLinkUrl = null, string AdImageUrl = null, string AdOwner = null)
        {
            Guid id = ad.Id;

            string sql = $"UPDATE Advertisment SET ";
            List<string> args = new List<string>();
            if (AdContent != null) args.Add($"AdContent = '{AdContent}'");
            if (AdLinkUrl != null) args.Add($"AdLinkUrl = '{AdLinkUrl}'");
            if (AdImageUrl != null) args.Add($"AdImageUrl = '{AdImageUrl}'");
            if (AdOwner != null) args.Add($"AdOwner = '{AdOwner}'");

            sql += string.Join(", ", args);
            sql += $"WHERE Id = `{id}`";

            try
            {
                await _databaseservice.ExecuteSQL(sql.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAdvertisement(Advertisment ad)
        {
            Guid id = ad.Id;
            string sql = $"DELETE FROM Advertisment WHERE Id = '{id}'";
            try
            {
                await _databaseservice.ExecuteSQL(sql.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        public async Task<(List<Advertisment>, bool)> GetAdsForUser(Guid UserId)
        {
            string sql = $"SELECT * FROM Advertisment JOIN UserAdLink ON Id=Ad_Id WHERE User_Id='{UserId}'";
            try
            {
                var result = await _databaseservice.ExecuteSQL(sql.ToString());
                List<Advertisment> returnedList = result.Item1.Select(item => new Advertisment
                {
                    Id = Guid.Parse(item[0]),
                    AdContent = item[1],
                    AdLinkUrl = item[2],
                    AdImageUrl = item[3],
                    AdCategory = (AdvertismentCategory)Enum.Parse(typeof(AdvertismentCategory), item[4]),
                    AdOwner = item[5],
                }).ToList();
                return (returnedList, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return (null, false);
            }
        }
        public async Task<bool> SetAdsForUser(Guid UserId, List<Advertisment> ads)
        {
            string sql = $"INSERT INTO UserAdLink (Ad_Id, User_Id) VALUES ";
            List<string> args = new List<string>();
            foreach (Advertisment ad in ads)
            {
                args.Add($"('{ad.Id}', '{UserId}')");
            }
            sql += string.Join(", ", args);
            try
            {
                await _databaseservice.ExecuteSQL(sql.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAdsForUser(Guid UserId, List<Guid> AdsId)
        {
            string sql = $"DELETE FROM UserAdLink WHERE User_Id = '{UserId.ToString()}' AND (";
            List<string> args = new List<string>();
            foreach (Guid id in AdsId)
            {
                args.Add($"Ad_Id = '{id.ToString()}'");
            }
            sql += string.Join(" OR ", args);
            sql += ")";
            try
            {
                await _databaseservice.ExecuteSQL(sql.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
    }
}