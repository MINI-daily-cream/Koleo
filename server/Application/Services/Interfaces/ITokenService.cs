using Koleo.Models;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}