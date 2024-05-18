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

        public async Task<(List<ConnectionInfoDTO>, bool)> GetConnectionsInfo(List<Connection> connections)
        {
            List<ConnectionInfoDTO> connectionsInfo = new List<ConnectionInfoDTO>();
            var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoForBrowsing(connections, connectionsInfo);
            if (!tmpResult) return (new List<ConnectionInfoDTO> { }, false);

            return (connectionsInfo, true);
        }

        public async Task<(List<ConnectionInfoDTO>, bool)> GetFilteredConnections(FindConnectionsDTO filters)
        {
            var startStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.StartCity);
            if(!startStationIdsResult.Item2) return (null, false);
            List<string> startStationIds = startStationIdsResult.Item1;

            var endStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.EndCity);
            if (!endStationIdsResult.Item2) return (null, false);
            List<string> endStationIds = endStationIdsResult.Item1;


            List<Connection> connections = new List<Connection>();

            foreach(var start in startStationIds)
            {
                foreach(var end in endStationIds)
                {
                    var connectionsResult = await _getInfoFromIdService.GetConnectionsByStationIds(start, end);
                    if (!connectionsResult.Item2) return (null, false);
                    List<Connection> connections1 = connectionsResult.Item1;
                    connections.AddRange(connections1);
                }
            }

            var connectionsInfoResult = await GetConnectionsInfo(connections);
            if (!connectionsInfoResult.Item2) return (null, false);
            List<ConnectionInfoDTO> connectionsInfo = connectionsInfoResult.Item1;

            return (connectionsInfo, true);
        }
    }
}
