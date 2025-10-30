using API.DTOs;
using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IComplaintService
    {
        public Task<bool> MakeComplaint(string userId, string ticketId, string content);
        public Task<bool> EditComplaint(string complaintId, string content);
        public Task<bool> RemoveComplaint(string complaintId);
        public Task<(List<ComplaintWithAnswer>, bool)> ListAnsweredComplaintsByUser(string userId);
        public Task<(List<ComplaintWithoutAnswer>, bool)> ListUnansweredComplaintsByUser(string userId);
        public Task<(List<ComplaintWithoutAnswer>, bool)> ListUnansweredComplaintsByAdmin();
        public Task<bool> AnswerComplaint(string adminId, string complaintId, string response);
    }
}
