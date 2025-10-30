using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IRankingService
    {
        public Task<List<RankingInfo>[]> GetByUser(string userID);
        public Task Update(string userID, string rankingID);
    }
}
