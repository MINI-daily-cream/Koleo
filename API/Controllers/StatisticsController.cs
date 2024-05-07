using API.Services.Interfaces;
using Domain;
using Koleo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService _statisticService)
        {
            statisticsService= _statisticService;
        }


        [HttpGet("{id}")]
        public Task<StatisticsInfo>? Get(string id)
        {
            return statisticsService.GetByUser(id);
        }

    }
}
