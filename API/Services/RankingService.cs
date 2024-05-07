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
            List<RankingInfo> rankingInfos = new List<RankingInfo>();
            string sql = $"SELECT * from RankingUser where User_id = {userID}";
            var result = await _databaseService.ExecuteSQL(sql);
            foreach(var a  in result)
            {
                string ranking_id = a[1];
                string sql2 = $"Select Name from Rankings where ID={ranking_id}";
                var resutl2= await _databaseService.ExecuteSQL(sql2);
                string ranking_name = resutl2[0][1];

                rankingInfos.Add(new RankingInfo(ranking_id, userID, ranking_name));
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