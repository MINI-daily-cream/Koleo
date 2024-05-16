using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly ITicketServive _ticketService;
        private readonly DataContext _context;
        public ConnectionController(ITicketServive ticketService, DataContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }

        [HttpGet]
        public Task<List<TicketInfoDTO>> Get()
        {
            return Task.FromResult(_ticketService.GetConnectionsInfo(_context.Connections.ToList()).Result.Item1);
        }

        //[HttpGet]
        //public Task<List<TicketInfoDTO>> Get(FindConnectionsDTO filters)
        //{
        //    return Task.FromResult(_ticketService.GetConnectionsInfo(_context.Connections.ToList()).Result.Item1);
        //}
    }
}
