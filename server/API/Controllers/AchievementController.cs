using API.Services.Interfaces;
using Domain;
using Koleo.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementsService _achievementsService;

        public AchievementController(IAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }

        [HttpGet("{id}")]

        public Task<List<AchievementInfo>> GetAchievement(string id) 
        {
            return _achievementsService.GetAchievementsByUser(id);
        }


    }
}
