using Application.Users;
using Koleo.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Authk.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("healthcheck")]
        public ActionResult<string> Get()
        {
            //  return _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;  
            Console.WriteLine(User.Claims.FirstOrDefault(x => x.Type == "Role").Value);
            return "healthy";
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            // return await dataContext.Users.ToListAsync();
            return await _mediator.Send(new List.Query());
        }

        // [Authorize]

        // [HttpGet("{username}")]
        // public  async Task<ActionResult<User>> GetUser(string username)
        // {
        //     Console.WriteLine("Heja");
        //     Console.WriteLine(username);
        //     Console.WriteLine(User.Identity.Name);
        //     if(username != User.Identity.Name)
        //     {
        //         return Forbid();
        //     }
        //     Console.WriteLine("przeszlo");
        //     return await dataContext.Users.FindAsync(new Guid(username));
        //     // return await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        // }
    }
}