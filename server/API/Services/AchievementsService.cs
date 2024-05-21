using API.Services.Interfaces;
using Koleo.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Koleo.Services
{
    public class AchievementsService : IAchievementsService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        private readonly IStatisticsService _statisticsService;

        public AchievementsService(IDatabaseServiceAPI databaseService, IStatisticsService statisticsService)
        {
            _databaseService = databaseService;
            _statisticsService = statisticsService;
        }

        public async Task<List<AchievementInfo>> GetAchievementsByUser(string userID)
        {
            userID = userID.ToUpper();
            string sql = $"SELECT A.Id, A.Name FROM AchievementUsers AU INNER JOIN Achievement A ON AU.Achievement_Id = A.Id WHERE AU.User_Id = '{userID}'";
            var result = await _databaseService.ExecuteSQL(sql);
            List<AchievementInfo> achievements = new List<AchievementInfo>();

            foreach (var row in result.Item1)
            {
                achievements.Add(new AchievementInfo(row[0].ToString(), row[1].ToString()));
  
            }

            return achievements;
        }

        public async Task AddAchievementToUser(string userID, string achievementName)
        {

            string sql = $"SELECT Id FROM Achievement WHERE Name = '{achievementName}'";
            var result = await _databaseService.ExecuteSQL(sql);


            if (result.Item1.Count > 0)
            {
                string achievementId = result.Item1[0][0].ToString();
                sql = $"INSERT INTO AchievementUsers (Id, User_Id, Achievement_Id) VALUES('{Guid.NewGuid().ToString().ToUpper()}', '{userID}', '{achievementId}')";
                await _databaseService.ExecuteSQL(sql);
            }
        }

        public async Task CheckAndAddAchievements(string userID)
        {
            var statistics = await _statisticsService.GetByUser(userID);
            if (statistics != null)
            {
                int kmNumber = int.Parse(statistics.KmNumber);

                if (kmNumber >= 10 && !await HasAchievement(userID, "10km"))
                {
                    await AddAchievementToUser(userID, "10km");
                }

                if (kmNumber >= 100 && !await HasAchievement(userID, "100km"))
                {
                    await AddAchievementToUser(userID, "100km");
                }

                if (kmNumber >= 1000 && !await HasAchievement(userID, "1000km"))
                {
                    await AddAchievementToUser(userID, "1000km");
                }


            }
        }

        public async Task<bool> HasAchievement(string userID, string achievementName)
        {
            string sql = $"SELECT COUNT(*) FROM AchievementUsers AU INNER JOIN Achievement A ON AU.Achievement_Id = A.Id WHERE AU.User_Id = '{userID}' AND A.Name = '{achievementName}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item1.Count > 0 && int.Parse(result.Item1[0][0]) > 0;
        }

        //public async static Task<bool> AddAchievementToDatabase(string achievemntName)
        //{
        //    string sql = $"INSERT INTO Achievement (Id, Name) VALUES ({Guid.NewGuid().ToString().ToUpper()},{achievemntName})";
        //    var result = await _databaseService.ExecuteSQL(sql);
        //    return result.Item2;
        //}

        //public async static Task AddAchievements()
        //{
        //   await AddAchievementToDatabase("Przejechane 100 km");
        //   await AddAchievementToDatabase("Przejechane 1000 km");

        //}
    }


    public interface IAchievementsService
    {
        Task<List<AchievementInfo>> GetAchievementsByUser(string userID);
        Task AddAchievementToUser(string userID, string achievementName);
        Task CheckAndAddAchievements(string userID);
        Task<bool> HasAchievement(string userID, string achievementName);
        //Task<bool> AddAchievementToDatabase(string achievemntName);
        //Task AddAchievements();


    }



}
