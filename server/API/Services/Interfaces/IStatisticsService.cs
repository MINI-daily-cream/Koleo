using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IStatisticsService
    { 
            Task<StatisticsInfo>? GetByUser(string userID);
        Task Update(string userID, string ticketId);
        
    }
}
