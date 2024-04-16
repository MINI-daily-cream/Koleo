using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface ITicketServive
    {
        public Task<bool> Buy(Guid userId, List<Connection> connections, string targetName, string targetSurname);
        public Task<List<ConnectionInfoObject>> ListByUser(int userId);
        public Task Add(Guid userId, List<Connection> connections, string targetName, string targetSurname);
    }
}
