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
        public Task<List<ConnectionInfoObject>> List(string userId)
        {
            return Task.FromResult(_ticketService.ListByUser(userId.ToUpper()).Result.Item1);
        }

        [HttpPost("buy/{userId}")]
        public async Task<string> Buy(string userId, [FromBody] BuyTicketDTO info)
        {
            Console.WriteLine("pala");
            return (await _ticketService.Buy(userId.ToUpper(), info.connectionIds, info.targetName, info.targetSurname)).Item1;
        }

        [HttpPost("generate/{userId}/{ticketId}")]
        public Task<bool> Generate(string userId, string ticketId)
        {
            return _ticketService.Generate(userId.ToUpper(), ticketId.ToUpper());
        }

        [HttpPost("remove/{ticketId}")]
        public Task<bool> Remove(string ticketId)
        {
            return _ticketService.Remove(ticketId.ToUpper());
        }

        //public Task<bool> Add(string userId, List<Connection> connections, string targetName, string targetSurname);
        // to się wywołuje w buy
        [HttpPut("change-details/{userId}/{ticketId}")]
        public Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname)
        {
            return _ticketService.ChangeDetails(userId.ToUpper(), ticketId.ToUpper(), newTargetName, newTargetSurname);
        }
    }
}