using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<bool> RemoveAccount(Guid id);
        public Task<bool> AuthoriseAccount(string email, string password);
        public Task<bool> GiveAdminPermissions(string userId);
        public Task<bool> RejectAdminRequest(string userId);
        public Task<bool> DeleteUser(string userId);
        public Task<(List<string>, bool)> ListAdminCandidates();
        public Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);

    }
}
