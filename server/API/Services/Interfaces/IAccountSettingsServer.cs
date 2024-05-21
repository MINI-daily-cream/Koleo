namespace API.Services.Interfaces
{
    public interface IAccountSettingsServer
    {
        public Task<bool> CreateAccount(string name, string surname, string email, string password, string? cardNumber);
        public Task<bool> VerifyAccount(string email);
        public Task<bool> RemoveAccount(Guid id);
        public Task<bool> AuthoriseAccount(string email, string password);
<<<<<<< Updated upstream:server/API/Services/Interfaces/IAccountSettingsServer.cs
=======
        public Task<bool> GiveAdminPermissions(string userId);
        public Task<bool> RejectAdminRequest(string userId);
        public Task<bool> DeleteUser(string userId);
        public Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);

>>>>>>> Stashed changes:server/API/Services/Interfaces/IAdminService.cs
    }
}
