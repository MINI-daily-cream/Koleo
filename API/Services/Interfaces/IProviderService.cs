namespace API.Services.Interfaces
{
    public interface IProviderService
    {
        public Task<bool> AddProvider(string providerName);
        public Task<bool> RemoveProvider(Guid providerId);
        public Task<bool> EditProvider(Guid providerId, string newName);
    }
}
