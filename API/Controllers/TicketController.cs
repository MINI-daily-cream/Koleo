using API.DTOs;
using API.Services.Interfaces;
using Domain;
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

        [HttpGet("list-by-user/{id}")]
        public Task<List<ConnectionInfoObject>> List(int id)
        {
            return Task.FromResult(_ticketService.ListByUser(id).Result.Item1);
        }

        [HttpGet("buy/{id}")]
        public Task<bool> Buy(string id, [FromBody] BuyTicketDTO info)
        {
            return _ticketService.Buy(id, info.connections, info.targetName, info.targetSurname);
        }
    }
}
