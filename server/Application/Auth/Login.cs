using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Interfaces;
using API.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Auth
{
    public class Login
    {
        public class Query : IRequest<UserDto>
        {
            public LoginDto loginDto { get; set; }
        }

        public class LoginHandler : AuthBaseHandler, IRequestHandler<Query, UserDto>
        {
            public LoginHandler(DataContext dataContext, ITokenService tokenService, IUserService userService, IStatisticsService statisticsService)
                : base(dataContext, tokenService, userService, statisticsService) { }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _userService.UserExists(request.loginDto.email))
                    throw new UnauthorizedAccessException("Username not found");

                var user = await _dataContext.Users.FirstOrDefaultAsync(usr => usr.Email == request.loginDto.email);

                using var hmac = new HMACSHA512(user.PasswordSalt);

                var test_password = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.loginDto.password));

                if (test_password.Length != user.PasswordHash.Length)
                    throw new UnauthorizedAccessException("Wrong password");

                for (int i = 0; i < test_password.Length; i++)
                {
                    if (test_password[i] != user.PasswordHash[i])
                    {
                        throw new UnauthorizedAccessException("Wrong password");
                    }
                }
                _statisticsService.Update(user.Id.ToString(), null);

                return new UserDto
                {
                    id = user.Id.ToString(),
                    token = _tokenService.CreateToken(user)
                };
            }
        }
    }
}