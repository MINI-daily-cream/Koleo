namespace Koleo.Services
{
    public class RankingService
    {
        public RankingService()
        {
        }
        public void GetByUser()
        {

        }
<<<<<<< Updated upstream
        public void Update()
=======
        public async Task<List<RankingInfo>> GetByUser(string userID)
        {
            List<RankingInfo> rankingInfos = new List<RankingInfo>();
            string sql = $"SELECT * from RankingUsers where User_Id = '{userID}'";
            (var result,var result1) = await _databaseService.ExecuteSQLLastRow(sql);
            foreach(var a  in result)
            {
                string ranking_id = a[1].ToString();
                string sql2 = $"Select Name from Rankings where Id='{ranking_id}'";
                (var resutl2,var result22)= await _databaseService.ExecuteSQL(sql2);
                string ranking_name = resutl2[0][0];
                string ranking_position = a[3].ToString();
                rankingInfos.Add(new RankingInfo(ranking_id, userID, ranking_name,ranking_position));
            }
            return rankingInfos;
        }
        public void Update(string userID, string rankingID)
>>>>>>> Stashed changes
        {
            
        }
    }
}