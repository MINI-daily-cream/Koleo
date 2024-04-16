using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Koleo.Models
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        bool AddProvider()
        {
            return false;
        }
        bool EditProvider(int providerId)
        {
            return false;
        }
        bool RemoveProvider(int providerId)
        {
            return false;
        }
        List<Complaint> ListComplaints()
        {
            return new List<Complaint>();
        }
        bool AnswerComplaint(int complaintId)
        {
            return false;
        }
        AccountInfo ChceckUserAccount(int userId)
        {
            return new AccountInfo("", "", "");
        }
        List<int> ListAdminCandidates()
        {
            return new List<int>();
        }
        bool AcceptNewAdmin(int userId)
        {
            return false;
        }// limit na adminow ?
        void RejectNewAdmin(int userId)
        {
            return;
        }
        void DeleteUser(int userId)
        {
            return;
        }
        bool BackupUserDB()
        {
            return false;
        }
        bool BackupProviderDB()
        {
            return false;
        }
        bool BackupConnectionDB()
        {
            return false;
        }
        public async Task<bool> AddNewAdvertisement(string AdContent, string AdLinkUrl, string AdImageUrl, AdvertismentCategory AdCategory, string AdOwner)
        {
            string sql = $"INSERT INTO Advertisment (AdContent, AdLinkUrl, AdImageUrl, AdCategory) " +
                $"VALUES ('{AdContent}, '{AdLinkUrl}', '{AdImageUrl}', '{AdCategory.ToString()}', '{AdOwner}')";

            try
            {
                await DatabaseService.ExecuteSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        async Task<bool> EditAdvertisement(Advertisment ad, string AdContent = null, string AdLinkUrl = null, string AdImageUrl = null, string AdOwner = null)
        {
            Guid id = ad.Id;

            string sql = $"UPDATE Advertisment SET ";
            List<string> args = new List<string>();
            if(AdContent != null) args.Add($"AdContent = '{AdContent}'");
            if (AdLinkUrl != null) args.Add($"AdLinkUrl = '{AdLinkUrl}'");
            if (AdImageUrl != null) args.Add($"AdImageUrl = '{AdImageUrl}'");
            if (AdOwner != null) args.Add($"AdOwner = '{AdOwner}'");

            sql += string.Join(", ", args);
            sql += $"WHERE Id = `{id}`";

            try
            {
                await DatabaseService.ExecuteSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        async Task<bool> DeleteAdvertisement(Advertisment ad)
        {
            Guid id = ad.Id;
            string sql = $"DELETE FROM Advertisment WHERE Id = '{id}'";
            try
            {
                await DatabaseService.ExecuteSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }

        async Task<List<Advertisment>> GetAdsForUser(Guid UserId)
        {
            string sql = $"SELECT * FROM Advertisment JOIN UserAdLink ON Id=Ad_Id WHERE User_Id='{UserId}'";
            try
            {
                var result = await DatabaseService.ExecuteSQL(sql);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return new List<Advertisment>();
            }
        }
        async Task<bool> SetAdsForUser(Guid UserId, List<Advertisment> ads)
        {
            string sql = $"INSERT INTO UserAdLink (Ad_Id, User_Id) VALUES ";
            List<string> args = new List<string>();
            foreach(Advertisment ad in ads)
            {
                args.Add($"('{ad.Id}', '{UserId}')");
            }
            sql += string.Join(", ", args);
            try
            {
                await DatabaseService.ExecuteSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
        async Task<bool> DeleteAdsForUser(Guid UserId, List<Guid> AdsId)
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
                await DatabaseService.ExecuteSQL(sql);
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
