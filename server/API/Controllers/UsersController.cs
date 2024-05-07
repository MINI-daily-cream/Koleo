using Koleo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private DataContext dataContext;
        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        } 

        [AllowAnonymous]
        [HttpGet("healthcheck")]
        public ActionResult<string> Get()
        {
            return "healthy";
        }
   

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await dataContext.Users.ToListAsync();
        }

        [Authorize]
        
        [HttpGet("{username}")]
        public  async Task<ActionResult<User>> GetUser(string username)
        {
            if(username != User.Identity.Name)
            {
                return Forbid();
            }

            return await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

    }
}