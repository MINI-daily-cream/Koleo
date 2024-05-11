using API.Services.Interfaces;
using Koleo.Models;

namespace Koleo.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public ProviderService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> AddProvider(string providerName)
        {
            string sql = $"INSERT INTO Providers (Id, Name) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{providerName}')";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        public async Task<bool> EditProvider(string providerId, string newName)
        {
            string sql = $"UPDATE Providers SET Name = '{newName}' WHERE Id = '{providerId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        public async Task<bool> RemoveProvider(string providerId)
        {
            string sql = $"DELETE FROM Providers WHERE Id = '{providerId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

    }
}