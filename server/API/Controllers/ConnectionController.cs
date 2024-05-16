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
        private readonly IConnectionService _connectionService;
        private readonly DataContext _context;
        public ConnectionController(IConnectionService connectionService, DataContext context)
        {
            _connectionService = connectionService;
            _context = context;
        }

        [HttpGet]
        public Task<List<TicketInfoDTO>> Get()
        {
            return Task.FromResult(_connectionService.GetConnectionsInfo(_context.Connections.ToList()).Result.Item1);
        }

        [HttpGet("filtered")]
        public Task<List<TicketInfoDTO>> Get(FindConnectionsDTO filters)
        {
            return Task.FromResult(_connectionService.GetFilteredConnections(filters).Result.Item1);
        }
    }
}
