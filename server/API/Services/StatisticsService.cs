using API.Services.Interfaces;
using Koleo.Models;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Koleo.Services
{
    public class StatisticsService: IStatisticsService
    {
        private readonly IDatabaseServiceAPI _databaseService;

        public StatisticsService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<StatisticsInfo>? GetByUser(string userID)
        {
            userID = userID.ToUpper();

            string sql = $"SELECT * FROM STATISTICS WHERE User_Id='{userID}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            if (result.Item1.Count > 0)
            {
                string[] userData = new string[result.Item1[0].Length];
                for(int i = 0; i < result.Item1[0].Length;i++)
                {
                    string s = result.Item1[0][i].ToString();
                    userData[i] = s;
                }
                StatisticsInfo statisticsInfo = new StatisticsInfo(userData[0], userData[6], userData[2], userData[5], userData[1], userData[3], userData[4]);
       
                return statisticsInfo;
            }
            return null;
        }
        public async void Update(string userID,ConnectionInfoObject connectionInfoObject)
        {
            userID = userID.ToUpper();


            string sql = $"SELECT * FROM STATISTICS WHERE User_Id='{userID}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            if (result.Item1.Count > 0)
            {
                string[] userData = new string[result.Item1[0].Length];
                for (int i = 0; i < result.Item1[0].Length; i++)
                {
                    string s = result.Item1[0][i].ToString();
                    userData[i] = s;
                }
                int km_number = int.Parse(userData[2])+connectionInfoObject.KmNumber;
                int train_number = int.Parse(userData[3])+int.Parse(connectionInfoObject.TrainNumber);
                int connections_number = int.Parse(userData[4])+1;
                TimeSpan longest_connection = TimeSpan.Parse(userData[5]);
                int point = int.Parse(userData[6]);

                if (connectionInfoObject.Duration > longest_connection) longest_connection = connectionInfoObject.Duration;

                sql = $"UPDATE STATISTICS SET kmnumber ={km_number}, trainnumber={train_number}, connectionsnumber = {connections_number}, longestconnection_time={longest_connection}";
              await _databaseService.ExecuteSQL(sql);
            }
            else
            {
                int km_number = connectionInfoObject.KmNumber;
                string train_number =  connectionInfoObject.TrainNumber;
                int connections_number =  1;
                TimeSpan longest_connection = connectionInfoObject.Duration;
                int points = 0;

                

                sql =$"INSERT INTO STATISTICS (Id, User_Id, KmNumber, TrainNumber, ConnectionsNumber, LongestConnectionTime, Points) VALUES('{Guid.NewGuid().ToString().ToUpper()}', '{userID}', '{userID}', '{km_number}', '{train_number}', '{connections_number}', '{longest_connection}', '{points}')";

                await _databaseService.ExecuteSQL(sql);
            }
        }
    }
}