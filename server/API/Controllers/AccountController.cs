using API.Services.Interfaces;
using Domain;
using Koleo.Services;
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
        private readonly IAdminService _adminService;
        public AccountController(IAccountService accountService, IAdminService adminService) 
        {
            _accountService = accountService;
            _adminService = adminService;
        }

        [HttpGet("{id}")] // check user account - Admin
        public Task<AccountInfo> Get(string id)
        {
            return _accountService.GetAccountInfo(id.ToUpper())!;
        }

        [HttpPut("{id}")]
        public Task<bool> Update(string id, [FromBody]AccountInfo newInfo)
        {
            return _accountService.UpdateAccountInfo(id.ToUpper(), newInfo.Name, newInfo.Surname, newInfo.Email);
        }
        [HttpGet("admin-candidates")] // check user account - Admin
        public Task<List<string>> ListAdminCandidates()
        {
            return Task.FromResult(_adminService.ListAdminCandidates().Result.Item1);
        }


        [HttpPut("admin-request/accept")]
        public Task<bool> Accept(string userId)
        {
            return _adminService.GiveAdminPermissions(userId.ToUpper());
        }

        [HttpPut("admin-request/reject")]
        public Task<bool> Reject(string userId)
        {
            return _adminService.RejectAdminRequest(userId.ToUpper());
        }
        [HttpDelete("delete-user/{userId}")]
        public Task<bool> DeleteUser(string userId)
        {
            return _adminService.DeleteUser(userId.ToUpper());
        }
    }
}
