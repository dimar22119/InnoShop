using UserManagementApi.Models;

namespace UserManagementApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
