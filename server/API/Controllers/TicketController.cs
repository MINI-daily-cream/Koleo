using API.DTOs;
using API.Services.Interfaces;
using Domain;
using Koleo.Models;
using Koleo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServive _ticketService;
        public TicketController(ITicketServive ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize]
        [HttpGet("list-by-user/{userId}")]
        public Task<List<TicketInfoDTO>> List(string userId)
        {
            Console.WriteLine("o cholera");
            Console.WriteLine(userId);
            Console.WriteLine(User.Identity.Name);
            if(userId != User.Identity.Name)
            {
                return null;
            }
            return Task.FromResult(_ticketService.ListByUser(userId.ToUpper()).Result.Item1);
        }
        [Authorize]
        [HttpGet("list-by-user-future-connections/{userId}")]
        public Task<List<TicketInfoDTO>> ListFuture(string userId)
        {
            Console.WriteLine("o cholera");
            Console.WriteLine(userId);
            Console.WriteLine(User.Identity.Name);
            if(userId != User.Identity.Name)
            {
                return null;
            }
            return Task.FromResult(_ticketService.ListByUserFutureConnections(userId.ToUpper()).Result.Item1);
        }
        [Authorize]
        [HttpGet("list-by-user-past-connections/{userId}")]
        public Task<List<TicketInfoDTO>> ListPast(string userId)
        {
            Console.WriteLine("o cholera");
            Console.WriteLine(userId);
            Console.WriteLine(User.Identity.Name);
            if(userId != User.Identity.Name)
            {
                return null;
            }
            return Task.FromResult(_ticketService.ListByUserPastConnections(userId.ToUpper()).Result.Item1);
        }

        // [Authorize]
        [AllowAnonymous]
        [HttpPost("buy/{userId}")]
        public async Task<ActionResult<string>> Buy(string userId, [FromBody] BuyTicketDTO info)
        {
            Console.WriteLine("Panie");
            Console.WriteLine(userId);
            Console.WriteLine(User.Identity.Name);
            // if(userId != User.Identity.Name)
            // {
            //     return Forbid();
            // }
            return (await _ticketService.Buy(userId.ToUpper(), info.connectionIds, info.targetName, info.targetSurname, info.seat)).Item1;
        }

        [HttpPost("generate/{userId}/{ticketId}")]
        public Task<bool> Generate(string userId, string ticketId)
        {
            return _ticketService.Generate(userId.ToUpper(), ticketId.ToUpper());
        }

        [HttpPost("remove/{ticketId}")]
        public Task<bool> Remove(string ticketId)
        {
            return _ticketService.Remove(ticketId.ToUpper());
        }

        //public Task<bool> Add(string userId, List<Connection> connections, string targetName, string targetSurname);
        // to się wywołuje w buy
        [HttpPut("change-details/{userId}/{ticketId}")]
        // public Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname)
        public Task<bool> ChangeDetails(string userId, string ticketId, [FromBody] ChangeTicketDetailsDTO info)
        {
            return _ticketService.ChangeDetails(userId.ToUpper(), ticketId.ToUpper(), info.targetName, info.targetSurname);
        }
        [Authorize]
        [HttpGet("get-ticket-for-complaint/{userId}/{ticketId}")]
        public Task<TicketInfoDTO> GetTicketForComplaint(string userId, string ticketId)
        {
            //Console.WriteLine("o cholera");
            //Console.WriteLine(userId);
            //Console.WriteLine(User.Identity.Name);
            if(userId != User.Identity.Name)
            {
               return null;
            }
            return Task.FromResult(_ticketService.GetTicketByIdToComplaint(userId.ToUpper(), ticketId.ToUpper()).Result);
        }
    }
}