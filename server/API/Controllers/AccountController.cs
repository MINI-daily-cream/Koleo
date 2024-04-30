using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Interfaces;
using API.Services;
using Koleo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private DataContext dataContext;
        private ITokenService tokenService;
        public AccountController(DataContext dataContext, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.dataContext = dataContext;
        }

        [HttpGet]
        public ActionResult<string> get()
        {
            Console.WriteLine("weszlo");
            return "yep";
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.username))
                return BadRequest("Username taken");
            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = registerDto.username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key,
                Name = "",
                Surname = "",
                Email = "",
                Password = "",
                CardNumber = ""
            };
            dataContext.Users.ToList().ForEach(x => Console.WriteLine(x.UserName));

            await dataContext.AddAsync(user);
            await dataContext.SaveChangesAsync();
            return new UserDto
            {
                username = user.UserName,
                token = tokenService.CreateToken(user)
            };
        }
        public async Task<bool> UserExists(string username)
        {
            return await dataContext.Users.AnyAsync(usr => usr.UserName == username);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if(! await UserExists(loginDto.username))
                return Unauthorized("Username not found");
            
            var user = await dataContext.Users.FirstOrDefaultAsync(usr => usr.UserName == loginDto.username);

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var test_password = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            if(test_password.Length != user.PasswordHash.Length)
                return Unauthorized("Wrong password");

            for(int i = 0; i < test_password.Length; i++)
            {
                if(test_password[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Wrong password");
                }
            }
            return new UserDto
            {
                username = loginDto.username,
                token = tokenService.CreateToken(user)
            };
        }
    }
}