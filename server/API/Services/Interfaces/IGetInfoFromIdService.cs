using API.DTOs;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IGetInfoFromIdService
    {
        public Task<(string?, bool)> GetStationNameById(string stationId);
        public Task<(string?, bool)> GetProviderNameById(string trainId);
        public Task<(string?, bool)> GetCityNameByStationId(string stationId);
        public Task<(List<string>?, bool)> GetTicketsByUser(string userId);
        public Task<(string[], bool)> GetTicketById(string ticketId);
        public Task<(List<Connection>?, bool)> GetConnectionsByTicket(string ticketId);
        public Task<bool> UpdateConnectionsInfoList(string ticketId, List<TicketInfoDTO> connectionsInfo);
        public Task<bool> UpdateConnectionsInfoForBrowsing(List<Connection> connections, List<TicketInfoDTO> connectionsInfo);
    }
}
