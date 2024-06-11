namespace API.Services.Interfaces
{
    public interface IProviderService
    {
        public Task<bool> AddProvider(string providerName);
        public Task<bool> RemoveProvider(string providerId);
        public Task<bool> EditProvider(string providerId, string newName);
    }
}
