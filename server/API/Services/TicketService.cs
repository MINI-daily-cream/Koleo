using Domain;
using Koleo.Models;
using KoleoPL.Services;
using System.Reflection.Metadata;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Org.BouncyCastle.Asn1.X509.SigI;
using API.Services.Interfaces;
using API.DTOs;
using Org.BouncyCastle.Asn1.X509;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Koleo.Services
{
    public class TicketService : ITicketServive
    {
        private readonly IDatabaseServiceAPI _databaseService;
        private readonly IPaymentService _paymentService;
        private readonly IGetInfoFromIdService _getInfoFromIdService;
        public TicketService(IDatabaseServiceAPI databaseService, IPaymentService paymentService, 
            IGetInfoFromIdService getInfoFromIdService)
        {
            _databaseService = databaseService;
            _paymentService = paymentService;
            _getInfoFromIdService = getInfoFromIdService;
        }
        public async Task<(string, bool)> Buy(string userId, List<string> connectionsIds, string targetName, string targetSurname, string seat)
        {
            if (await _paymentService.ProceedPayment())
            {
                var tmpResult = await Add(userId, connectionsIds, targetName, targetSurname);
                await _databaseService.SaveConnectionSeatInfo(connectionsIds[0], seat);
                return tmpResult;
            }
            return ("", false);
        }
        public async Task<(List<TicketInfoDTO>, bool)> ListByUser(string userId)
        {
            var result = await _getInfoFromIdService.GetTicketsByUser(userId);
            if (!result.Item2) return (new List<TicketInfoDTO> { }, false);
            List<string> ticketIds = result.Item1;
            List<TicketInfoDTO> connectionsInfo = new List<TicketInfoDTO>();
            foreach (string ticketId in ticketIds)
            {
                var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoList(ticketId, connectionsInfo);
                if (!tmpResult) return (new List<TicketInfoDTO> { }, false);
            }
            return (connectionsInfo, true);
        }
        public async Task<(List<TicketInfoDTO>, bool)> ListByUserFutureConnections(string userId)
        {
            var result = await _getInfoFromIdService.GetTicketsByUser(userId);
            if (!result.Item2) return (new List<TicketInfoDTO> { }, false);
            List<string> ticketIds = result.Item1;
            List<TicketInfoDTO> connectionsInfo = new List<TicketInfoDTO>();
            foreach (string ticketId in ticketIds)
            {
                var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoList(ticketId, connectionsInfo);
                if (!tmpResult) return (new List<TicketInfoDTO> { }, false);
            }
            List<TicketInfoDTO> futureConnections = new List<TicketInfoDTO>();
            foreach(TicketInfoDTO info in connectionsInfo)
            {
                if(DateOnly.FromDateTime(DateTime.Now) < info.StartDate || (DateOnly.FromDateTime(DateTime.Now) == info.StartDate 
                &&
                 TimeOnly.FromDateTime(DateTime.Now) < info.StartTime))
                 {
                    futureConnections.Add(info);
                 }
            }
            return (futureConnections, true);
        }
        public async Task<(List<TicketInfoDTO>, bool)> ListByUserPastConnections(string userId)
        {
            var result = await _getInfoFromIdService.GetTicketsByUser(userId);
            if (!result.Item2) return (new List<TicketInfoDTO> { }, false);
            List<string> ticketIds = result.Item1;
            List<TicketInfoDTO> connectionsInfo = new List<TicketInfoDTO>();
            foreach (string ticketId in ticketIds)
            {
                var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoList(ticketId, connectionsInfo);
                if (!tmpResult) return (new List<TicketInfoDTO> { }, false);
            }
            List<TicketInfoDTO> pastConnections = new List<TicketInfoDTO>();
            foreach(TicketInfoDTO info in connectionsInfo)
            {
                if(DateOnly.FromDateTime(DateTime.Now) > info.StartDate || (DateOnly.FromDateTime(DateTime.Now) == info.StartDate 
                &&
                 TimeOnly.FromDateTime(DateTime.Now) > info.StartTime))
                 {
                    pastConnections.Add(info);
                 }
            }
            return (pastConnections, true);
        }
        public async Task<bool> Generate(string userId, string ticketId)
        {
            List<TicketInfoDTO> connectionsInfo = new List<TicketInfoDTO>();
            var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoList(ticketId, connectionsInfo);
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
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream($"ticket_{ticketId}.pdf", FileMode.Create));
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
                document.Close();
                
                sql = $"SELECT Email FROM Users WHERE Id = '{userId}'";
                resultMain = await _databaseService.ExecuteSQL(sql);
                if (!resultMain.Item2) return false;
                result = resultMain.Item1;
                userData = result[0];
                string destEmail = userData[0],
                subject = $"Koleo ticket",
                message = "Your purhcased ticket:";
                EmailSender emailSender = new EmailSender();
                await emailSender.SendEmailAsync(destEmail, subject, message, $"ticket_{ticketId}.pdf");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

            string getTicketNumberSql = "SELECT COUNT(*) FROM Tickets";
            var ticketNumberResult = await _databaseService.ExecuteSQLLastRow(getTicketNumberSql);
            if (!ticketNumberResult.Item2) return ("", false);
            var ticketNumber = (System.Int64)ticketNumberResult.Item1[0][0];

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
        public async Task<TicketInfoDTO> GetTicketByIdToComplaint(string userId, string ticketId)
        {
            List<TicketInfoDTO> connectionsInfo = new List<TicketInfoDTO>();
            var tmpResult = await _getInfoFromIdService.UpdateConnectionsInfoList(ticketId, connectionsInfo);
            if (!tmpResult) return new TicketInfoDTO();
            return connectionsInfo[0];
        }
    }
}
