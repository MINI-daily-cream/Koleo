using API.DTOs;
using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface ITicketServive
    {
        public Task<(string, bool)> Buy(string userId, List<string> connectionsIds, string targetName, string targetSurname, string seat);
        public Task<(List<TicketInfoDTO>, bool)> ListByUser(string userId);
        public Task<bool> Generate(string userId, string ticketId);
        public Task<bool> Remove(string ticketId);
        public Task<(string, bool)> Add(string userId, List<string> connectionsIds, string targetName, string targetSurname);
        public Task<bool> ChangeDetails(string userId, string ticketId, string newTargetName, string newTargetSurname);
        public Task<TicketInfoDTO> GetTicketByIdToComplaint(string userId, string ticketId);
        public Task<(List<TicketInfoDTO>, bool)> ListByUserFutureConnections(string userId);
        public Task<(List<TicketInfoDTO>, bool)> ListByUserPastConnections(string userId);
    }
}
