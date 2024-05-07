using API.Services.Interfaces;
using Application;
using Domain;
using Koleo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        private readonly DataContext _context;
        public AdvertismentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("getAllAds")]
        public Task<List<Advertisment>> GetAds()
        {
            return Task.FromResult(_context.Advertisments.ToList());
        }

        // [HttpGet]
        // public Task<List<CityStation>> GetCityStations(Guid Id)
        // {
        //     return Task.FromResult(_context.Connections.ToList());
        // }
    }
}
