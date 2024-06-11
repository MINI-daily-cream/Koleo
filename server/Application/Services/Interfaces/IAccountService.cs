using Domain;

namespace API.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountInfo>? GetAccountInfo(string userId);
        public Task<bool> UpdateAccountInfo(string userId, string newName, string newSurname, string newEmail);
        public Task<bool> ChangeUserPassword(string id, string newPassword, string oldPasword);
        public Task<bool> DeleteUserAccount(string id);

    }
}
