using API.Services.Interfaces;
using Koleo.Models;
using Persistence;

namespace Koleo.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public UserService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<bool> CreateAccount(string name, string surname, string email, string password, string? cardNumber)
        {
            if (await VerifyAccount(email)) {
                string sql = $"INSERT INTO Users (Name, Surname, Email, Password, CardNumber) VALUES ('{name}', '{surname}', '{email}', '{password}', '{cardNumber}')";
                var result = await _databaseService.ExecuteSQL(sql);
                return result.Item2;
            }
            return false;
        }

        public async Task<bool> VerifyAccount(string email)
        {
            string sql = $"SELECT COUNT(*) FROM Users WHERE Email = '{email}'";
            var (results, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if(!isSuccess) return false;
            int count = int.Parse(results[0][0]);
            return count == 0;
        }

        public async Task<bool> RemoveAccount(Guid id)
        {
            string sql = $"DELETE FROM Users WHERE Id = '{id}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        public async Task<bool> AuthoriseAccount(string email, string password)
        {
            string sql = $"SELECT Password FROM Users WHERE Email = '{email}'";
            var (results, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess || results.Count == 0)
            {
                return false;
            }
            return results[0][0] == password;
        }

        public async Task<bool> UserExists(string email)
        {
            string sql = $"SELECT * FROM Users WHERE Email = '{email}'";
            var (results, isSuccess) = await _databaseService.ExecuteSQLLastRow(sql);
            if (!isSuccess || results.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}