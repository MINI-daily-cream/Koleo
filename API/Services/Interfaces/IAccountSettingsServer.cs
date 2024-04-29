namespace API.Services.Interfaces
{
    public interface IAccountSettingsServer
    {
        public Task<bool> CreateAccount(string name, string surname, string email, string password, string? cardNumber);
        public Task<bool> VerifyAccount(string email);
        public Task<bool> RemoveAccount(Guid id);
        public Task<bool> AuthoriseAccount(string email, string password);
    }
}
