using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;
using Koleo.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }
        [HttpPut("make/{userId}")]
        public Task<bool> Make(string userId, ComplaintDTO complaintDto)
        {
            return _complaintService.MakeComplaint(userId, complaintDto.ticketId, complaintDto.content);
        }
        [HttpPut("edit/{complaintId}")]
        public Task<bool> Edit(string complaintId, ComplaintDTO complaintDto)
        {
            return _complaintService.EditComplaint(complaintId, complaintDto.content);
        }
        [HttpDelete("remove/{complaintId}")]
        public Task<bool> Remove(string complaintId)
        {
            return _complaintService.RemoveComplaint(complaintId);
        }
        [HttpGet("list-by-user-answered/{userId}")]
        public Task<List<ComplaintWithAnswer>> UserListAnswered(string userId) { 
            return Task.FromResult(_complaintService.ListAnsweredComplaintsByUser(userId).Result.Item1);
        }
        [HttpGet("list-by-user-unanswered/{userId}")]
        public Task<List<ComplaintWithoutAnswer>> UserListUnanswered(string userId)
        {
            return Task.FromResult(_complaintService.ListUnansweredComplaintsByUser(userId).Result.Item1);
        }
        [HttpGet("list-by-admin")]
        public Task<List<ComplaintWithoutAnswer>> AdminListUnanswered()
        {
            return Task.FromResult(_complaintService.ListUnansweredComplaintsByAdmin().Result.Item1);
        }
        [HttpPut("answer/{adminId}")]
        public Task<bool> ResponseComplaint(string adminId, AdminComplaintDTO admincomplaintDto)
        {
            return _complaintService.AnswerComplaint(adminId, admincomplaintDto.complaintId, admincomplaintDto.response);
        }

    }
}
