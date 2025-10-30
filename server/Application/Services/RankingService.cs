using API.Services.Interfaces;
using Koleo.Models;
using Domain;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Koleo.Services
{
    public class RankingService : IRankingService
    {
        private readonly IDatabaseServiceAPI _databaseService;

        public RankingService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<RankingInfo>[]> GetByUser(string userID)
        {
            userID = userID.ToUpper();
            string sqlRankings = "SELECT id FROM rankings";
            var resultRankings = await _databaseService.ExecuteSQL(sqlRankings);

            List<RankingInfo>[] rankings = new List<RankingInfo>[5];
            for (int i = 0; i < 5; i++)
                rankings[i] = new List<RankingInfo>();

            for (int i = 0; i < 5; i++)
            {
                string rankingId = resultRankings.Item1[i][0];
                List<RankingInfo> rankingInfos = await GetRankingInfoByRankingId(rankingId);
                rankings[i] = rankingInfos;
            }
            return rankings;
        }

        private async Task<List<RankingInfo>> GetRankingInfoByRankingId(string rankingId)
        {
            List<RankingInfo> rankingInfos = new List<RankingInfo>();
            string sqlRankingUsers = $"SELECT * FROM RankingUsers WHERE ranking_Id = '{rankingId}'";
            var (rankingUsers, _) = await _databaseService.ExecuteSQLLastRow(sqlRankingUsers);

            foreach (var user in rankingUsers)
            {
                string rankingName = await GetRankingNameById(rankingId);
                string rankingPosition = user[2].ToString();
                string rankingPoints = user[1].ToString();
                string userId = user[4].ToString();
                string username = await GetUsernameById(userId);

                rankingInfos.Add(new RankingInfo(rankingId, userId, rankingName, rankingPosition, rankingPoints, username));
            }
            return rankingInfos;
        }

        private async Task<string> GetRankingNameById(string rankingId)
        {
            string sqlRankingName = $"SELECT Name FROM Rankings WHERE Id = '{rankingId}'";
            var (result, _) = await _databaseService.ExecuteSQL(sqlRankingName);
            return result[0][0];
        }

        private async Task<string> GetUsernameById(string userId)
        {
            string sqlUsername = $"SELECT Email FROM Users WHERE Id = '{userId}'";
            var (result, _) = await _databaseService.ExecuteSQL(sqlUsername);
            return result[0][0];
        }

        public async Task Update(string userID, string rankingID)
        {
            string rankingName = await GetRankingNameById(rankingID);
            string sqlStatistics = $"SELECT User_Id, {rankingName} FROM Statistics";
            var (statistics, _) = await _databaseService.ExecuteSQLLastRow(sqlStatistics);

            if (rankingName == "LongestConnectionTime")
                statistics.Sort((x, y) => TimeSpan.Parse(y[1].ToString()).CompareTo(TimeSpan.Parse(x[1].ToString())));
            else
                statistics.Sort((x, y) => int.Parse(y[1].ToString()).CompareTo(int.Parse(x[1].ToString())));

            foreach (var (stat, index) in statistics.Select((value, i) => (value, i)))
            {
                string userId = stat[0].ToString();
                string points = stat[1].ToString();
                int position = index + 1;

                string sqlCheckIfExist = $"SELECT * FROM RankingUsers WHERE User_Id='{userId}' AND Ranking_Id = '{rankingID}'";
                var (existingEntries, _) = await _databaseService.ExecuteSQLLastRow(sqlCheckIfExist);

                string sql;
                if (existingEntries.Count > 0)
                    sql = $"UPDATE RankingUsers SET Position='{position}', Points='{points}' WHERE User_Id='{userId}' AND Ranking_Id = '{rankingID}'";
                else
                    sql = $"INSERT INTO RankingUsers (Id, Points, Position, Ranking_Id, User_Id) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{points}', '{position}', '{rankingID}', '{userId}')";

                await _databaseService.ExecuteSQLLastRow(sql);
            }
        }
    }
}
