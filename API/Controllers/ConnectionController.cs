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
        //private readonly IAccountService _accountService;
        //public ConnectionController(IAccountService accountService)
        //{
        //    _accountService = accountService;
        //}

        private readonly DataContext _context;
        public ConnectionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Connection Get()
        {
            return _context.Connections.ToList().First();
        }
    }
}
