//using API.Services.Interfaces;
//using Domain;
//using Koleo.Models;

//namespace API.Services
//{
//    public class ConnectionService : IConnectionService
//    {
//        private readonly IDatabaseServiceAPI _databaseService;
//        public ConnectionService(IDatabaseServiceAPI databaseService)
//        {
//            _databaseService = databaseService;
//        }
//        public async Task<Connection>? Get()
//        {
            
//            string sql = $"SELECT TOP 1 * FROM Connections";
//            (var result, bool success) = await _databaseService.ExecuteSQL(sql);
//            if (success && result.Count > 0)
//            {
//                string[] userData = result[0];
//                string name = userData[0];
//                string surname = userData[1];
//                string email = userData[2];
//                return new AccountInfo(name, surname, email);
//            }
//            return null;
//        }
//        public async Task<bool> UpdateAccountInfo(string userId, string newName, string newSurname, string newEmail)
//        {
//            string sql = $"UPDATE Users SET Name = '{newName}', Surname = '{newSurname}', Email = '{newEmail}' WHERE Id = '{userId}'";
//            var result = await _databaseService.ExecuteSQL(sql);
//            return result.Item2;
//        }
//    }
//}
