using API.Services.Interfaces;
using Koleo.Models;
using Domain;
using System;
using System.Threading.Tasks;

namespace Koleo.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        private readonly IGetInfoFromIdService _getInfoFromIdService;
        private readonly IRankingService _rankingService;
        private readonly IAchievementsService _achievementsService;

        public StatisticsService(
            IDatabaseServiceAPI databaseService,
            IGetInfoFromIdService getInfoFromIdService,
            IRankingService rankingService,
            IAchievementsService achievementsService)
        {
            _databaseService = databaseService;
            _getInfoFromIdService = getInfoFromIdService;
            _rankingService = rankingService;
            _achievementsService = achievementsService;
        }

        public async Task<StatisticsInfo?> GetByUser(string userID)
        {
            userID = userID.ToUpper();
            string sql = $"SELECT * FROM STATISTICS WHERE User_Id='{userID}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);

            if (result.Item1.Count > 0)
            {
                var userData = result.Item1[0];
                return new StatisticsInfo(
                    userData[0].ToString(),
                    userData[6].ToString(),
                    userData[2].ToString(),
                    userData[5].ToString(),
                    userData[1].ToString(),
                    userData[3].ToString(),
                    userData[4].ToString()
                );
            }

            return null;
        }

        public async Task Update(string userID, string ticketId)
        {
            userID = userID.ToUpper();
            var (connections, _) = await _getInfoFromIdService.GetConnectionsByTicket(ticketId);

            string sql = $"SELECT * FROM STATISTICS WHERE User_Id='{userID}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);

            if (result.Item1.Count > 0)
            {
                await UpdateExistingUserStatistics(userID, result.Item1[0], connections);
            }
            else
            {
                await CreateNewUserStatistics(userID, connections);
            }

            await UpdateRankings(userID);
            await CheckAndAddAchievements(userID);
        }

        private async Task UpdateExistingUserStatistics(string userID, object[] userData, List<Connection> connections)
        {
            int kmNumber = int.Parse(userData[2].ToString()) + connections.Sum(c => c.KmNumber);
            int trainNumber = int.Parse(userData[5].ToString()) + connections.Count;
            int connectionsNumber = int.Parse(userData[1].ToString()) + connections.Count;
            int points = int.Parse(userData[4].ToString()) + new Random().Next(kmNumber);
            TimeSpan longestConnection = TimeSpan.Parse(userData[3].ToString());
            foreach (var connection in connections)
            {
                if (connection.Duration > longestConnection)
                {
                    longestConnection = connection.Duration;
                }
            }

            string sql = $@"
                UPDATE STATISTICS SET
                    KmNumber = '{kmNumber}',
                    TrainNumber = '{trainNumber}',
                    ConnectionsNumber = '{connectionsNumber}',
                    LongestConnectionTime = '{longestConnection}',
                    Points = '{points}'
                WHERE User_Id = '{userID}'";

            await _databaseService.ExecuteSQL(sql);
        }

        private async Task CreateNewUserStatistics(string userID, List<Connection> connections)
        {
            string sql = $@"
                INSERT INTO STATISTICS (Id, User_Id, KmNumber, TrainNumber, ConnectionsNumber, LongestConnectionTime, Points)
                VALUES (
                    '{Guid.NewGuid().ToString().ToUpper()}',
                    '{userID}',
                    '0',
                    '0',
                    '0',
                    '00:00:00',
                    '0')";

            await _databaseService.ExecuteSQL(sql);
        }

        private async Task UpdateRankings(string userID)
        {
            string sql = "SELECT id FROM rankings";
            var result = await _databaseService.ExecuteSQL(sql);

            foreach (var ranking in result.Item1)
            {
                _rankingService.Update(userID, ranking[0].ToString());
            }
        }

        private async Task CheckAndAddAchievements(string userID)
        {
            string sql = $"SELECT * FROM STATISTICS WHERE User_Id='{userID}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);

            if (result.Item1.Count > 0)
            {
                var userData = result.Item1[0];
                var statistics = new StatisticsInfo(
                    "",
                    userID,
                    userData[2].ToString(),
                    userData[5].ToString(),
                    userData[1].ToString(),
                    userData[3].ToString(),
                    userData[4].ToString()
                );
                _achievementsService.CheckAndAddAchievements(userID, statistics);
            }
        }
    }
}
