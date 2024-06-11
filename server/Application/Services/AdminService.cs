using API.Services.Interfaces;
using Domain;
using Koleo.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using Persistence;
using System.Security.Cryptography;
using System.Text;

namespace Koleo.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        private DataContext _dataContext;

        public AdminService(IDatabaseServiceAPI databaseService,DataContext dataContext) // jakieś DI
        {
            _databaseService = databaseService;
            _dataContext = dataContext;
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

        public async Task<(List<string>, bool)> ListAdminCandidates() {
            string sql = "SELECT * FROM AdminCandidates";
            var (results, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess) return (new List<string>(), false);
            List<string> res = new List<string>();
            for (int i = 0; i < results.Count; ++i)
            {
                res.Add(results[i][1]);
            }
            return (res, true);

        }
        public async Task<bool> GiveAdminPermissions(string userId)
        {
            string sql = $"SELECT Name, Surname, Email, Password FROM Users WHERE Id = '{userId}'";
            var (result, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess || result.Count == 0)
            {
                return false;
            }
            string name = result[0][0];
            string surname = result[0][1];
            string email = result[0][2];
            string password = result[0][3];
            sql = $"INSERT INTO Administrators (Id, Name, Surname, Email, Password) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{name}', '{surname}', '{email}', '{password}')";
            (result, isSuccess) = await _databaseService.ExecuteSQL(sql);
            if (!isSuccess) return false;
            sql = $"DELETE FROM AdminCandidates WHERE User_Id = '{userId}'";
            (result, isSuccess) = await _databaseService.ExecuteSQL(sql);
            return isSuccess;
        }
        public async Task<bool> RejectAdminRequest(string userId)
        {
            string sql = $"DELETE FROM AdminCandidates WHERE User_Id = '{userId}'";
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

        public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            userId = userId.ToUpper();
            var guid_userID = Guid.Parse(userId);
            
            var user = await _dataContext.Users.FirstOrDefaultAsync(usr => usr.Id == guid_userID);

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var test_password = hmac.ComputeHash(Encoding.UTF8.GetBytes(oldPassword));


            if (test_password.Length != user.PasswordHash.Length)
                return false;

            for (int i = 0; i < test_password.Length; i++)
            {
                if (test_password[i] != user.PasswordHash[i])
                {
                    return false;
                }

            }

             var npassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));


            user.PasswordHash = npassword;


            _dataContext.SaveChanges();



            return true;
        }
    }
}
