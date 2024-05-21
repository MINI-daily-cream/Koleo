using API.DTOs;
using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IConnectionService
    {
        public Task<(List<ConnectionInfoDTO[]>, bool)> GetConnectionsInfo(List<Connection[]> connections);
        public Task<(List<ConnectionInfoDTO[]>, bool)> GetFilteredConnections(FindConnectionsDTO filters);
    }
}
