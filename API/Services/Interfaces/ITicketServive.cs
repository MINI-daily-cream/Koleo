using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface ITicketServive
    {
        public Task<bool> Buy(string userId, List<Connection> connections, string targetName, string targetSurname);
        public Task<(List<ConnectionInfoObject>, bool)> ListByUser(int userId);
        public Task<bool> Generate(Guid userId, Guid ticketId);
        public Task<bool> Remove(Guid ticketId);
        public Task<bool> Add(string userId, List<Connection> connections, string targetName, string targetSurname);
        public Task<bool> ChangeDetails(Guid userId, Guid ticketId, string newTargetName, string newTargetSurname);
    }
}
