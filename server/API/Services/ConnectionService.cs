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
            var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoForBrowsing(connections, connectionsInfo);
            if (!tmpResult) return (new List<TicketInfoDTO> { }, false);

            //foreach (var connection in connections)
            //{
            //    var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoForBrowsing(connections, connectionsInfo);
            //    if (!tmpResult) return (new List<TicketInfoDTO> { }, false);
            //}
            return (connectionsInfo, true);
        }

        public async Task<(List<TicketInfoDTO>, bool)> GetFilteredConnections(FindConnectionsDTO filters)
        {
            var startStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.StartCity);
            if(!startStationIdsResult.Item2) return (null, false);
            List<string> startStationIds = startStationIdsResult.Item1;

            var endStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.EndCity);
            if (!endStationIdsResult.Item2) return (null, false);
            List<string> endStationIds = endStationIdsResult.Item1;

            var connectionsResult = await _getInfoFromIdService.GetConnectionsByStationIds(startStationIds[1], endStationIds[0]);
            if (!connectionsResult.Item2) return (null, false);
            List<Connection> connections = connectionsResult.Item1;


            var connectionsInfoResult = await GetConnectionsInfo(connections);
            if (!connectionsInfoResult.Item2) return (null, false);
            List<TicketInfoDTO> connectionsInfo = connectionsInfoResult.Item1;


            return (connectionsInfo, true);
        }
    }
}
