using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;
using Koleo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServive _ticketService;
        public TicketController(ITicketServive ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("list-by-user/{userId}")]
        public Task<(List<ConnectionInfoObject>, bool)> List(string userId)
        {
            return _ticketService.ListByUser(userId);
        }

        [HttpPost("buy/{userId}")]
        public Task<bool> Buy(string userId, [FromBody] BuyTicketDTO info)
        {
            return _ticketService.Buy(userId, info.connections, info.targetName, info.targetSurname);
        }

        [HttpPost("generate/{userId}/{ticketId}")]
        public Task<bool> Generate(string userId, string ticketId)
        {
            return _ticketService.Generate(userId, ticketId);
        }

        [HttpPost("remove/{ticketId}")]
        public Task<bool> Remove(string ticketId)
        {
            return _ticketService.Remove(ticketId);
        }

        //public Task<bool> Add(string userId, List<Connection> connections, string targetName, string targetSurname);
        // to się wywołuje w buy
        [HttpPut("change-details/{userId}/{ticketId}")]
        public Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname)
        {
            return _ticketService.ChangeDetails(userId, ticketId, newTargetName, newTargetSurname);
        }
    }
}
