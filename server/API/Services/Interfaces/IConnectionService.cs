using API.DTOs;
using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IConnectionService
    {
        public Task<(List<TicketInfoDTO>, bool)> GetConnectionsInfo(List<Connection> connections);
        public Task<(List<TicketInfoDTO>, bool)> GetFilteredConnections(FindConnectionsDTO filters);
    }
}
