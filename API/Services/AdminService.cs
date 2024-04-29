using API.Services.Interfaces;
using Koleo.Models;
using Org.BouncyCastle.Asn1.X509;

namespace Koleo.Services
{
    public class AdminService : IAccountSettingsServer
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public AdminService(IDatabaseServiceAPI databaseService) // jakieś DI
        {
            _databaseService = databaseService;
        }

        public async Task<bool> CreateAccount(string name, string surname, string email, string password, string? cardNumbe)
        {
            if (await VerifyAccount(email))
            {
                string sql = $"INSERT INTO Administrators (Name, Surname, Email, Password) VALUES ('{name}', '{surname}', '{email}', '{password}')";
                var result = await _databaseService.ExecuteSQL(sql);
                return result.Item2;
            }
            return false;
        }

        public async Task<bool> VerifyAccount(string email)
        {
            string sql = $"SELECT COUNT(*) FROM Administrators WHERE Email = '{email}'";
            var (results, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess) return false;
            int count = int.Parse(results[0][0]);
            return count == 0;
        }

        public async Task<bool> RemoveAccount(Guid id)
        {
            string sql = $"DELETE FROM Administrators WHERE Id = '{id}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        public async Task<bool> AuthoriseAccount(string email, string password)
        {
            string sql = $"SELECT Password FROM Administrators WHERE Email = '{email}'";
            var (results, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess || results.Count == 0)
            {
                return false;
            }
            return results[0][0] == password;
        }
        public async Task<bool> GiveAdminPermissions(string userId)
        {
            string sql = $"SELECT Name, Surname, Email, Password FROM Users WHERE Id = {userId}";
            var (result, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess || result.Count == 0)
            {
                return false;
            }
            string name = result[0][0];
            string surname = result[0][1];
            string email = result[0][2];
            string password = result[0][3];
            isSuccess = await CreateAccount(name, surname, email, password, null);
            if (!isSuccess) return false;
            sql = $"DELETE FROM AdminCandidates WHERE Id = '{userId}'";
            (result, isSuccess) = await _databaseService.ExecuteSQL(sql);
            return isSuccess;
        }
        public async Task<bool> RejectAdminRequest(string userId)
        {
            string sql = $"DELETE FROM AdminCandidates WHERE Id = '{userId}'";
            var (_, isSuccess) = await _databaseService.ExecuteSQL(sql);
            return isSuccess;
        }
        public async Task<bool> DeleteUser(string userId)
        {
            string sql = $"DELETE FROM AdminCandidates WHERE User_Id = '{userId}'";
            var (_, isSuccess1) = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM RankingUsers WHERE User_Id = '{userId}'";
            (_, var isSuccess2) = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM Statistics WHERE User_Id = '{userId}'";
            (_, var isSuccess3) = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM Tickets WHERE User_Id = '{userId}'";
            (_, var isSuccess4) = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM AdminComplaints WHERE Complaint_Id IN (SELECT Id FROM Complaints WHERE User_Id = '{userId}');";
            (_, var isSuccess5) = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM Complaints WHERE User_Id = '{userId}';";
            (_, var isSuccess6) = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM Users WHERE Id = '{userId}'";
            (_, var isSuccess7) = await _databaseService.ExecuteSQL(sql);
            return isSuccess1 && isSuccess2 && isSuccess3 && isSuccess4 && isSuccess5 && isSuccess6 && isSuccess7;
        }
    }
}
