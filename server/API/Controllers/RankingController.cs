using API.Services.Interfaces;
using Domain;
using Koleo.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]

    public class RankingController : ControllerBase
    {
        private readonly IRankingService _RankingService;

        public RankingController(IRankingService rankingService)
        {
            _RankingService = rankingService;
        }

        [HttpGet("{id}")]
        public Task<List<RankingInfo>> Get(string id)
        {
            return _RankingService.GetByUser(id);
            
        }

    }
}
