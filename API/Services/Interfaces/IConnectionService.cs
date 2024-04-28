using Domain;
using Koleo.Models;

namespace API.Services.Interfaces
{
    public interface IConnectionService
    {
        public Task<Connection>? Get();
    }
}
