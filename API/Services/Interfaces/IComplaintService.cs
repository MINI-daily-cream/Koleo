using API.DTOs;
using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IComplaintService
    {
        public Task<bool> MakeComplaint(string userId, string ticketId, string content);
        public Task<bool> EditComplaint(string complaintId, string ticketId, string content);
        public Task<bool> RemoveComplaint(string complaintId);
        public Task<(List<ComplaintWithAnswer>, bool)> ListComplaints(string userId);
        public Task<(List<ComplaintWithoutAnswer>, bool)> ListUnansweredComplaints();
        public Task<bool> AnswerComplaint(string adminId, string complaintId, string response);
    }
}
