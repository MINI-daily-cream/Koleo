using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IStatisticsService
    { 
            Task<StatisticsInfo>? GetByUser(string userID);
             void Update(string userID, ConnectionInfoObject connectionInfoObject);
        
    }
}
