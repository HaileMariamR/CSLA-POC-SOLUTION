using Data_Layer.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Interface
{
    public interface IAuthService
    {
        Task<AuthModel> AuthenticateUser(string username, string password);
        string GenerateRefreshToken();
        Task<User> GetCurrentUserFromExpiredToken(string token , string username);

        Task<User> getCurrentUser(string username);

        JwtSecurityToken GenerateToken();
    }
}
