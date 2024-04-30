using Domain;
using Koleo.Models;
using KoleoPL.Services;
using System.Reflection.Metadata;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Org.BouncyCastle.Asn1.X509.SigI;
using API.Services.Interfaces;

namespace Koleo.Services
{
    public class TicketService : ITicketServive
    {
        private readonly IDatabaseServiceAPI _databaseService;
        private readonly IPaymentService _paymentService;

        public TicketService(IDatabaseServiceAPI databaseService, IPaymentService paymentService)
        {
            _databaseService = databaseService;
            _paymentService = paymentService;
        }
        public async Task<(string, bool)> Buy(string userId, List<string> connectionsIds, string targetName, string targetSurname)
        {
            if (await _paymentService.ProceedPayment())
            {
                var tmpResult = await Add(userId, connectionsIds, targetName, targetSurname);
                return tmpResult;
            }
            return ("", false);
        }

        public async Task<(List<ConnectionInfoObject>, bool)> ListByUser(string userId)
        {
            var result = await GetTicketsByUser(userId);
            if (!result.Item2) return (new List<ConnectionInfoObject> { }, false);
            List<string> ticketIds = result.Item1;
            List<ConnectionInfoObject> connectionsInfo = new List<ConnectionInfoObject>();
            foreach (string ticketId in ticketIds)
            {
                var tmpResult = await UpdateConnectionsInfoList(ticketId, connectionsInfo);
                if (!tmpResult) return (new List<ConnectionInfoObject> { }, false);
            }
            return (connectionsInfo, true);
        }

        public async Task<bool> Generate(string userId, string ticketId)
        {
            List<ConnectionInfoObject> connectionsInfo = new List<ConnectionInfoObject>();
            var tmpResult = await UpdateConnectionsInfoList(ticketId, connectionsInfo);
            if (!tmpResult) return false;
            string sql = $"SELECT Target_Name, Target_Surname FROM Tickets WHERE Id = '{ticketId}'";
            var resultMain = await _databaseService.ExecuteSQL(sql);
            if (!resultMain.Item2) return false;
            var result = resultMain.Item1;
            string[] userData = result[0];
            string name = userData[0];
            string surname = userData[1];
            try
            {
                string filePath = $"ticket_{ticketId}.pdf";
                using (iTextSharp.text.Document document = new Document())
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create)))
                    {
                        document.Open();
                        Paragraph title = new Paragraph("Ticket Information");
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);
                        document.Add(Chunk.NEWLINE);
                        document.Add(new Paragraph($"Name and surname:{name} {surname}"));
                        foreach (var connection in connectionsInfo)
                        {
                            document.Add(new Paragraph($"Date: {connection.StartDate.ToShortDateString()}"));
                            document.Add(new Paragraph($"Train Number: {connection.TrainNumber}"));
                            document.Add(new Paragraph($"Start Station: {connection.StartStation}"));
                            document.Add(new Paragraph($"End Station: {connection.EndStation}"));
                            document.Add(new Paragraph($"Provider: {connection.ProviderName}"));
                            document.Add(new Paragraph($"Source City: {connection.SourceCity}"));
                            document.Add(new Paragraph($"Destination City: {connection.DestinationCity}"));
                            document.Add(new Paragraph($"Departure Time: {connection.DepartureTime}"));
                            document.Add(new Paragraph($"Arrival Time: {connection.ArrivalTime}"));
                            document.Add(new Paragraph($"Km Number: {connection.KmNumber}"));
                            document.Add(new Paragraph($"Duration: {connection.Duration}"));
                            document.Add(Chunk.NEWLINE);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Remove(string ticketId)
        {
            if (await _paymentService.CancelPayment())
            {
                string deleteConnectionsSql = $"DELETE FROM TicketConnections WHERE Ticket_Id = '{ticketId}'";
                var tmpResult = await _databaseService.ExecuteSQL(deleteConnectionsSql);
                if (!tmpResult.Item2) return false;
                string deleteTicketSql = $"DELETE FROM Tickets WHERE Id = '{ticketId}'";
                tmpResult = await _databaseService.ExecuteSQL(deleteTicketSql);
                return tmpResult.Item2;
            }
            return false;
        }

        public async Task<(string, bool)> Add(string userId, List<string> connectionsIds, string targetName, string targetSurname)
        {
            string insertTicketSql = $"INSERT INTO Tickets (Id, User_Id, Target_Name, Target_Surname) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{userId}', '{targetName}', '{targetSurname}')";
            var result = await _databaseService.ExecuteSQL(insertTicketSql);
            if (!result.Item2) return ("", false);

            string getLastInsertedTicketNumberSql = "SELECT last_insert_rowid()";
            var ticketIdReturn = await _databaseService.ExecuteSQLLastRow(getLastInsertedTicketNumberSql);
            if (!ticketIdReturn.Item2) return ("", false);

            var ticketNumber = (System.Int64) ticketIdReturn.Item1[0][0];

            string getLastInsertedTicketIdSql = $"SELECT * FROM Tickets LIMIT 1 OFFSET {ticketNumber - 1};";
            var ticketIdResult = await _databaseService.ExecuteSQL(getLastInsertedTicketIdSql);

            string ticketId = "";
            if (ticketIdResult.Item1 != null && ticketIdResult.Item1.Count > 0)
            {
                ticketId = new string(ticketIdResult.Item1[0][0]);

                foreach (var connectionId in connectionsIds)
                {
                    string insertTicketConnectionSql = $"INSERT INTO TicketConnections (Id, Ticket_Id, Connection_Id) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{ticketId.ToString().ToUpper()}', '{connectionId.ToString().ToUpper()}')";
                    var tmpresult = await _databaseService.ExecuteSQL(insertTicketConnectionSql);
                    if (!tmpresult.Item2) return ("", false);
                }
            }
            return (ticketId, true);
        }

        public async Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname)
        {
            string updateDetailsSql = $"UPDATE Tickets SET Target_Name = '{newTargetName}', Target_Surname = '{newTargetSurname}' WHERE Id = '{ticketId}'";
            var tmpResult = await _databaseService.ExecuteSQL(updateDetailsSql);
            if(!tmpResult.Item2) return false;
            var result = await Generate(userId, ticketId);
            return result;
        }

