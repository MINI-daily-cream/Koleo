using Koleo.Models;
using KoleoPL.Services;

namespace Koleo.Services
{
    public class AccountService
    {
        private readonly DatabaseService _databaseService;
        public AccountService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<AccountInfo>? GetAccountInfo(Guid userId)
        {
            string sql = $"SELECT Name, Surname, Email FROM Users WHERE Id = '{userId}'";

            var result = await _databaseService.ExecuteSQL(sql);

            if (result.Count > 0)
            {
                string[] userData = result[0];
                string name = userData[0];
                string surname = userData[1];
                string email = userData[2];

                return new AccountInfo(name, surname, email);
            }
            else
            {
                return null;
            }

        }
        public async Task<bool> UpdateAccountInfo(Guid userId, string newName, string newSurname, string newEmail)
        {
            string sql = $"UPDATE Users SET Name = '{newName}', Surname = '{newSurname}', Email = '{newEmail}' WHERE Id = '{userId}'";

            try
            {
                await _databaseService.ExecuteSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }

        // the same as GetAccountInfo
        //public void CheckUserAccount()
        //{
            
        //}
    }
}