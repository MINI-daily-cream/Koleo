using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface ITicketServive
    {
        public Task<bool> Buy(string userId, List<Connection> connections, string targetName, string targetSurname);
        public Task<(List<ConnectionInfoObject>, bool)> ListByUser(string userId);
        public Task<bool> Generate(string userId, string ticketId);
        public Task<bool> Remove(string ticketId);
        public Task<bool> Add(string userId, List<Connection> connections, string targetName, string targetSurname);
        public Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname);
    }
}