        private async Task<(List<string>?, bool)> GetTicketsByUser(string userId)
        {
            string sql = $"SELECT Id FROM Tickets WHERE User_Id='{userId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1.Select(row => row[0]).ToList(), true);
            //return (result.Item1.Select(row => string.Parse(row[0])).ToList(), true);
        }

        private async Task<(List<Connection>?, bool)> GetConnectionsByTicket(string ticketId)
        {
            //string sql = $"SELECT c.* FROM TicketConnections tc JOIN Connections c ON tc.Connection_Id = c.Id WHERE tc.Ticket_Id = '{ticketId}'";
            string sql = $"SELECT * FROM Connections c JOIN TicketConnections tc ON c.Id=tc.Connection_Id WHERE tc.Ticket_Id='{ticketId}'";
            var result = await _databaseService.ExecuteSQLLastRow(sql);
            //var result = await _databaseService.ExecuteSQL(sql);
            if(!result.Item2) return(null, false);

            //var row = result.Item1[0];
            //var Id = Guid.Parse((string)row[0]);
            //var Duration = TimeSpan.Parse((string)row[1]);
            //var EndStation_Id = (string)row[2];
            //var EndTime = DateTime.Parse((string)row[3]);
            //var KmNumber = (int)((System.Int64)row[4]);
            ////var KmNumber = int.Parse((string)row[4]);
            //var StartStation_Id = (string)row[5];
            //var StartTime = DateTime.Parse((string)row[6]);
            //var Train_Id = (string)row[7];

            return (result.Item1.Select(row => new Connection
            {
                //Id = Guid.NewGuid(),
                Id = Guid.Parse((string)row[0]),
                Duration = TimeSpan.Parse((string)row[1]),
                EndStation_Id = (string)row[2],
                EndTime = DateTime.Parse((string)row[3]),
                KmNumber = (int)((System.Int64)row[4]),
                StartStation_Id = (string)row[5],
                StartTime = DateTime.Parse((string)row[6]),
                Train_Id = (string)row[7],
            }).ToList(), true) ;
        }

        private async Task<(string?, bool)> GetStationNameById(string stationId)
        {
            string sql = $"SELECT Name FROM Stations WHERE Id='{stationId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        private async Task<(string?, bool)> GetProviderNameById(string trainId)
        {
            string sql = $"SELECT p.Name FROM Providers p JOIN Trains t WHERE t.Id='{trainId}' AND p.Id=t.Provider_Id";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        private async Task<(string?, bool)> GetCityNameByStationId(string stationId)
        {
            string sql = $"SELECT c.Name FROM Cities c JOIN CityStations cs ON c.Id = cs.City_Id WHERE cs.Station_Id ='{stationId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        private async Task<bool> UpdateConnectionsInfoList(string ticketId, List<ConnectionInfoObject> connectionsInfo)
        {
            var result = await GetConnectionsByTicket(ticketId);
            if(!result.Item2) return false;
            List<Connection> connections = result.Item1;
            foreach (Connection connection in connections)
            {

                var startStationName = await GetStationNameById(connection.StartStation_Id);
                var endStationName = await GetStationNameById(connection.EndStation_Id);
                var providerName = await GetProviderNameById(connection.Train_Id);
                var sourceCityName = await GetCityNameByStationId(connection.StartStation_Id);
                var destinationCityName = await GetCityNameByStationId(connection.EndStation_Id);
                if (!startStationName.Item2 || !endStationName.Item2 || !providerName.Item2 || !sourceCityName.Item2 || !destinationCityName.Item2) return false;
                connectionsInfo.Add(new ConnectionInfoObject
                {
                    Id = connection.Id.ToString().ToUpper(),
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
                    Duration = connection.Duration
                });
            }
            return true;
        }

        private async Task<bool> UpdateConnectionsInfoForBrowsing(List<Connection> connections, List<ConnectionInfoObject> connectionsInfo)
        {
            foreach (Connection connection in connections)
            {

                var startStationName = await GetStationNameById(connection.StartStation_Id);
                var endStationName = await GetStationNameById(connection.EndStation_Id);
                var providerName = await GetProviderNameById(connection.Train_Id);
                var sourceCityName = await GetCityNameByStationId(connection.StartStation_Id);
                var destinationCityName = await GetCityNameByStationId(connection.EndStation_Id);
                if (!startStationName.Item2 || !endStationName.Item2 || !providerName.Item2 || !sourceCityName.Item2 || !destinationCityName.Item2) return false;
                connectionsInfo.Add(new ConnectionInfoObject
                {
                    Id = connection.Id.ToString().ToUpper(),
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
                    Duration = connection.Duration
                });
            }
            return true;
        }

        public async Task<(List<ConnectionInfoObject>, bool)> GetConnectionsInfo(List<Connection> connections)
        {
            List<ConnectionInfoObject> connectionsInfo = new List<ConnectionInfoObject>();
            foreach (var connection in connections)
            {
                var tmpResult = await UpdateConnectionsInfoForBrowsing(connections, connectionsInfo);
                if (!tmpResult) return (new List<ConnectionInfoObject> { }, false);
            }
            return (connectionsInfo, true);
        }
    }
}
