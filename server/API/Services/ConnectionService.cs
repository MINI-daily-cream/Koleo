using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;

namespace API.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IGetInfoFromIdService _getInfoFromIdService;
        private readonly IGetIdFromInfoService _getIdFromInfoService;
        public ConnectionService(IGetInfoFromIdService getInfoFromIdService, IGetIdFromInfoService getIdFromInfoService)
        {
            _getInfoFromIdService = getInfoFromIdService;
            _getIdFromInfoService = getIdFromInfoService;
        }

        public async Task<(List<TicketInfoDTO>, bool)> GetConnectionsInfo(List<Connection> connections)
        {
            List<TicketInfoDTO> connectionsInfo = new List<TicketInfoDTO>();
            foreach (var connection in connections)
            {
                var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoForBrowsing(connections, connectionsInfo);
                if (!tmpResult) return (new List<TicketInfoDTO> { }, false);
            }
            return (connectionsInfo, true);
        }

        public async Task<(List<TicketInfoDTO>, bool)> GetFilteredConnections(FindConnectionsDTO filters)
        {
            var startStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.StartCity);
            if(!startStationIdsResult.Item2) return (null, false);
            string[] startStationIds = startStationIdsResult.Item1;

            return (null, false);
        }
    }
}
