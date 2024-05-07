using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface ITicketServive
    {
        public Task<(string, bool)> Buy(string userId, List<string> connectionsIds, string targetName, string targetSurname);
        public Task<(List<ConnectionInfoObject>, bool)> ListByUser(string userId);
        public Task<bool> Generate(string userId, string ticketId);
        public Task<bool> Remove(string ticketId);
        public Task<(string, bool)> Add(string userId, List<string> connectionsIds, string targetName, string targetSurname);
        public Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname);
        public Task<(List<ConnectionInfoObject>, bool)> GetConnectionsInfo(List<Connection> connections);
    }
}
