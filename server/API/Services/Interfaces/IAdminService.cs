namespace API.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<bool> CreateAccount(string name, string surname, string email, string password, string? cardNumber);
        public Task<bool> VerifyAccount(string email);
        public Task<bool> RemoveAccount(Guid id);
        public Task<bool> AuthoriseAccount(string email, string password);
        public Task<bool> GiveAdminPermissions(string userId);
        public Task<bool> RejectAdminRequest(string userId);
        public Task<bool> DeleteUser(string userId);
    }
}
