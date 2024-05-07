using API.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public Task<AccountInfo> Get(string id)
        {
            return _accountService.GetAccountInfo(id)!;
        }

        [HttpPut("{id}")]
        public Task<bool> Update(string id, [FromBody]AccountInfo newInfo)
        {
            return _accountService.UpdateAccountInfo(id, newInfo.Name, newInfo.Surname, newInfo.Email);
        }

        [HttpPut("{id}/ChangePassword")]
        public Task<bool> ChangePassword(string id, string newPassword, string oldPassword)
        {
          
            return _accountService.ChangeUserPassword(id, newPassword, oldPassword);
        }
        [HttpDelete("{id}")]
        public Task<bool> DeleteAccount(string id)
        {
            
            return _accountService.DeleteUserAccount(id);
        }

    }
}
