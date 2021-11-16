using System.Collections.Generic;
using Lindholm.Webshop2021.Security.Model;

namespace Lindholm.Webshop2021.Security
{
    public interface IAuthService
    {
        string GenerateJwtToken(LoginUser userUserName);
        string Hash(string password);
        List<Permission> GetPermissions(int userId);
    }
}