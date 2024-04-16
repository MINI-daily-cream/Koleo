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
    public class TicketService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        private readonly PaymentService _paymentService;

        public TicketService(IDatabaseServiceAPI databaseService, PaymentService paymentService)
        {
            _databaseService = databaseService;
            _paymentService = paymentService;
        }
        public async Task<bool> Buy(Guid userId, List<Connection> connections, string targetName, string targetSurname)
        {
            if (_paymentService.ProceedPayment())
            {
                await Add(userId, connections, targetName, targetSurname);
                return true;
            }
            return false;
        }

        private async void UpdateConnectionsInfoList(Guid ticketId, List<ConnectionInfoObject> connectionsInfo) {
            List<Connection> connections = await GetConnectionsByTicket(ticketId);
            foreach (Connection connection in connections)
            {

                string startStationName = await GetStationNameById(connection.StartStation_Id);
                string endStationName = await GetStationNameById(connection.EndStation_Id);
                string providerName = await GetProviderNameById(connection.Train_Id);
                string sourceCityName = await GetCityNameByStationId(connection.StartStation_Id);
                string destinationCityName = await GetCityNameByStationId(connection.EndStation_Id);

                connectionsInfo.Add(new ConnectionInfoObject
                {
                    Date = connection.StartTime.Date,
                    TrainNumber = connection.Train_Id,
                    StartStation = startStationName,
                    EndStation = endStationName,
                    ProviderName = providerName,
                    SourceCity = sourceCityName,
                    DestinationCity = destinationCityName,
                    DepartureTime = connection.StartTime.ToShortTimeString(),
                    ArrivalTime = connection.EndTime.ToShortTimeString(),
                    KmNumber = connection.KmNumber,
                    Duration = connection.Duration
                });
            }
        }

        public async Task<List<ConnectionInfoObject>> ListByUser(int userId)
        {
            List<Guid> ticketIds = await GetTicketsByUser(userId);
            List<ConnectionInfoObject> connectionsInfo = new List<ConnectionInfoObject>();
            foreach (Guid ticketId in ticketIds)
            {
                UpdateConnectionsInfoList(ticketId, connectionsInfo);   
            }
            return connectionsInfo;
        }

        public async void Generate(Guid userId, Guid ticketId)
        {
            List<ConnectionInfoObject> connectionsInfo = new List<ConnectionInfoObject>();
            UpdateConnectionsInfoList(ticketId, connectionsInfo);
            string sql = $"SELECT Target_Name, Target_Surname FROM Tickets WHERE Id = '{ticketId}'";
            var result = await _databaseService.ExecuteSQL(sql);
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
                            document.Add(new Paragraph($"Date: {connection.Date.ToShortDateString()}"));
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

                Console.WriteLine($"PDF ticket generated successfully. File saved at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while generating the PDF ticket: {ex.Message}");
            }

        }

        public async Task<bool> Remove(Guid ticketId)
        {
            if (_paymentService.CancelPayment())
            {
                string deleteConnectionsSql = $"DELETE FROM TicketConnections WHERE Ticket_Id = '{ticketId}'";
                await _databaseService.ExecuteSQL(deleteConnectionsSql);
                string deleteTicketSql = $"DELETE FROM Tickets WHERE Id = '{ticketId}'";
                await _databaseService.ExecuteSQL(deleteTicketSql);
                return true;
            }
            return false;
            
        }

        public async Task Add(Guid userId, List<Connection> connections, string targetName, string targetSurname)
        {
            string insertTicketSql = $"INSERT INTO Tickets (User_Id, Target_Name, Target_Surname) VALUES ('{userId}', '{targetName}', '{targetSurname}')";
            await _databaseService.ExecuteSQL(insertTicketSql);
            string getLastInsertedTicketIdSql = "SELECT last_insert_rowid()";
            var ticketIdResult = await _databaseService.ExecuteSQL(getLastInsertedTicketIdSql);
            Guid ticketId = new Guid(ticketIdResult[0][0]);

            foreach (var connection in connections)
            {
                string insertTicketConnectionSql = $"INSERT INTO TicketConnections (Ticket_Id, Connection_Id) VALUES ('{ticketId}', '{connection.Id}')";
                await _databaseService.ExecuteSQL(insertTicketConnectionSql);
            }
        }

        public async Task ChangeDetails(Guid userId, Guid ticketId, string newTargetName, string newTargetSurname)
        {
            string updateDetailsSql = $"UPDATE Tickets SET Target_Name = '{newTargetName}', Target_Surname = '{newTargetSurname}' WHERE Id = '{ticketId}'";
            await _databaseService.ExecuteSQL(updateDetailsSql);
            Generate(userId, ticketId);
        }

        private async Task<List<Guid>> GetTicketsByUser(int userId)
        {
            string sql = $"SELECT Id FROM Tickets WHERE User_Id = {userId}";
            var result = await _databaseService.ExecuteSQL(sql);

            return result.Select(row => Guid.Parse(row[0])).ToList();
        }

        private async Task<List<Connection>> GetConnectionsByTicket(Guid ticketId)
        {
            string sql = $"SELECT c.* FROM TicketConnections tc JOIN Connections c ON tc.Connection_Id = c.Id WHERE tc.Ticket_Id = '{ticketId}'";
            var result = await _databaseService.ExecuteSQL(sql);

            return result.Select(row => new Connection
            {
                Id = Guid.Parse(row[0]),
                StartStation_Id = int.Parse(row[1]),
                EndStation_Id = int.Parse(row[2]),
                Train_Id = int.Parse(row[3]),
                StartTime = DateTime.Parse(row[4]),
                EndTime = DateTime.Parse(row[5]),
                KmNumber = int.Parse(row[6]),
                Duration = TimeSpan.Parse(row[7])
            }).ToList();
        }

        private async Task<string> GetStationNameById(int stationId)
        {
            string sql = $"SELECT Name FROM Stations WHERE Id = {stationId}";
            var result = await _databaseService.ExecuteSQL(sql);

            return result[0][0];
        }

        private async Task<string> GetProviderNameById(int trainId)
        {
            string sql = $"SELECT Name FROM Providers WHERE Id = {trainId}";
            var result = await _databaseService.ExecuteSQL(sql);

            return result[0][0];
        }

        private async Task<string> GetCityNameByStationId(int stationId)
        {
            string sql = $"SELECT c.Name FROM Cities c JOIN CityStations cs ON c.Id = cs.City_Id WHERE cs.Station_Id = {stationId}";
            var result = await _databaseService.ExecuteSQL(sql);

            return result[0][0];
        }
    }
}
