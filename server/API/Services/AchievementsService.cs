using API.Services.Interfaces;
using Koleo.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Koleo.Services
{
    public class AchievementsService : IAchievementsService
    {
        private readonly IDatabaseServiceAPI _databaseService;

        public AchievementsService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
            
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
            Console.WriteLine(sql);

            if (result.Item1.Count > 0)
            {
                string achievementId = result.Item1[0][0].ToString();
                sql = $"INSERT INTO AchievementUsers (Id, User_Id, Achievement_Id) VALUES('{Guid.NewGuid().ToString().ToUpper()}', '{userID}', '{achievementId}')";
                Console.WriteLine(sql);

                await _databaseService.ExecuteSQL(sql);
            }
        }

        public async Task CheckAndAddAchievements(string userID, StatisticsInfo statistics)
        {
     
            if (statistics != null)
            {
                int kmNumber = int.Parse(statistics.KmNumber);
                Console.WriteLine(kmNumber);
                if (kmNumber >= 1 && !await HasAchievement(userID, "Przejechane 1 km"))
                {
                    await AddAchievementToUser(userID, "Przejechane 1 km");
                }
                if (kmNumber >= 10 && !await HasAchievement(userID, "Przejechane 10 km"))
                {
                    await AddAchievementToUser(userID, "Przejechane 10 km");
                }

                if (kmNumber >= 100 && !await HasAchievement(userID, "Przejechane 100 km"))
                {
                    await AddAchievementToUser(userID, "Przejechane 100 km");
                }

                if (kmNumber >= 1000 && !await HasAchievement(userID, "Przejechane 1000 km"))
                {
                    await AddAchievementToUser(userID, "Przejechane 1000 km");
                }


            }
        }

        public async Task<bool> HasAchievement(string userID, string achievementName)
        {
            string sql = $"SELECT Id FROM Achievement where Name = '{achievementName}'";
            var result = await _databaseService.ExecuteSQL(sql);
            Console.WriteLine(sql);
           sql = $"SELECT * FROM AchievementUsers where User_Id = '{userID}' and Achievement_Id='{result.Item1[0][0]}'";
            result = await _databaseService.ExecuteSQL(sql);
            Console.WriteLine(sql);

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



    



}
