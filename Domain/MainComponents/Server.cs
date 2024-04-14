using System.Text;

namespace Koleo.Models
{
    public class Server
    {
        List<Advertisment> GetAds(List<AdvertismentCategory> categories)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Advertisment WHERE ");
            foreach (var category in categories)
            {
                sql.Append($"AdvertismentCategory='{category.ToString()}' and ");
            }
            int startIndex = sql.Length - 5;
            sql.Remove(sql.Length - 5, 5);


            return new List<Advertisment>();
        }
    }
}
