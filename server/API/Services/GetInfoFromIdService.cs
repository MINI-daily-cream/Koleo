using API.DTOs;
using API.Services.Interfaces;
using Koleo.Models;

namespace API.Services
{
    public class GetInfoFromIdService : IGetInfoFromIdService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public GetInfoFromIdService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<(string?, bool)> GetStationNameById(string stationId)
        {
            string sql = $"SELECT Name FROM Stations WHERE Id='{stationId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        public async Task<(string?, bool)> GetProviderNameById(string trainId)
        {
            string sql = $"SELECT p.Name FROM Providers p JOIN Trains t WHERE t.Id='{trainId}' AND p.Id=t.Provider_Id";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        public async Task<(string?, bool)> GetCityNameByStationId(string stationId)
        {
            string sql = $"SELECT c.Name FROM Cities c JOIN CityStations cs ON c.Id = cs.City_Id WHERE cs.Station_Id ='{stationId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        public async Task<(List<string>?, bool)> GetTicketsByUser(string userId)
        {
            string sql = $"SELECT Id FROM Tickets WHERE User_Id='{userId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1.Select(row => row[0]).ToList(), true);
            //return (result.Item1.Select(row => string.Parse(row[0])).ToList(), true);
        }

        public async Task<(string[], bool)> GetTicketById(string ticketId)
        {
            string sql = $"SELECT * FROM Tickets WHERE Id='{ticketId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0], true);
        }

        public async Task<(List<Connection>?, bool)> GetConnectionsByTicket(string ticketId)
        {
            //string sql = $"SELECT c.* FROM TicketConnections tc JOIN Connections c ON tc.Connection_Id = c.Id WHERE tc.Ticket_Id = '{ticketId}'";
            string sql = $"SELECT c.* FROM Connections c JOIN TicketConnections tc ON c.Id=tc.Connection_Id WHERE tc.Ticket_Id='{ticketId}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            //var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);

            return (result.Item1.Select(row => new Connection
            {
                //Id = Guid.NewGuid(),
                Id = Guid.Parse((string)row[0]),
                StartStation_Id = (string)row[1],
                EndStation_Id = (string)row[2],
                Train_Id = (string)row[3],
                StartTime = DateTime.Parse((string)row[4]),
                EndTime = DateTime.Parse((string)row[5]),
                KmNumber = (int)((System.Int64)row[6]),
                Duration = TimeSpan.Parse((string)row[7]),
            }).ToList(), true);
        }

        public async Task<bool> UpdateConnectionsInfoList(string ticketId, List<TicketInfoDTO> connectionsInfo)
        {
            var result = await GetConnectionsByTicket(ticketId);
            if (!result.Item2) return false;
            List<Connection> connections = result.Item1;

            var result1 = await GetTicketById(ticketId);
            if (!result1.Item2) return false;
            string[] ticketInfo = result1.Item1;

            foreach (Connection connection in connections)
            {
                var startStationName = await GetStationNameById(connection.StartStation_Id);
                var endStationName = await GetStationNameById(connection.EndStation_Id);
                var providerName = await GetProviderNameById(connection.Train_Id);
                var sourceCityName = await GetCityNameByStationId(connection.StartStation_Id);
                var destinationCityName = await GetCityNameByStationId(connection.EndStation_Id);
                if (!startStationName.Item2 || !endStationName.Item2 || !providerName.Item2 || !sourceCityName.Item2 || !destinationCityName.Item2) return false;
                connectionsInfo.Add(new TicketInfoDTO
                {
                    Id = ticketInfo[0],
                    StartDate = DateOnly.FromDateTime(connection.StartTime.Date),
                    EndDate = DateOnly.FromDateTime(connection.EndTime.Date),
                    StartTime = TimeOnly.FromDateTime(connection.StartTime),
                    EndTime = TimeOnly.FromDateTime(connection.EndTime),
                    TrainNumber = connection.Train_Id,
                    StartStation = startStationName.Item1,
                    EndStation = endStationName.Item1,
                    ProviderName = providerName.Item1,
                    SourceCity = sourceCityName.Item1,
                    DestinationCity = destinationCityName.Item1,
                    DepartureTime = connection.StartTime.ToShortTimeString(),
                    ArrivalTime = connection.EndTime.ToShortTimeString(),
                    KmNumber = connection.KmNumber,
                    Duration = connection.Duration,
                    Name = ticketInfo[2],
                    Surname = ticketInfo[3],
                });
            }
            return true;
        }

        public async Task<bool> UpdateConnectionsInfoForBrowsing(List<Connection[]> connections, List<ConnectionInfoDTO[]> connectionsInfo)
        {
            foreach (var connectionArr in connections)
            {
                ConnectionInfoDTO[] connectionInfosArr = new ConnectionInfoDTO[connectionArr.Length];
                for (int i = 0; i < connectionArr.Length; i++)
                {
                    if (connectionArr[i] == null) 
                        continue;
                    var startStationName = await GetStationNameById(connectionArr[i].StartStation_Id);
                    var endStationName = await GetStationNameById(connectionArr[i].EndStation_Id);
                    var providerName = await GetProviderNameById(connectionArr[i].Train_Id);
                    var sourceCityName = await GetCityNameByStationId(connectionArr[i].StartStation_Id);
                    var destinationCityName = await GetCityNameByStationId(connectionArr[i].EndStation_Id);
                    if (!startStationName.Item2 || !endStationName.Item2 || !providerName.Item2 || !sourceCityName.Item2 || !destinationCityName.Item2) return false;
                    connectionInfosArr[i] = (new ConnectionInfoDTO
                    {
                        Id = connectionArr[i].Id.ToString().ToUpper(),
                        StartDate = DateOnly.FromDateTime(connectionArr[i].StartTime.Date),
                        EndDate = DateOnly.FromDateTime(connectionArr[i].EndTime.Date),
                        StartTime = TimeOnly.FromDateTime(connectionArr[i].StartTime),
                        EndTime = TimeOnly.FromDateTime(connectionArr[i].EndTime),
                        TrainNumber = connectionArr[i].Train_Id,
                        StartStation = startStationName.Item1,
                        EndStation = endStationName.Item1,
                        ProviderName = providerName.Item1,
                        SourceCity = sourceCityName.Item1,
                        DestinationCity = destinationCityName.Item1,
                        DepartureTime = connectionArr[i].StartTime.ToShortTimeString(),
                        ArrivalTime = connectionArr[i].EndTime.ToShortTimeString(),
                        KmNumber = connectionArr[i].KmNumber,
                        Duration = connectionArr[i].Duration
                    });
                }
                connectionsInfo.Add(connectionInfosArr);
            }
            return true;
        }

        public async Task<(List<Connection>?, bool)> GetConnectionsByStationIds(string startStationId, string endStationId)
        {
            string sql = $"SELECT DISTINCT c.* FROM Stations s JOIN Connections c ON (c.StartStation_Id='{startStationId}' AND c.EndStation_Id='{endStationId}')";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            if (!result.Item2) return (null, false);

            return (result.Item1.Select(row => new Connection
            {
                Id = Guid.Parse((string)row[0]),
                StartStation_Id = (string)row[1],
                EndStation_Id = (string)row[2],
                Train_Id = (string)row[3],
                StartTime = DateTime.Parse((string)row[4]),
                EndTime = DateTime.Parse((string)row[5]),
                KmNumber = (int)((System.Int64)row[6]),
                Duration = TimeSpan.Parse((string)row[7]),
            }).ToList(), true);
        }

        public async Task<(List<Connection>?, bool)> GetConnectionsByStartStationId(string startStationId)
        {
            string sql = $"SELECT DISTINCT c.* FROM Stations s JOIN Connections c ON (c.StartStation_Id='{startStationId}')";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            if (!result.Item2) return (null, false);

            return (result.Item1.Select(row => new Connection
            {
                Id = Guid.Parse((string)row[0]),
                StartStation_Id = (string)row[1],
                EndStation_Id = (string)row[2],
                Train_Id = (string)row[3],
                StartTime = DateTime.Parse((string)row[4]),
                EndTime = DateTime.Parse((string)row[5]),
                KmNumber = (int)((System.Int64)row[6]),
                Duration = TimeSpan.Parse((string)row[7]),
            }).ToList(), true);
        }

        public async Task<(List<Connection>?, bool)> GetConnectionsByEndStationId(string endStationId)
        {
            //string sql = $"SELECT c.* FROM TicketConnections tc JOIN Connections c ON tc.Connection_Id = c.Id WHERE tc.Ticket_Id = '{ticketId}'";
            string sql = $"SELECT DISTINCT c.* FROM Stations s JOIN Connections c ON (c.EndStation_Id='{endStationId}')";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            //var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);

            return (result.Item1.Select(row => new Connection
            {
                //Id = Guid.NewGuid(),
                Id = Guid.Parse((string)row[0]),
                StartStation_Id = (string)row[1],
                EndStation_Id = (string)row[2],
                Train_Id = (string)row[3],
                StartTime = DateTime.Parse((string)row[4]),
                EndTime = DateTime.Parse((string)row[5]),
                KmNumber = (int)((System.Int64)row[6]),
                Duration = TimeSpan.Parse((string)row[7]),
            }).ToList(), true);
        }

        public async Task<(List<Connection>?, bool)> GetConnectionsByStationIdsAndTime(string startStationId, string endStationId, DateTime dateTime)
        {
            string sql = $"SELECT DISTINCT c.* FROM Stations s JOIN Connections c ON (c.StartStation_Id='{startStationId}' AND c.EndStation_Id='{endStationId}')";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            if (!result.Item2) return (null, false);

            var connections = result.Item1.Select(row => new Connection
            {
                //Id = Guid.NewGuid(),
                Id = Guid.Parse((string)row[0]),
                StartStation_Id = (string)row[1],
                EndStation_Id = (string)row[2],
                Train_Id = (string)row[3],
                StartTime = DateTime.Parse((string)row[4]),
                EndTime = DateTime.Parse((string)row[5]),
                KmNumber = (int)((System.Int64)row[6]),
                Duration = TimeSpan.Parse((string)row[7]),
            }).ToList();

            connections = connections.FindAll(conn => conn.StartTime > dateTime && (conn.StartTime - dateTime).TotalHours < 20);

            return (connections, true);
        }
    }
}
