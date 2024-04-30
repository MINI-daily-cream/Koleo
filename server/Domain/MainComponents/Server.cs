using System;
using System.Text;

namespace Koleo.Models
{
    public class Server
    {
        public async Task<List<Advertisment>> GetAds(List<AdvertismentCategory> categories)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Advertisment WHERE ");
            foreach (var category in categories)
            {
                sql.Append($"AdvertismentCategory='{category.ToString()}' AND ");
            }
            int startIndex = sql.Length - 5;
            sql.Remove(sql.Length - 5, 5);

            var result = await DatabaseService.ExecuteSQL(sql.ToString());

            if(result != null)
            {
                return result.Select(item => new Advertisment
                {
                    Id = Guid.Parse(item[0]),
                    AdContent = item[1],
                    AdLinkUrl = item[2],
                    AdImageUrl = item[3],
                    AdCategory = (AdvertismentCategory)Enum.Parse(typeof(AdvertismentCategory), item[4]),
                    AdOwner = item[5],
                }).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
