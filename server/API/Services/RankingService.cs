using API.Services.Interfaces;
using Koleo.Models;
using Domain;

namespace Koleo.Services
{
    public class RankingService : IRankingService
    {
        private readonly IDatabaseServiceAPI _databaseService;

        public RankingService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;

            }
        public async Task<List<RankingInfo>> GetByUser(string userID)
        {
            userID = userID.ToUpper();
            List<RankingInfo> rankingInfos = new List<RankingInfo>();
            string sql = $"SELECT * from RankingUsers where User_Id = '{userID}'";
            (var result, var result1) = await _databaseService.ExecuteSQLLastRow(sql);
            foreach (var a in result)
            {
                string ranking_id = a[3].ToString();
       
                string sql2 = $"Select Name from Rankings where Id='{ranking_id}'";
                (var resutl2, var result22) = await _databaseService.ExecuteSQL(sql2);
                string ranking_name = resutl2[0][0];
                string ranking_position = a[2].ToString();
                rankingInfos.Add(new RankingInfo(ranking_id, userID, ranking_name, ranking_position));
            }
            return rankingInfos;
        }

        public void Update(string userID, string rankingID)
        {
            
            //string sql = $"SELECT * FROM  WHERE USER_ID ={userID}";
            //var result = await _databaseService.ExecuteSQL(sql);
        }
    }
}