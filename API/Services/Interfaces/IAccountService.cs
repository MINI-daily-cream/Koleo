using Domain;

namespace API.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountInfo>? GetAccountInfo(string userId);
        public Task<bool> UpdateAccountInfo(string userId, string newName, string newSurname, string newEmail);
    }
}
