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
    public class CityController : ControllerBase
    {
        private readonly DataContext _context;
        public CityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<City>> GetCities()
        {
            return Task.FromResult(_context.Cities.ToList());
        }

        // [HttpGet]
        // public Task<List<CityStation>> GetCityStations(Guid Id)
        // {
        //     return Task.FromResult(_context.Connections.ToList());
        // }
    }
}
