using API.Services.Interfaces;
using Domain;
using KoleoPL.Services;

namespace Koleo.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public AccountService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<AccountInfo>? GetAccountInfo(string userId)
        {
            string sql = $"SELECT Name, Surname, Email FROM Users WHERE Id = '{userId}'";
            (var result, bool success) = await _databaseService.ExecuteSQL(sql);
            if (success && result.Count > 0)
            {
                string[] userData = result[0];
                string name = userData[0];
                string surname = userData[1];
                string email = userData[2];
                return new AccountInfo(name, surname, email);
            }
            return null;
        }
        public async Task<bool> UpdateAccountInfo(string userId, string newName, string newSurname, string newEmail)
        {
            string sql = $"UPDATE Users SET Name = '{newName}', Surname = '{newSurname}', Email = '{newEmail}' WHERE Id = '{userId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        // the same as GetAccountInfo
        //public void CheckUserAccount()
        //{
            
        //}
    }
}