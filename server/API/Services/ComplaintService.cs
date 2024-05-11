using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;

namespace Koleo.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IDatabaseServiceAPI _databaseService;

        public ComplaintService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> MakeComplaint(string userId, string ticketId, string content)
        {
            var sql = $"INSERT INTO Complaints (Id, User_Id, Ticket_Id, Content) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{userId}', '{ticketId}', '{content}')";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        public async Task<bool> EditComplaint(string complaintId, string content)
        {
            var sql = $"UPDATE Complaints SET Content = '{content}' WHERE Id = '{complaintId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }

        public async Task<bool> RemoveComplaint(string complaintId)
        {
            var sql = $"DELETE FROM Complaints WHERE Id = '{complaintId}'";
            var result1 = await _databaseService.ExecuteSQL(sql);
            sql = $"DELETE FROM AdminComplaints WHERE Complaint_Id = '{complaintId}'";
            var result2 = await _databaseService.ExecuteSQL(sql);
            return result1.Item2 && result2.Item2;
        }

        public async Task<(List<ComplaintWithAnswer>, bool)> ListAnsweredComplaintsByUser(string userId)
        {
            string sql = $"SELECT c.Ticket_Id, c.Content, ac.Response FROM Complaints c JOIN AdminComplaints ac ON c.Id = ac.Complaint_Id WHERE c.User_Id = '{userId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (new List<ComplaintWithAnswer> { }, false);
            return (result.Item1.Select(row => new ComplaintWithAnswer
            {
                ticketId = row[0],
                content = row[1],
                response = row[2]
            }).ToList(), true);
        }

        public async Task<bool> AnswerComplaint(string adminId, string complaintId, string response)
        {
            var sql = $"INSERT INTO AdminComplaints (Id, Admin_Id, Complaint_Id, Response) VALUES ('{Guid.NewGuid().ToString().ToUpper()}', '{adminId}', '{complaintId}', '{response}')";
            var result = await _databaseService.ExecuteSQL(sql);
            return result.Item2;
        }
        public async Task<(List<ComplaintWithoutAnswer>, bool)> ListUnansweredComplaintsByUser(string userId) 
        {
            var sql = $"SELECT c.Id, c.User_Id, c.Ticket_Id, c.Content FROM Complaints c LEFT JOIN AdminComplaints ac ON c.Id = ac.Complaint_Id WHERE ac.Complaint_Id IS NULL AND c.User_Id = '{userId}';";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (new List<ComplaintWithoutAnswer> { }, false);
            return (result.Item1.Select(row => new ComplaintWithoutAnswer
            {
                User_Id = row[0],
                Ticket_Id = row[1],
                Content = row[3]
            }).ToList(), true);
        }
        public async Task<(List<ComplaintWithoutAnswer>, bool)> ListUnansweredComplaintsByAdmin()
        {
            var sql = "SELECT c.Id, c.User_Id, c.Ticket_Id, c.Content FROM Complaints c LEFT JOIN AdminComplaints ac ON c.Id = ac.Complaint_Id WHERE ac.Complaint_Id IS NULL;";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (new List<ComplaintWithoutAnswer> { }, false);
            return (result.Item1.Select(row => new ComplaintWithoutAnswer
            {
                User_Id = row[0],
                Ticket_Id = row[1],
                Content = row[2]
            }).ToList(), true);
        }

        // dla Admina też powinniśmy robić logikę w Services?
    }
}
