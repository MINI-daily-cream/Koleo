using API.Interfaces;
using API.Services.Interfaces;
using Persistence;


namespace Application.Auth
{
    public abstract class AuthBaseHandler
    {
        protected readonly DataContext _dataContext;
        protected readonly ITokenService _tokenService;
        protected readonly IUserService _userService;
        protected readonly IStatisticsService _statisticsService;
        protected AuthBaseHandler(DataContext dataContext, ITokenService tokenService, IUserService userService, IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            _userService = userService;
            _tokenService = tokenService;
            _dataContext = dataContext;
        }
    }
}