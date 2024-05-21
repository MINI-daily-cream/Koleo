using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;
using Org.BouncyCastle.Crypto.Engines;
using static iTextSharp.text.pdf.AcroFields;

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

        public async Task<(List<ConnectionInfoDTO[]>, bool)> GetConnectionsInfo(List<Connection[]> connections)
        {
            List<ConnectionInfoDTO[]> connectionsInfo = new List<ConnectionInfoDTO[]>();
            var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoForBrowsing(connections, connectionsInfo);
            if (!tmpResult) return (new List<ConnectionInfoDTO[]> { }, false);

            return (connectionsInfo, true);
        }

        public async Task<(List<ConnectionInfoDTO[]>, bool)> GetFilteredConnections(FindConnectionsDTO filters)
        {
            DateTime day = DateTime.Parse(filters.Day);

            var startStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.StartCity);
            if(!startStationIdsResult.Item2) return (null, false);
            List<string> startStationIds = startStationIdsResult.Item1;

            var endStationIdsResult = await _getIdFromInfoService.GetStationIdsByCityName(filters.EndCity);
            if (!endStationIdsResult.Item2) return (null, false);
            List<string> endStationIds = endStationIdsResult.Item1;


            List<Connection[]> connections = new List<Connection[]>();

            //foreach(var start in startStationIds)
            //{
            //    foreach(var end in endStationIds)
            //    {
            //        var connectionsResult = await _getInfoFromIdService.GetConnectionsByStationIds(start, end);
            //        if (!connectionsResult.Item2) return (null, false);
            //        List<Connection> connections1 = connectionsResult.Item1;
            //        connections.AddRange(connections1);
            //    }
            //}

            List<Connection> startConnections = new List<Connection>();

            foreach (var start in startStationIds)
            {
                var connectionsResult = await _getInfoFromIdService.GetConnectionsByStartStationId(start);
                if (!connectionsResult.Item2) return (null, false);
                List<Connection> connections1 = connectionsResult.Item1;

                startConnections.AddRange(connections1);
            }

            foreach (var startConn in startConnections)
            {
                if(endStationIds.Contains(startConn.EndStation_Id))
                    connections.Add([startConn]);;
                foreach (var end in endStationIds) 
                {
                    var connectionsResult = await _getInfoFromIdService.GetConnectionsByStationIdsAndTime(startConn.EndStation_Id, end, startConn.EndTime);
                    if (!connectionsResult.Item2) return (null, false);
                    List<Connection> connections1 = connectionsResult.Item1;
                    
                    foreach (var conn in connections1)
                    {
                        connections.Add([ startConn, conn ]);
                    }
                }
            }
            connections = connections.FindAll(compositeConn => 
                compositeConn[compositeConn.Length - 1].StartTime.Date.Date == day.Date &&
                (endStationIds.Contains(compositeConn[0].EndStation_Id) 
                    || (compositeConn[1] != null && endStationIds.Contains(compositeConn[1].EndStation_Id))
                )
            );
            connections.Sort(0, connections.Count, new ConnectionComparer());

            var connectionsInfoResult = await GetConnectionsInfo(connections);
            if (!connectionsInfoResult.Item2) return (null, false);
            List<ConnectionInfoDTO[]> connectionsInfo = connectionsInfoResult.Item1;

            return (connectionsInfo, true);
        }
    }

    class ConnectionComparer : IComparer<Connection[]>
    {
        public int Compare(Connection[] c1, Connection[] c2)
        {
            return c1[c1.Length - 1].EndTime.CompareTo(c2[c2.Length - 1].EndTime);
        }
    }
}
