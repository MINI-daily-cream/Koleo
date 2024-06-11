using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Interfaces;
using API.Services.Interfaces;
using Koleo.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;


namespace Application.Auth
{
    public class Register
    {
        public class Query : IRequest<UserDto>
        {
            public RegisterDto registerDto { get; set; }
        }

        public class RegisterHandler : AuthBaseHandler, IRequestHandler<Query, UserDto>
        {
            public RegisterHandler(DataContext dataContext, ITokenService tokenService, IUserService userService, IStatisticsService statisticsService)
                : base(dataContext, tokenService, userService, statisticsService) { }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                if (await _userService.UserExists(request.registerDto.email))
                    throw new BadHttpRequestException("Username taken");

                using var hmac = new HMACSHA512();
                var user = new User
                {
                    UserName = "",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.registerDto.password)),
                    PasswordSalt = hmac.Key,
                    Name = request.registerDto.name,
                    Surname = request.registerDto.surname,
                    Email = request.registerDto.email,
                    Password = "",
                    CardNumber = ""
                };

                _dataContext.Users.ToList().ForEach(x => Console.WriteLine(x.UserName));
                await _dataContext.AddAsync(user);
                await _dataContext.SaveChangesAsync();
                _statisticsService.Update(user.Id.ToString(), null);

                return new UserDto
                {
                    id = user.Id.ToString(),
                    token = _tokenService.CreateToken(user)
                };
                throw new NotImplementedException();
            }
        }
    }
}