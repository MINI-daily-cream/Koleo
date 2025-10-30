using Domain;

namespace API.Services.Interfaces
{

    public interface IAchievementsService
    {
        Task<List<AchievementInfo>> GetAchievementsByUser(string userID);
        Task AddAchievementToUser(string userID, string achievementName);
        Task CheckAndAddAchievements(string userID, StatisticsInfo statistics);
        Task<bool> HasAchievement(string userID, string achievementName);



    }
}