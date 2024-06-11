using API.DTOs;
using API.Services.Interfaces;
using Koleo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }
        [HttpPut("add-provider")]
        public Task<bool> Add(string providerName)
        {
            return _providerService.AddProvider(providerName);
        }
        [HttpPut("edit-provider/{providerId}")]
        public Task<bool> Edit(string providerId, string providerName)
        {
            return _providerService.EditProvider(providerId, providerName);
        }
        [HttpDelete("remove/{providerId}")]
        public Task<bool> Remove(string providerId)
        {
            return _providerService.RemoveProvider(providerId);
        }
    }
}
