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
    public class ConnectionSeatsController : ControllerBase
    {
        private readonly IConnectionSeatsService _connectionSeatsService;

        public ConnectionSeatsController(IConnectionSeatsService connectionSeatsService)
        {
            _connectionSeatsService = connectionSeatsService;
        }

        [HttpGet("get/{connectionId}")]
        public async Task<List<int>> Get(string connectionId)
        {
            
            return await _connectionSeatsService.GetConnectionSeats(connectionId);
        }
    }
}