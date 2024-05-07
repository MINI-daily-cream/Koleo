using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IRankingService
    {
        public Task<List<RankingInfo>> GetByUser(string userID);
        public void Update(string userID, string rankingID);
    }
}
